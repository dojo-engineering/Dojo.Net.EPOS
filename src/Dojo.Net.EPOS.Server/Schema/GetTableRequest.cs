using Dojo.Net.EPOS.Server.JsonRpc;

namespace Dojo.Net.EPOS.Server.Schema
{
    public class GetTableRequestParams
    {
        public string? Name { get; set; }
        public RequestorInfo? RequestorInfo { get; set; }
    }

    public class GetTableRequest : JsonRpc.JsonRpcRequestEnvelope<GetTableRequestParams>
    {
        public override async Task<JsonRpcResponseEnvelope?> AcceptMessageAsync(ITablesAPIServer server)
        {
            return await server.AcceptMessageAsync(this);
        }
    }
}