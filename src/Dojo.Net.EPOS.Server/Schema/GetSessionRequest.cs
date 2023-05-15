using Dojo.Net.EPOS.Server.JsonRpc;

namespace Dojo.Net.EPOS.Server.Schema
{
    public class GetSessionRequestParams
    {
        public required Guid SessionId { get; init; }

        public required RequestorInfo RequestorInfo { get; init; }
    }

    public class GetSessionRequest : JsonRpcRequestEnvelope<GetSessionRequestParams>
    {
        public override async Task<JsonRpcResponseEnvelope?> AcceptMessageAsync(ITablesAPIServer server)
        {
            return await server.AcceptMessageAsync(this);
        }
    }
}