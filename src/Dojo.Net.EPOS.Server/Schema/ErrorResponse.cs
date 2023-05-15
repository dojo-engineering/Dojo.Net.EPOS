using Dojo.Net.EPOS.Server.Exceptions;
using Dojo.Net.EPOS.Server.JsonRpc;

namespace Dojo.Net.EPOS.Server.Schema
{
    public class ErrorResult
    {
        public TablesErrorCode ErrorCode { get; set; }
        public string? ErrorReason { get; set; }
    }

    public class ErrorResponse : JsonRpcResponseEnvelope<ErrorResult>
    {
        public ErrorResponse(string id, TablesErrorCode errorCode, string message) : base(id)
        {
            this.Result = new ErrorResult
            {
                ErrorCode = errorCode,
                ErrorReason = message
            };
        }
    }
}