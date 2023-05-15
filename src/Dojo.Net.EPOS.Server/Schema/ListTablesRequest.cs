using Dojo.Net.EPOS.Server.JsonRpc;

namespace Dojo.Net.EPOS.Server.Schema
{
    public class ListTablesParams
    {
        public List<TableStatus>? Statuses { get; init; }
        public RequestorInfo? RequestorInfo { get; init; }
    }

    public class ListTablesRequest : JsonRpc.JsonRpcRequestEnvelope<ListTablesParams>
    {
        public override async Task<JsonRpcResponseEnvelope?> AcceptMessageAsync(ITablesAPIServer server)
        {
            return await server.AcceptMessageAsync(this);
        }
    }
}