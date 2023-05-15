using Dojo.Net.EPOS.Server.Schema;

namespace Dojo.Net.EPOS.Server
{
    public interface ITablesAPIServer
    {
        /// <summary>
        /// Accepts a <c>ListSessions</c> request message from the client and returns a response.
        /// </summary>
        /// <note>
        /// A request from Dojo for a list of sessions. If there are filtering params specified, 
        /// the response from the method should contain only sessions matching all of the filters. 
        /// If no sessions match the filters, the method should return a <c>ListSessionsResponse</> with no sessions.
        /// </note>
        /// <param name="request">The request message. <see href="https://docs.dojo.tech/tables/api/#operation-subscribe-/ListSessions"/></param>
        /// <returns>The response message.</returns>
        Task<ListSessionsResponse> AcceptMessageAsync(ListSessionsRequest request);
        /// <summary>
        /// Accepts a <c>GetBillItems</c> request message from the client and returns a response.
        /// </summary>
        /// <note>
        /// A request from Dojo for a list of bill items for a session.
        /// </note>
        /// <param name="request">The request message. <see href="https://docs.dojo.tech/tables/api/#operation-subscribe-/GetBillItems"/> </param>
        /// <returns>The response message.</returns>
        Task<GetBillItemsResponse> AcceptMessageAsync(GetBillItemsRequest request);
        /// <summary>
        /// Accepts a <c>GetFullBill</c> request message from the client and returns a response.
        /// </summary>
        /// <note>
        /// A request from Dojo for an itemised bill receipt. 
        /// If the session does not exist, the method should throw the <c>TablesException</c> with <c>SessionNoSuchSession</c> error code.
        /// <example>throw new TablesException(TablesErrorCode.SessionNoSuchSession, "reason")</example>. 
        /// If the bill does not exist, the method should throw the <c>TablesException</c> with <c>BillNoSuchBill</c> error code.
        /// Otherwise, the method should return a GetFullBill response for the session.
        /// </note>
        /// <param name="request">The request message. <see href="https://docs.dojo.tech/tables/api/#operation-subscribe-/GetFullBill"/></param>
        /// <returns>The response message.</returns>
        Task<GetFullBillResponse> AcceptMessageAsync(GetFullBillRequest request);
        /// <summary>
        /// Accepts a <c>GetSession</c> request message from the client and returns a response.
        /// <see href="https://docs.dojo.tech/tables/api/#operation-subscribe-/GetSession"/>
        /// </summary>
        /// <note>
        /// A request from Dojo for a session. 
        /// If the session does not exist, the method should throw the <c>TablesException</c> with <c>SessionNoSuchSession</c> error code.
        /// otherwise return a GetSession response.
        /// </note>
        /// <param name="request">The request message. </param>
        /// <returns>The response message.</returns> 
        /// <exception cref="TablesException">If the session does not exist, the method should throw the <c>TablesException</c> with <c>SessionNoSuchSession</c> error code.</exception>           
        Task<GetSessionResponse> AcceptMessageAsync(GetSessionRequest request);
        /// <summary>
        /// Accepts a <c>GetTable</c> request message from the client and returns a response.
        /// </summary>
        /// <note>
        /// A request from Dojo for a table. If the table doesn't exist, the method should throw the <c>TablesException</c> with <c>SessionNoSuchSession</c> error code.
        /// </note>
        /// <param name="request">The request message. <see href="https://docs.dojo.tech/tables/api/#operation-subscribe-/GetTable"/></param>
        /// <returns>The response message.</returns>
        Task<GetTableResponse> AcceptMessageAsync(GetTableRequest request);
        /// <summary>
        /// Accepts a <c>ListBillItems</c> request message from the client and returns a response.
        /// </summary>
        /// <note>
        /// A request from Dojo for multiple bills. 
        /// The method should return a list of the bills for sessions specified. If no bills can be found, or the specified sessions don't exist, 
        /// the method should return a ListBillItems response with no bill itms. 
        /// Omitting sessionIds returns all bill items.
        /// </note>
        /// <param name="request">The request message. <see href="https://docs.dojo.tech/tables/api/#operation-subscribe-/ListBillItems"/></param>
        /// <returns>The response message.</returns>
        Task<ListBillItemsResponse> AcceptMessageAsync(ListBillItemsRequest request);
        /// <summary>
        /// Accepts a <c>ListTables</c> request message from the client and returns a response.
        /// </summary>
        /// <note>
        /// A request for a list of tables. If there are params set, the ListTables response should contain only tables matching all of the filters using AND logic.
        /// If no tables match the filters, the method should return a ListTables response with no tables.
        /// </note>
        /// <param name="request">The request message. <see href="https://docs.dojo.tech/tables/api/#operation-subscribe-/ListTables"/></param>
        /// <returns>The response message.</returns>
        Task<ListTablesResponse> AcceptMessageAsync(ListTablesRequest request);
        /// <summary>
        /// Accepts a <c>LockSession</c> request message from the client and returns a response.
        /// </summary>
        /// <note>
        /// A request from Dojo to lock a specific session. 
        /// If the session does not exist, the method should throw the <c>TablesException</c> with <c>SessionNoSuchSession</c> error code.
        /// If it is already locked, the method should throw the <c>TablesException</c> with <c>SessionAlreadyLocked</c> error code.
        /// Otherwise, the method should return a LockSession response for the session.
        /// </note>
        /// <param name="request">The request message. <see href="https://docs.dojo.tech/tables/api/#operation-subscribe-/LockSession"/></param>
        /// <returns>The response message.</returns>
        Task<LockSessionResponse> AcceptMessageAsync(LockSessionRequest request);
        /// <summary>
        /// Accepts a <c>UnlockSession</c> request message from the client and returns a response.
        /// </summary>
        /// <note>
        /// A request from Dojo to unlock a locked session. 
        /// If the session does not exist, the method should throw the <c>TablesException</c> with <c>SessionNoSuchSession</c> error code.
        /// If the session is not locked, the method should throw the <c>TablesException</c> with <c>SessionNotLocked</c> error code.
        /// If the EPOS cannot unlock the session due to internal reasons, the method should throw the <c>TablesException</c> with <c>SessionUnableToUnlock</c> error code.
        /// Otherwise, the method should return an UnlockSession response.
        /// </note>
        /// <param name="request">The request message. <see href="https://docs.dojo.tech/tables/api/#operation-subscribe-/UnlockSession"/></param>
        /// <returns>The response message.</returns>
        Task<UnlockSessionResponse> AcceptMessageAsync(UnlockSessionRequest request);
        /// <summary>
        /// Accepts a <c>RecordPaymentRequest</c> request message from the client and returns a response.
        /// </summary>
        /// <note>
        /// A request from Dojo to record a payment. 
        /// If the session does not exist, the method should throw the <c>TablesException</c> with <c>SessionNoSuchSession</c> error code.
        /// If the session connected to the payment is not locked, the method should throw the <c>TablesException</c> with <c>SessionNotLocked</c> error code.
        /// If this particular payment has already been recorded, the method should throw the <c>TablesException</c> with <c>PaymentAlreadyRecorded</c> error code.
        /// If the EPOS cannot record the payment for internal reasons, the method should throw the <c>TablesException</c> with <c>PaymentNotRecorded</c> error code.
        /// If the request is successful, the method should return a RecordPayment response.
        /// </note>
        /// <param name="request">The request message. <see href="https://docs.dojo.tech/tables/api/#operation-subscribe-/RecordPayment"/></param>
        /// <returns>The response message.</returns>
        Task<RecordPaymentResponse> AcceptMessageAsync(RecordPaymentRequest request);
        /// <summary>
        /// Accepts a <c>ErrorNotification</c> request message from the client and returns a response.
        /// </summary>
        /// <note>
        /// Error notification will be sent by Dojo whenever the message sent by the EPOS can't be read.
        /// </note>
        /// <param name="request">The request message. <see href="https://docs.dojo.tech/tables/api/#operation-subscribe-/ErrorNotification"/></param>
        /// <returns>No response.</returns>
        Task AcceptMessageAsync(ErrorNotificationRequest request);
    }
}