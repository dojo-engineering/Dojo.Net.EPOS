namespace Dojo.Net.EPOS.Server.Schema
{
    public class GetTableResult
    {
        public required Table Table { get; init; }
    }

    public class GetTableResponse : JsonRpc.JsonRpcResponseEnvelope<GetTableResult>
    {
        public GetTableResponse(string id, Table table) : base(id)
        {
            this.Result = new GetTableResult
            {
                Table = table
            };
        }
    }
}