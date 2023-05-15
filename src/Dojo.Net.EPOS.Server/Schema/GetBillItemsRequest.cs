using Dojo.Net.EPOS.Server.JsonRpc;

namespace Dojo.Net.EPOS.Server.Schema
{
    public class GetBillItemsRequestParams 
    {
        public Guid SessionId { get; set; }
        public RequestorInfo? RequestorInfo { get; set; } 
    }

    public class GetBillItemsRequest : JsonRpcRequestEnvelope<GetBillItemsRequestParams>
    {
        public override async Task<JsonRpcResponseEnvelope?> AcceptMessageAsync(ITablesAPIServer server)
        {
            return await server.AcceptMessageAsync(this);
        }
    }
}