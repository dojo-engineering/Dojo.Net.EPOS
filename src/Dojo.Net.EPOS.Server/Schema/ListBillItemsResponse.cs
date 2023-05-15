using Dojo.Net.EPOS.Server.JsonRpc;

namespace Dojo.Net.EPOS.Server.Schema
{
    public class ListBillItemsResult
    {
        public required List<Bill> BillItems { get; init; }
    }

    public class ListBillItemsResponse : JsonRpcResponseEnvelope<ListBillItemsResult>
    {
        public ListBillItemsResponse(string id, List<Bill> billItems) : base(id)
        {
            this.Result = new ListBillItemsResult
            {
                BillItems = billItems
            };
        }
    }
}