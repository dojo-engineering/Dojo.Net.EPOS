namespace Dojo.Net.EPOS.Server.Schema
{
    public class ListTablesResult
    {
        public required List<Table> Tables { get; init; }
    }

    public class ListTablesResponse : JsonRpc.JsonRpcResponseEnvelope<ListTablesResult>
    {
        public ListTablesResponse(string id, List<Table> tables) : base(id)
        {
            this.Result = new ListTablesResult
            {
                Tables = tables
            };
        }
    }
}