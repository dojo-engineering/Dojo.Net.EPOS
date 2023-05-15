using Dojo.Net.EPOS.Server.JsonRpc;

namespace Dojo.Net.EPOS.Server.Schema
{
    public class UnlockSessionResult
    {
    }

    public class UnlockSessionResponse: JsonRpcResponseEnvelope<UnlockSessionResult>
    {
        public UnlockSessionResponse(string id) : base(id)
        {
            this.Result = new UnlockSessionResult();
        }
    }
}