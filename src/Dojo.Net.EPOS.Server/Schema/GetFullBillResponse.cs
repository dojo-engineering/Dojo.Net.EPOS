using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Dojo.Net.EPOS.Server.Schema
{
    public enum ReceiptLineType
    {
        [EnumMember(Value = "RECEIPT_LINE_TYPE_LOGO")]
        Logo,
        [EnumMember(Value = "RECEIPT_LINE_TYPE_TEXT")]
        Text,
        [EnumMember(Value = "RECEIPT_LINE_TYPE_MERCHANT_NAME")]
        MerchantName,
        [EnumMember(Value = "RECEIPT_LINE_TYPE_MERCHANT_PHONE_NUMBER")]
        MerchantPhoneNumber,
        [EnumMember(Value = "RECEIPT_LINE_TYPE_MERCHANT_EMAIL_ADDRESS")]
        MerchantEmailAddress,
        [EnumMember(Value = "RECEIPT_LINE_TYPE_MERCHANT_ADDRESS")]
        MerchantAddress,
        [EnumMember(Value = "RECEIPT_LINE_TYPE_HORIZONTAL_LINE")]
        HorizontalLine,
        [EnumMember(Value = "RECEIPT_LINE_TYPE_VAT_NUMBER")]
        VATNumber
    }

    public class ReceiptLogo
    {
        public required string SvgImage { get; init; }
    }

    public enum ReceiptImageAlign
    {
        [EnumMember(Value = "ALIGN_LEFT")]
        Left,
        [EnumMember(Value = "ALIGN_CENTER")]
        Center,
        [EnumMember(Value = "ALIGN_RIGHT")]
        Right
    }

    public class ReceiptImage
    {
        public required string SvgImage { get; init; }

        public ReceiptImageAlign? Align { get; set; }
    }    

    public enum ReceiptTextSize 
    {
        [EnumMember(Value = "SIZE_HEADER_1")]
        Header1,
        [EnumMember(Value = "SIZE_HEADER_2")]
        Header2,
        [EnumMember(Value = "SIZE_BODY")]
        Body
    }

    public enum ReceiptTextAlign 
    {
        [EnumMember(Value = "ALIGN_LEFT")]
        Left,

        [EnumMember(Value = "ALIGN_RIGHT")]
        Right,

        [EnumMember(Value = "ALIGN_CENTER")]
        Center
    }

    public class ReceiptText
    {
        public required string Value { get; init; }

        public required ReceiptTextSize Size { get; init; }

        public ReceiptTextAlign? Align { get; set; }

        public bool? EmphasisBold {get; set;}
    }


    public enum ReceiptHorizontalLineType
    {
        [EnumMember(Value = "HORIZONTAL_LINE_SINGLE")]
        Single,
        [EnumMember(Value = "HORIZONTAL_LINE_DOUBLE")]
        Double
    }

    public class ReceiptHorizontalLine
    {
        public ReceiptHorizontalLineType Type { get; set; }
    }

    public class ReceiptUrl
    {
        public string? Description { get; set; }

        public bool? ShowQR { get; set; }
        public bool? ShowURL { get; set; }
        public required string Url { get; init; } 
    }

    public class ReceiptMerchantAddress
    {
        public required List<string> AddressLines { get; init; }

        public required string Postcode { get; init; }
    }

    public class ReceiptMerchantName
    {
        public required string MerchantName { get; init; }
    }

    public class ReceiptVATNumber
    {
        public required string VATNumber { get; init; }
    }

    public class ReceiptMerchantPhoneNumber
    {
        public required string PhoneNumber { get; init; }
    }

    public class ReceiptMerchantEmailAddress
    {
        public required string EmailAddress { get; set; }
    }

    public interface IReceiptLine
    {
        public ReceiptLineType ReceiptLineType { get; }
    }

    public class ReceiptLineText: IReceiptLine
    {
        public ReceiptLineType ReceiptLineType => ReceiptLineType.Text;

        public ReceiptText ReceiptText { get; }

        public ReceiptLineText(ReceiptText receiptText)
        {
            this.ReceiptText = receiptText;
        }
    }

    public class ReceiptLineImage: IReceiptLine
    {
        public ReceiptLineType ReceiptLineType => ReceiptLineType.Text;

        public ReceiptImage ReceiptImage { get; }

        public ReceiptLineImage(ReceiptImage receiptText)
        {
            this.ReceiptImage = receiptText;
        }
    }

    public class ReceiptLineLogo: IReceiptLine
    {
        public ReceiptLineType ReceiptLineType => ReceiptLineType.Logo;

        public ReceiptLogo ReceiptLogo { get; }

        public ReceiptLineLogo(ReceiptLogo receiptLogo)
        {
            this.ReceiptLogo = receiptLogo;
        }
    }

    public class ReceiptLineHorizontalLine: IReceiptLine
    {
        public ReceiptLineType ReceiptLineType => ReceiptLineType.HorizontalLine;

        public ReceiptHorizontalLine ReceiptHorizontalLine { get; }

        public ReceiptLineHorizontalLine(ReceiptHorizontalLine receiptHorizontalLine)
        {
            this.ReceiptHorizontalLine = receiptHorizontalLine;
        }
    }

    public class ReceiptLineUrl: IReceiptLine
    {
        public ReceiptLineType ReceiptLineType => ReceiptLineType.Text;

        public ReceiptUrl ReceiptUrl { get; }

        public ReceiptLineUrl(ReceiptUrl receiptUrl)
        {
            this.ReceiptUrl = receiptUrl;
        }
    }

    public class ReceiptLineMerchantAddress: IReceiptLine
    {
        public ReceiptLineType ReceiptLineType => ReceiptLineType.MerchantAddress;

        public ReceiptMerchantAddress ReceiptMerchantAddress { get; }

        public ReceiptLineMerchantAddress(ReceiptMerchantAddress receiptMerchantAddress)
        {
            this.ReceiptMerchantAddress = receiptMerchantAddress;
        }
    }

    public class ReceiptLIneMerchantName: IReceiptLine
    {
        public ReceiptLineType ReceiptLineType => ReceiptLineType.MerchantName;

        public ReceiptMerchantName ReceiptMerchantName { get; }

        public ReceiptLIneMerchantName(ReceiptMerchantName receiptMerchantName)
        {
            this.ReceiptMerchantName = receiptMerchantName;
        }
    }

    public class ReceiptLineVATNumber: IReceiptLine
    {
        public ReceiptLineType ReceiptLineType => ReceiptLineType.VATNumber;

        public ReceiptVATNumber ReceiptVATNumber { get; }

        public ReceiptLineVATNumber(ReceiptVATNumber receiptVATNumber)
        {
            this.ReceiptVATNumber = receiptVATNumber;
        }
    }

    public class ReceiptLineMerchantPhoneNumber: IReceiptLine
    {
        public ReceiptLineType ReceiptLineType => ReceiptLineType.MerchantPhoneNumber;

        public ReceiptMerchantPhoneNumber ReceiptMerchantPhoneNumber { get; }

        public ReceiptLineMerchantPhoneNumber(ReceiptMerchantPhoneNumber receiptMerchantPhoneNumber)
        {
            this.ReceiptMerchantPhoneNumber = receiptMerchantPhoneNumber;
        }
    }

    public class ReceiptLineMerchantEmailAddress: IReceiptLine
    {
        public ReceiptLineType ReceiptLineType => ReceiptLineType.MerchantEmailAddress;

        public ReceiptMerchantEmailAddress ReceiptMerchantEmailAddress { get; }

        public ReceiptLineMerchantEmailAddress(ReceiptMerchantEmailAddress receiptMerchantEmailAddress)
        {
            this.ReceiptMerchantEmailAddress = receiptMerchantEmailAddress;
        }
    }

    public class FullBill
    {
        public BillSection? Header { get; set; }

        public required Bill BillItems { get; init; }

        public BillSection? Footer { get; set; }        
    }

    public class BillSection
    {
        public required List<IReceiptLine> ReceiptLines { get; init; }
    }       

    public class GetFullBillResult 
    {
        public required FullBill FullBill { get; init;}
    }

    public class GetFullBillResponse : JsonRpc.JsonRpcResponseEnvelope<GetFullBillResult>
    {
        public GetFullBillResponse(string id, FullBill fullBIll) : base(id)
        {
            this.Result = new GetFullBillResult { FullBill = fullBIll };
        }
    }
}