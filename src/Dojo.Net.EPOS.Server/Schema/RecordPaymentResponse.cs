using Dojo.Net.EPOS.Server.JsonRpc;

namespace Dojo.Net.EPOS.Server.Schema
{
    public class RecordPaymentResult
    {
    }

    public class RecordPaymentResponse: JsonRpcResponseEnvelope<RecordPaymentResult>
    {
        public RecordPaymentResponse(string id) : base(id)
        {
            this.Result = new RecordPaymentResult();
        }
    }
}