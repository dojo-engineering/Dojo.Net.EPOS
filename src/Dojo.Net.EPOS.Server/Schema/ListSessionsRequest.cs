using Dojo.Net.EPOS.Server.JsonRpc;

namespace Dojo.Net.EPOS.Server.Schema
{
    public class ListSessionsRequestParams
    {
        public bool? IsFinished { get; set; }
        public bool? HasTable { get; set; }
        public bool? IsPayable { get; set; }
        public string[]? TableNames { get; set; }
        public RequestorInfo? RequestorInfo { get; set; }
    }

    /// <summary>
    /// <see href="https://docs.dojo.tech/tables/api/#operation-subscribe-/ListSessions"/>
    /// </summary>
    public class ListSessionsRequest : JsonRpcRequestEnvelope<ListSessionsRequestParams>
    {
        public override async Task<JsonRpcResponseEnvelope?> AcceptMessageAsync(ITablesAPIServer server)
        {
            return  await server.AcceptMessageAsync(this);
        }
    }
}