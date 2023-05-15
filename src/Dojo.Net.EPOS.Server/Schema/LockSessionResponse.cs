using Dojo.Net.EPOS.Server.JsonRpc;

namespace Dojo.Net.EPOS.Server.Schema
{
    public class LockSessionResult
    {
        public required Bill BillItems { get; init; }
    }

    public class LockSessionResponse : JsonRpcResponseEnvelope<LockSessionResult>
    {
        public LockSessionResponse(string id, Bill billItems) : base(id)
        {
            this.Result = new LockSessionResult
            {
                BillItems = billItems
            };
        }
    }
}