namespace Dojo.Net.EPOS.Server.Schema
{
    public class GetSessionResult
    {
        public required TableSession Session { get; init; }
    }

    public class GetSessionResponse : JsonRpc.JsonRpcResponseEnvelope<GetSessionResult>
    {
        public GetSessionResponse(string id, TableSession session) : base(id)
        {
            this.Result = new GetSessionResult
            {
                Session = session
            };
        }
    }
}