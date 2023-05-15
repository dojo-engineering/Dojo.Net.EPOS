namespace Dojo.Net.EPOS.Server.Schema
{
    public class RequestorInfo
    {
        public string? RequestorType { get; set; }

        public CardMachineRequestorInfo? CardMachineRequestorInfo { get; set; }
        public ConsumerDeviceRequestorInfo? ConsumerDeviceRequestorInfo { get; set; }
    }
}