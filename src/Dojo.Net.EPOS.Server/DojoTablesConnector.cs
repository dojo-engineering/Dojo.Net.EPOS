using System.Net.WebSockets;
using System.Text;
using Dojo.Net.EPOS.Server.Exceptions;
using Dojo.Net.EPOS.Server.JsonRpc;
using Dojo.Net.EPOS.Server.Schema;
using Dojo.Net.EPOS.Server.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Dojo.Net.EPOS.Server
{
    public class DojoTablesConnector
    {
        private const string StagingUrl = "wss://staging-api.dojo.dev/ws/v1/tables/epos";
        private const string ProductionUrl = "wss://api.dojo.tech/ws/v1/tables/epos";

        public string? ResellerId { get; init; }
        public string SoftwareHouseId { get; }
        public bool IsSandbox { get; }
        private string Token { get; }

        private JsonSerializerSettings jsonSerializerSettings {get;}

        public DojoTablesConnector(string accountId, string apiKey, string softwareHouseId, bool isSandbox)
        {
            if (accountId == null)
                throw new ArgumentNullException(nameof(accountId));

            if (apiKey == null)
                throw new ArgumentNullException(nameof(apiKey));

            this.Token = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{accountId}:{apiKey}"));
            this.IsSandbox = isSandbox;
            this.SoftwareHouseId = softwareHouseId ?? throw new ArgumentNullException(nameof(softwareHouseId));

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

            await client.ConnectAsync(new Uri(this.IsSandbox ? StagingUrl : ProductionUrl), cancellationToken);

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
                        // log
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
                            throw new TablesException(TablesErrorCode.ErrorParseError, "Invalid request");
                            // log
                        }

                        requestId = requestEnvelope.Id ?? "";
                        response = await requestEnvelope.AcceptMessageAsync(serverHandler);
                    }
                    catch (TablesException ex)
                    {
                        response = new ErrorResponse(requestId, ex.ErrorCode, ex.Message);
                        // log
                    }
                    catch (Exception ex)
                    {
                        response = new ErrorResponse(requestId, TablesErrorCode.ErrorInternalPosError, ex.Message);
                        // log
                    }

                    if (response != null)
                    {
                        var responseMessage = JsonConvert.SerializeObject(response, jsonSerializerSettings);
                        await client.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(responseMessage)), WebSocketMessageType.Text, true, CancellationToken.None);
                    }
                }
            }
        }
    }
}