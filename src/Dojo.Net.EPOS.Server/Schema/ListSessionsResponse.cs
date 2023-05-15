using Dojo.Net.EPOS.Server.JsonRpc;
using Newtonsoft.Json;

namespace Dojo.Net.EPOS.Server.Schema
{
    public class ListSessionsResponseResult
    {
        [JsonProperty("sessions")]
        public required List<TableSession> Sessions { get; init; }
    }

    public class ListSessionsResponse : JsonRpcResponseEnvelope<ListSessionsResponseResult>
    {
        public ListSessionsResponse(string id, List<TableSession> sessions) : base(id)
        {
            this.Result = new ListSessionsResponseResult
            {
                Sessions = sessions
            };
        }
    }
}