using Dojo.Net.EPOS.Server.Exceptions;
using Dojo.Net.EPOS.Server.JsonRpc;
using Dojo.Net.EPOS.Server.Schema;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Dojo.Net.EPOS.Server.Serialization
{
    public class JsonRpcEnvelopeConverter : JsonConverter
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeof(JsonRpcRequestEnvelope).IsAssignableFrom(typeToConvert);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            JObject item = JObject.Load(reader);

            if(!item.TryGetValue("method", out JToken? methodToken))
            {
                throw new TablesException(TablesErrorCode.ErrorParseError, "Invalid JSON-RPC request: missing method.");
            }

            string type = methodToken!.Value<string>()!;
            object? result = null;
            switch(type)
            {
                case "ListSessions":
                    result = new ListSessionsRequest();       
                    break;       
                case "ErrorNotification":
                    result = new ErrorNotificationRequest();
                    break; 
                case "GetBillItems":
                    result = new GetBillItemsRequest();
                    break;
                case "GetFullBill":
                    result = new GetFullBillRequest();
                    break;
                case "GetSession":
                    result = new GetSessionRequest();
                    break;
                case "GetTable":
                    result = new GetTableRequest();
                    break;
                case "ListBillItems":
                    result = new ListBillItemsRequest();
                    break;
                case "ListTables":
                    result = new ListTablesRequest();
                    break;
                case "LockSession":
                    result = new LockSessionRequest();
                    break;
                case "UnlockSession":
                    result = new UnlockSessionRequest();
                    break;
                case "RecordPayment":
                    result = new RecordPaymentRequest();
                    break;
                default:
                    throw new TablesException(TablesErrorCode.ErrorParseError, $"Invalid JSON-RPC request: unknown method '{type}'.");
            }

            serializer.Populate(item.CreateReader(), result!);
            return result!;
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            throw new NotImplementedException("This method is not needed for deserialization.");
        }
    }
}