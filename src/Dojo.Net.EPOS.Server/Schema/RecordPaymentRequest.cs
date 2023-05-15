using Dojo.Net.EPOS.Server.JsonRpc;

namespace Dojo.Net.EPOS.Server.Schema
{
    public class RecordPaymentParams
    {
        public Payment? Payment { get; set; }
        public RequestorInfo? RequestorInfo { get; set; }
    }

    public class RecordPaymentRequest : JsonRpc.JsonRpcRequestEnvelope<RecordPaymentParams>
    {
        public override async Task<JsonRpcResponseEnvelope?> AcceptMessageAsync(ITablesAPIServer server)
        {
            return await server.AcceptMessageAsync(this);
        }
    }
}