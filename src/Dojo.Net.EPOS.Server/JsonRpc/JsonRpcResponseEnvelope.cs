using Newtonsoft.Json;

namespace Dojo.Net.EPOS.Server.JsonRpc
{
    public abstract class JsonRpcResponseEnvelope
    {
        [JsonProperty("jsonrpc")]
        public string? JsonRpc { get; } = "2.0";
        public string Id { get; set; }

        public JsonRpcResponseEnvelope(string id)
        {
            this.Id = id;
        }
    }

    public abstract class JsonRpcResponseEnvelope<T> : JsonRpcResponseEnvelope
        where T : class
    {
        public T Result { get; set;}

        public JsonRpcResponseEnvelope(string id): base(id)
        {
        }
    }
}