using System.Runtime.Serialization;

namespace Dojo.Net.EPOS.Server.Schema
{
    public enum PaymentMethod 
    {
        [EnumMember(Value = "PAYMENT_METHOD_REMOTE")]
        Remote,
        [EnumMember(Value = "PAYMENT_METHOD_CARD_PRESENT")]
        CardPresent
    }

    public enum RemotePaymentStatus
    {
        [EnumMember(Value = "REMOTE_PAYMENT_STATUS_SUCCESSFUL")]
        Successful,
        [EnumMember(Value = "REMOTE_PAYMENT_STATUS_UNKNOWN")]
        Unknown,
        [EnumMember(Value = "REMOTE_PAYMENT_STATUS_CANCELLED")]
        Cancelled
    }

    public enum CardPresentPaymentStatus
    {
        [EnumMember(Value = "CARD_PRESENT_PAYMENT_STATUS_SUCCESSFUL")]
        Successful,
        [EnumMember(Value = "CARD_PRESENT_PAYMENT_STATUS_UNKNOWN")]
        Unknown,
        [EnumMember(Value = "CARD_PRESENT_PAYMENT_STATUS_DECLINED")]
        Declined,
        [EnumMember(Value = "CARD_PRESENT_PAYMENT_STATUS_CANCELLED")]
        Cancelled
    }

    public enum RemoteVerificationMethod
    {
        [EnumMember(Value = "REMOTE_VERIFICATION_METHOD_3DS2")]
        ThreeDS2,
        [EnumMember(Value = "REMOTE_VERIFICATION_METHOD_UNKNOWN")]
        Unknown
    }
    public class ExpiryDate
    {
        public int Month { get; set; }
        public int Year { get; set; }
    }

    public class Card
    {
        public string? Scheme { get; set; }
        public string? Last4PAN { get; set; }
        public string? FundingType { get; set; }
        public ExpiryDate? ExpiryDate { get; set; }
    }

    public class RemotePaymentInfo
    {
        public string? AcquirerTransactionId { get; set; }
        public string? AuthCode { get; set; }
        public RemoteVerificationMethod RemoteVerificationMethod { get; set; }
        public string? MerchantId { get; set; }
        public Card? Card { get; set; }
    }

    public class PaymentMethodDetails
    {
        public PaymentMethod Method { get; set; }
        public RemotePaymentStatus? RemotePaymentStatus { get; set; }        
        public RemotePaymentInfo? RemotePaymentInfo { get; set; }
        public CardPresentPaymentStatus? CardPresentPaymentStatus { get; set; }
        public CardPresentPaymentInfo? CardPresentPaymentInfo { get; set; }
    }

    public enum CardholderVerificationMethod
    {
        [EnumMember(Value = "CARDHOLDER_VERIFICATION_METHOD_UNKNOWN")]
        Unknown,
        [EnumMember(Value = "CARDHOLDER_VERIFICATION_METHOD_NONE")]
        None,
        [EnumMember(Value = "CARDHOLDER_VERIFICATION_METHOD_SIGNATURE")]
        Signature,
        [EnumMember(Value = "CARDHOLDER_VERIFICATION_METHOD_DEVICE")]
        Device,
        [EnumMember(Value = "CARDHOLDER_VERIFICATION_METHOD_PIN")]
        Pin
    }

    public enum EntryMode
    {
        [EnumMember(Value = "ENTRY_MODE_UNKNOWN")]
        Unknown,
        [EnumMember(Value = "ENTRY_MODE_MANUAL_ENTRY")]
        ManualEntry,
        [EnumMember(Value = "ENTRY_MODE_MAGSTRIPE")]
        Magstripe,
        [EnumMember(Value = "ENTRY_MODE_CONTACT_CHIP")]
        ContactChip,
        [EnumMember(Value = "ENTRY_MODE_CONTACTLESS")]
        Contactless
    }

    public class CardPresentPaymentInfo
    {
        public string? AcquirerTransactionId { get; set; }
        public string? AuthCode { get; set; }
        public EntryMode EntryMode { get; set; }       
        public Card? Card { get; set; }
        public string? MerchantId { get; set; }
        public CardholderVerificationMethod CardholderVerificationMethod { get; set; }
        public string? TerminalId { get; set; }
    }

    public class Payment
    {
        public Guid Id { get; set; }
        public Guid SessionId { get; set; }
        public int WaiterId { get;set; }
        public int BaseAmount { get; set; }
        public int GratuityAmount { get; set; }
        public int CashbackAmount { get; set; }
        public string? Currency { get; set; }
        public bool PaymentSuccessful { get; set; }
        public DateTime AttemptedAt { get; set; }
        public PaymentMethodDetails? MethodDetails { get; set; }
    }
}