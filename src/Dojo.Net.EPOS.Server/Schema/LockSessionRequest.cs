using Dojo.Net.EPOS.Server.JsonRpc;

namespace Dojo.Net.EPOS.Server.Schema
{
    public class LockSessionParams
    {
        public Guid SessionId { get; set; }
        public RequestorInfo? RequestorInfo { get; set; }
    }

    public class LockSessionRequest : JsonRpc.JsonRpcRequestEnvelope<LockSessionParams>
    {
        public override async Task<JsonRpcResponseEnvelope?> AcceptMessageAsync(ITablesAPIServer server)
        {
            return await server.AcceptMessageAsync(this);
        }
    }
}