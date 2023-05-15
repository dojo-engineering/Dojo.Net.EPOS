using Dojo.Net.EPOS.Server.Schema;

namespace Dojo.Net.EPOS.Server
{
    public class DemoTablesAPIServer : ITablesAPIServer
    {
        public async Task<ListSessionsResponse> AcceptMessageAsync(ListSessionsRequest request)
        {
            if(request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            await Task.Yield();

            ListSessionsResponse response = new ListSessionsResponse(request.Id!, new List<TableSession>(){
                new TableSession
                {
                    Id = Guid.NewGuid(),
                    Name = "table 1",
                    IsPayable = true,
                    CreatedAt = DateTime.Now,
                    TableName = "table x1",
                    NumberOfCovers = 1,
                    Waiter = new Waiter
                    {
                        Id = 1,
                        Name = "waiter 1"
                    }
                }
            });

            return response;
        }

        public async Task AcceptMessageAsync(ErrorNotificationRequest request)
        {
            if(request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            await Task.Yield();

            Console.WriteLine($"ErrorNotificationRequest: {request.Params!.ErrorCode} {request.Params.ErrorReason}");
        }

        public async Task<GetFullBillResponse> AcceptMessageAsync(GetFullBillRequest request)
        {
            if(request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            await Task.Yield();

            GetFullBillResponse response = new(request.Id!,
                new()
                {
                    Header = new()
                    {
                        ReceiptLines = new() {
                            new ReceiptLineText(new ReceiptText(){
                                Value = "header line 1",
                                Size = ReceiptTextSize.Header1
                            })
                        }
                    },
                    BillItems = new()
                    {
                        TotalAmount = 10,
                        TaxAmount = 1,
                        PaidAmount = 10,
                        Currency = "GBP",
                        SessionId = Guid.NewGuid().ToString(),
                        Items = new () {
                            new BillItem() {
                                Id = "item1",
                                Name = "name1",
                                Quantity = 1,
                                AmountPerItem = 10,
                                LastOrderedAt = DateTime.Now,
                                Category = new List<string>() { "option1", "option2" }
                            }
                        }
                    },
                    Footer = new()
                    {
                        ReceiptLines = new() {
                            new ReceiptLineText(new ReceiptText(){
                                Value = "header line 1",
                                Size = ReceiptTextSize.Body
                            })
                        }
                    }
                });

            return response;
        }

        public async Task<GetBillItemsResponse> AcceptMessageAsync(GetBillItemsRequest request)
        {
            if(request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            await Task.Yield();

            return new GetBillItemsResponse(request.Id!,
                new Bill()
                {
                    TotalAmount = 10,
                    TaxAmount = 0,
                    PaidAmount = 10,
                    Currency = "GBP",
                    SessionId = Guid.NewGuid().ToString(),
                    Items = new List<BillItem>() {
                        new BillItem() {
                            Id = "item1",
                            Name = "name1",
                            Quantity = 1,
                            AmountPerItem = 10,
                            LastOrderedAt = DateTime.Now,
                            Category = new List<string>() { "option1", "option2" }
                        }
                    }
                }
            );
        }

        public async Task<GetSessionResponse> AcceptMessageAsync(GetSessionRequest request)
        {
            if(request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            await Task.Yield();

            return new GetSessionResponse(request.Id!, new TableSession() {
                Id = request.Params!.SessionId,
                Name = "table 1",
                IsPayable = true,
                CreatedAt = DateTime.Now,
                TableName = "table x1",
                NumberOfCovers = 1,
                Waiter = new Waiter
                {
                    Id = 1,
                    Name = "waiter 1"
                }
            });
        }

        public async Task<GetTableResponse> AcceptMessageAsync(GetTableRequest request)
        {
            if(request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            await Task.Yield();

            return new GetTableResponse(request.Id!, new Table(){
                Name = "table 1",
                MaxCovers = 10,
                Status = TableStatus.Available,
            });
        }

        public async Task<ListBillItemsResponse> AcceptMessageAsync(ListBillItemsRequest request)
        {
            if(request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            await Task.Yield();
            
            return new ListBillItemsResponse(request.Id!, new List<Bill> {
                new Bill{
                    TotalAmount = 10,
                    TaxAmount = 0,
                    PaidAmount = 10,
                    Currency = "GBP",
                    SessionId = Guid.NewGuid().ToString(),
                    Items = new List<BillItem>() {
                        new BillItem() {
                            Id = "item1",
                            Name = "name1",
                            Quantity = 1,
                            AmountPerItem = 10,
                            LastOrderedAt = DateTime.Now,
                            Category = new List<string>() { "option1", "option2" }
                        }
                    }
                }
            });
        }

        public async Task<ListTablesResponse> AcceptMessageAsync(ListTablesRequest request)
        {
            if(request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            await Task.Yield();

            return new ListTablesResponse(request.Id!, new List<Table>(){
                new Table(){
                    Name = "table 1",
                    MaxCovers = 10,
                    Status = TableStatus.Available,
                },
                new Table(){
                    Name = "table 2",
                    MaxCovers = 11,
                    Status = TableStatus.Available,
                }
            });
        }

        public async Task<LockSessionResponse> AcceptMessageAsync(LockSessionRequest request)
        {
            if(request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            await Task.Yield();
            return new LockSessionResponse(request.Id!, new()
                    {
                        TotalAmount = 10,
                        TaxAmount = 1,
                        PaidAmount = 10,
                        Currency = "GBP",
                        SessionId = Guid.NewGuid().ToString(),
                        Items = new () {
                            new BillItem() {
                                Id = "item1",
                                Name = "name1",
                                Quantity = 1,
                                AmountPerItem = 10,
                                LastOrderedAt = DateTime.Now,
                                Category = new List<string>() { "option1", "option2" }
                            }
                        }
                    });
        }

        public async Task<UnlockSessionResponse> AcceptMessageAsync(UnlockSessionRequest request)
        {
            if(request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            await Task.Yield();

            return new UnlockSessionResponse(request.Id!);
        }

        public async Task<RecordPaymentResponse> AcceptMessageAsync(RecordPaymentRequest request)
        {
            if(request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            await Task.Yield();
            return new RecordPaymentResponse(request.Id!);
        }
    }
}