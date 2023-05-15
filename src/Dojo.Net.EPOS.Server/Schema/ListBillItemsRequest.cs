using Dojo.Net.EPOS.Server.JsonRpc;

namespace Dojo.Net.EPOS.Server.Schema
{
    public class ListBillItemsParams
    {
        public List<Guid>? SessionIds { get; set; }
        public RequestorInfo? RequestorInfo { get; set; }
    }

    public class ListBillItemsRequest : JsonRpcRequestEnvelope<ListBillItemsParams>
    {
        public override async Task<JsonRpcResponseEnvelope?> AcceptMessageAsync(ITablesAPIServer server)
        {
            return await server.AcceptMessageAsync(this);
        }
    }
}