using Dojo.Net.EPOS.Server.JsonRpc;

namespace Dojo.Net.EPOS.Server.Schema
{
    public class UnlockSessionParams
    {
        public string? SessionId { get; init; }
        public RequestorInfo? RequestorInfo { get; init; }        
    }

    public class UnlockSessionRequest : JsonRpc.JsonRpcRequestEnvelope<UnlockSessionParams>
    {
        public override async Task<JsonRpcResponseEnvelope?> AcceptMessageAsync(ITablesAPIServer server)
        {
            return await server.AcceptMessageAsync(this);
        }
    }
}