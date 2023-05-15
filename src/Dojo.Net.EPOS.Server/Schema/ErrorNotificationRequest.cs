using Dojo.Net.EPOS.Server.JsonRpc;

namespace Dojo.Net.EPOS.Server.Schema
{
    public class ErrorNotificationRequestParams
    {
        public string? Id { get; set; }
        public string? ErrorCode { get; set; }
        public string? ErrorReason { get; set; }
    }

    public class ErrorNotificationRequest : JsonRpc.JsonRpcRequestEnvelope<ErrorNotificationRequestParams>
    {
        public override async Task<JsonRpcResponseEnvelope?> AcceptMessageAsync(ITablesAPIServer server)
        {
            await server.AcceptMessageAsync(this);

            return null;
        }
    }
}