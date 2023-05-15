using System.Net.WebSockets;
using System.Text;
using Dojo.Net.EPOS.Server.Exceptions;
using Dojo.Net.EPOS.Server.JsonRpc;
using Dojo.Net.EPOS.Server.Schema;
using Dojo.Net.EPOS.Server.Serialization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Dojo.Net.EPOS.Server
{
    public class DojoTablesConnector
    {
        private const int ReconnectTimeout = 5000;
        private const string StagingUrl = "wss://staging-api.dojo.dev/ws/v1/tables/epos";
        private const string ProductionUrl = "wss://api.dojo.tech/ws/v1/tables/epos";
        public string? ResellerId { get; init; }
        public ILogger Logger { get; init; } = NullLogger.Instance;
        public string SoftwareHouseId { get; }
        public bool IsSandbox { get; }
        private string Token { get; }

        private JsonSerializerSettings jsonSerializerSettings { get; }

        public DojoTablesConnector(string accountId, string apiKey, string softwareHouseId, bool isSandbox)
        {
            if (accountId == null)
                throw new ArgumentNullException(nameof(accountId));

            if (apiKey == null)
                throw new ArgumentNullException(nameof(apiKey));

            this.Token = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{accountId}:{apiKey}"));
            this.IsSandbox = isSandbox;
            this.SoftwareHouseId = softwareHouseId ?? throw new ArgumentNullException(nameof(softwareHouseId));

            this.Logger.LogInformation($"Dojo Tables Connector initialized for account {accountId} and software house {softwareHouseId} in {(isSandbox ? "sandbox" : "production")} mode");

            this.jsonSerializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                }
            };

            jsonSerializerSettings.Converters.Add(new StringEnumConverter());
        }

        public async Task StartAsync(ITablesAPIServer serverHandler, CancellationToken cancellationToken)
        {
            do
            {
                using var client = new ClientWebSocket();
                client.Options.SetRequestHeader("Authorization", $"Basic {this.Token}");

                if (!string.IsNullOrEmpty(this.ResellerId))
                {
                    client.Options.SetRequestHeader("reseller-id", this.ResellerId);
                }

                if (!string.IsNullOrEmpty(this.SoftwareHouseId))
                {
                    client.Options.SetRequestHeader("software-house-id", this.SoftwareHouseId);
                }
            
                try
                {
                    var uri = new Uri(this.IsSandbox ? StagingUrl : ProductionUrl);
                    await client.ConnectAsync(uri, cancellationToken);

                    this.Logger.LogInformation($"Connected to Dojo Tables API at {uri}");

                    var receiveBuffer = new ArraySegment<byte>(new byte[1024]);

                    while (client.State == WebSocketState.Open)
                    {
                        WebSocketReceiveResult result;
                        using var ms = new MemoryStream();
                        do
                        {
                            result = await client.ReceiveAsync(receiveBuffer, cancellationToken);
                            ms.Write(receiveBuffer.Array!, receiveBuffer.Offset, result.Count);
                        }
                        while (!result.EndOfMessage);

                        ms.Seek(0, SeekOrigin.Begin);
                        if (result.MessageType == WebSocketMessageType.Text)
                        {
                            using var reader = new StreamReader(ms, Encoding.UTF8);

                            var message = await reader.ReadToEndAsync();

                            if (message == null)
                            {
                                this.Logger.LogError($"Failed to read message from Dojo");
                                continue;
                            }

                            JsonRpcRequestEnvelope? requestEnvelope = JsonConvert.DeserializeObject<JsonRpcRequestEnvelope>(message, new JsonRpcEnvelopeConverter());
                            JsonRpcResponseEnvelope? response = null;
                            string requestId = string.Empty;
                            try
                            {
                                if (requestEnvelope == null)
                                {
                                    requestId = "";
                                    this.Logger.LogError($"Failed to parse request from Dojo, message: {message}");
                                    throw new TablesException(TablesErrorCode.ErrorParseError, "Invalid request");
                                }

                                requestId = requestEnvelope.Id ?? "";
                                this.Logger.LogInformation($"Received request {requestEnvelope.Method} with id {requestId}");
                                response = await requestEnvelope.AcceptMessageAsync(serverHandler);
                            }
                            catch (TablesException ex)
                            {
                                response = new ErrorResponse(requestId, ex.ErrorCode, ex.Message);
                                this.Logger.LogWarning($"Sending error response. ErrorCode: {ex.ErrorCode}, ErrorReason: {ex.Message}");
                            }
                            catch (Exception ex)
                            {
                                response = new ErrorResponse(requestId, TablesErrorCode.ErrorInternalPosError, ex.Message);
                                this.Logger.LogError(ex, "Error processing request");
                            }

                            if (response != null)
                            {
                                var responseMessage = JsonConvert.SerializeObject(response, jsonSerializerSettings);
                                await client.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(responseMessage)), WebSocketMessageType.Text, true, CancellationToken.None);
                            }
                        }
                    }                    
                }
                catch (OperationCanceledException)
                {
                    this.Logger.LogInformation("Connection to Dojo Tables API cancelled");
                    return;
                }
                catch (WebSocketException ex)
                {
                    // TODO: check when authorization fails, we need to cancel attempt to connect
                    this.Logger.LogError(ex, "Error while connecting to Dojo Tables API");
                }
                catch (Exception ex)
                {
                    this.Logger.LogError(ex, "Unkown error while connecting to Dojo Tables API");
                }

                await Task.Delay(ReconnectTimeout, cancellationToken); 
            } while (!cancellationToken.IsCancellationRequested);
        }
    }
}