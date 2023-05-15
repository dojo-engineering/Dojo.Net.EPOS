namespace Dojo.Net.EPOS.Server.JsonRpc
{
    public abstract class JsonRpcRequestEnvelope
    {
        public string? Id { get; set; }
        public string? JsonRpc { get; set; }
        public string? Method { get; set; }

        public abstract Task<JsonRpcResponseEnvelope?> AcceptMessageAsync(ITablesAPIServer server);
    }

    public abstract class JsonRpcRequestEnvelope<T> : JsonRpcRequestEnvelope
    {
        public T? Params { get; set; }
    }
}