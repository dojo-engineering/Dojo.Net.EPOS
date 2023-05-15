using Dojo.Net.EPOS.Server.Schema;

namespace Dojo.Net.EPOS.Server
{
    public interface ITablesAPIServer
    {
        Task<ListSessionsResponse> AcceptMessageAsync(ListSessionsRequest request);
        Task<GetBillItemsResponse> AcceptMessageAsync(GetBillItemsRequest request);
        Task<GetFullBillResponse> AcceptMessageAsync(GetFullBillRequest request);
        Task<GetSessionResponse> AcceptMessageAsync(GetSessionRequest request);
        Task<GetTableResponse> AcceptMessageAsync(GetTableRequest request);
        Task<ListBillItemsResponse> AcceptMessageAsync(ListBillItemsRequest request);
        Task<ListTablesResponse> AcceptMessageAsync(ListTablesRequest request);
        Task<LockSessionResponse> AcceptMessageAsync(LockSessionRequest request);
        Task<UnlockSessionResponse> AcceptMessageAsync(UnlockSessionRequest request);
        Task<RecordPaymentResponse> AcceptMessageAsync(RecordPaymentRequest request);
        Task AcceptMessageAsync(ErrorNotificationRequest request);
    }
}