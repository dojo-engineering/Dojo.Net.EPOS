using Newtonsoft.Json;

namespace Dojo.Net.EPOS.Server.Schema
{
    public class GetBillItemsResult 
    {
        public required Bill BillItems { get; init; }
    }

    public class GetBillItemsResponse : JsonRpc.JsonRpcResponseEnvelope<GetBillItemsResult>
    {
        public GetBillItemsResponse(string id, Bill bill) : base(id)
        {
            this.Result = new GetBillItemsResult
            {
                BillItems = bill
            };
        }
    }
}