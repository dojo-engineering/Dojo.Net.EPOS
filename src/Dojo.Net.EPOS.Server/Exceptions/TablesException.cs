using System.Runtime.Serialization;

namespace Dojo.Net.EPOS.Server.Exceptions
{
    public enum TablesErrorCode
    {
        [EnumMember(Value = "SESSION_NO_SUCH_SESSION")]
        SessionNoSuchSession,
        [EnumMember(Value = "SESSION_NOT_LOCKED")]
        SessionNotLocked,
        [EnumMember(Value = "SESSION_ALREADY_LOCKED")]
        SessionAlreadyLocked,        
        [EnumMember(Value = "SESSION_UNABLE_TO_UNLOCK")]
        SessionUnableToUnlock,        
        [EnumMember(Value = "PAYMENT_NOT_RECORDED")]
        PaymentNotRecorded,        
        [EnumMember(Value = "PAYMENT_ALREADY_RECORDED")]
        PaymentAlreadyRecorded,        
        [EnumMember(Value = "ERROR_PARSE_ERROR")]
        ErrorParseError,        
        [EnumMember(Value = "ERROR_INTERNAL_POS_ERROR")]
        ErrorInternalPosError,
        [EnumMember(Value = "TABLE_NO_SUCH_TABLE")]
        TableNoSuchTable,
        [EnumMember(Value = "BILL_NO_SUCH_BILL")]
        BillNoSuchBill,
        [EnumMember(Value = "WAITER_INCORRECT_WAITER_ID")]
        WaiterIncorrectWaiterId,
    }


    public class TablesException : Exception
    {
        public TablesErrorCode ErrorCode { get; }

        public TablesException()
        {
        }

        public TablesException(TablesErrorCode errorCode, string message) : base(message)
        {
            this.ErrorCode = errorCode;
        }

        public TablesException(TablesErrorCode errorCode, string message, Exception innerException) : base(message, innerException)
        {
            this.ErrorCode = errorCode;
        }

        protected TablesException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}