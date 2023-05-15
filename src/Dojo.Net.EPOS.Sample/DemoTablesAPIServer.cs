using Dojo.Net.EPOS.Server.Exceptions;
using Dojo.Net.EPOS.Server.Schema;

namespace Dojo.Net.EPOS.Server
{
    public class DemoTablesAPIServer : ITablesAPIServer
    {
        private readonly List<Bill> _bills = new List<Bill>(){
            new Bill{
                    TotalAmount = 10,
                    TaxAmount = 0,
                    PaidAmount = 10,
                    Currency = "GBP",
                    SessionId = new Guid("11111111-1111-1111-1111-111111111111"),
                    Items = new List<BillItem>() {
                        new BillItem() {
                            Id = "item1",
                            Name = "name1",
                            Quantity = 1,
                            AmountPerItem = 10,
                            LastOrderedAt = DateTime.Now,
                            Category = new List<string>() { "option1", "option2" }
                        },
                        new BillItem() {
                            Id = "item2",
                            Name = "name2",
                            Quantity = 10,
                            AmountPerItem = 2,
                            LastOrderedAt = DateTime.Now,
                            Category = new List<string>() { "option1", "option2" }
                        }
                    }
                },
                new Bill{
                    TotalAmount = 10,
                    TaxAmount = 0,
                    PaidAmount = 10,
                    Currency = "GBP",
                    SessionId = new Guid("22222222-2222-2222-2222-222222222222"),
                    Items = new List<BillItem>() {
                        new BillItem() {
                            Id = "item1",
                            Name = "name1",
                            Quantity = 1,
                            AmountPerItem = 10,
                            LastOrderedAt = DateTime.Now,
                            Category = new List<string>() { "option1", "option2" }
                        },
                        new BillItem() {
                            Id = "item2",
                            Name = "name2",
                            Quantity = 10,
                            AmountPerItem = 2,
                            LastOrderedAt = DateTime.Now,
                            Category = new List<string>() { "option1", "option2" }
                        }
                    }
                }
        };


        private readonly List<TableSession> _sessions = new List<TableSession>(){
            new TableSession
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                Name = "1",
                IsPayable = true,
                CreatedAt = DateTime.Now,
                TableName = "1",
                NumberOfCovers = 1,
                Waiter = new Waiter
                {
                    Id = 1,
                    Name = "waiter 1"
                }
            },
            new TableSession
            {
                Id = new Guid("22222222-2222-2222-2222-222222222222"),
                Name = "2",
                IsPayable = true,
                CreatedAt = DateTime.Now,
                TableName = "2",
                NumberOfCovers = 1,
                Waiter = new Waiter
                {
                    Id = 1,
                    Name = "waiter 1"
                }
            },
            new TableSession
            {
                Id = new Guid("32222222-2222-2222-2222-222222222222"),
                Name = "21",
                IsPayable = true,
                CreatedAt = DateTime.Now.AddHours(-11),
                FinishedAt =  DateTime.Now.AddHours(-10),
                TableName = "21",
                NumberOfCovers = 1,
                Waiter = new Waiter
                {
                    Id = 1,
                    Name = "waiter 1"
                }
            },
            new TableSession
            {
                Id = new Guid("42222222-2222-2222-2222-222222222222"),
                Name = "22",
                IsPayable = false,
                CreatedAt = DateTime.Now.AddHours(-11),
                FinishedAt =  DateTime.Now.AddHours(-10),
                TableName = "22",
                NumberOfCovers = 1,
                Waiter = new Waiter
                {
                    Id = 1,
                    Name = "waiter 1"
                }
            }
        };

        private List<Table> _tables = new List<Table>(){
                new Table(){
                    Name = "table 1",
                    MaxCovers = 10,
                    Status = TableStatus.Available,
                },
                new Table(){
                    Name = "table 11",
                    MaxCovers = 1,
                    Status = TableStatus.Available,
                },
                new Table(){
                    Name = "table 2",
                    MaxCovers = 11,
                    Status = TableStatus.NotInUse,
                },
                new Table(){
                    Name = "table 3",
                    MaxCovers = 11,
                    Status = TableStatus.Occupied,
                },
                new Table(){
                    Name = "table 4",
                    MaxCovers = 11,
                    Status = TableStatus.PendingAvailable,
                }
            };

        private HashSet<Guid> _tableSessionLockIds = new HashSet<Guid>();

        public async Task<ListSessionsResponse> AcceptMessageAsync(ListSessionsRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            await Task.Yield();

            var query = this._sessions.AsQueryable();

            if (request.Params?.TableNames?.Any() == true)
            {
                query = query.Where(x => request.Params.TableNames.Contains(x.TableName));
            }

            if (request.Params?.IsFinished.HasValue == true)
            {
                bool isFinished = request.Params?.IsFinished == true;
                query = query.Where(x => isFinished == (x.FinishedAt != null));
            }

            if (request.Params?.IsPayable.HasValue == true)
            {
                query = query.Where(x => x.IsPayable == request.Params.IsPayable);
            }

            if (request.Params?.HasTable.HasValue == true)
            {
                bool hasTable = request.Params?.HasTable == true;
                query = query.Where(x => hasTable == !string.IsNullOrEmpty(x.TableName));
            }


            ListSessionsResponse response = new ListSessionsResponse(request.Id!, query.ToList());

            return response;
        }

        public async Task AcceptMessageAsync(ErrorNotificationRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            await Task.Yield();

            Console.WriteLine($"ErrorNotificationRequest: {request.Params!.ErrorCode} {request.Params.ErrorReason}");
        }

        public async Task<GetFullBillResponse> AcceptMessageAsync(GetFullBillRequest request)
        {
            if (request == null)
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
                        SessionId = Guid.NewGuid(),
                        Items = new() {
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
            if (request == null)
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
                    SessionId = Guid.NewGuid(),
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
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            await Task.Yield();

            return new GetSessionResponse(request.Id!, new TableSession()
            {
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
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            await Task.Yield();

            return new GetTableResponse(request.Id!, new Table()
            {
                Name = "table 1",
                MaxCovers = 10,
                Status = TableStatus.Available,
            });
        }

        public async Task<ListBillItemsResponse> AcceptMessageAsync(ListBillItemsRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            await Task.Yield();

            var bills = this._bills;
            if (request.Params != null && request.Params.SessionIds != null && request.Params.SessionIds.Any())
            {
                bills = bills.Where(b => request.Params.SessionIds.Contains(b.SessionId)).ToList();
            }

            return new ListBillItemsResponse(request.Id!, bills);
        }

        public async Task<ListTablesResponse> AcceptMessageAsync(ListTablesRequest request)
        {
            if (request == null || request.Params == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            await Task.Yield();

            var query = this._tables.AsQueryable();
            if(request.Params.Statuses?.Any() == true)
            {
                query = query.Where(x => request.Params.Statuses.Contains(x.Status));
            }

            return new ListTablesResponse(request.Id!, query.ToList());
        }

        public async Task<LockSessionResponse> AcceptMessageAsync(LockSessionRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            await Task.Yield();

            var bill = this._bills.Where(x => x.SessionId == request.Params!.SessionId).FirstOrDefault();

            if(bill == null)
            {
                throw new TablesException(TablesErrorCode.SessionNoSuchSession, "Session not found");
            }   

            if(this._tableSessionLockIds.Contains(bill.SessionId))
            {
                throw new TablesException(TablesErrorCode.SessionAlreadyLocked, "Session already locked");
            }
            
            return new LockSessionResponse(request.Id!, bill);
        }

        public async Task<UnlockSessionResponse> AcceptMessageAsync(UnlockSessionRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            await Task.Yield();

            if(!this._tableSessionLockIds.Contains(request.Params!.SessionId))
            {
                throw new TablesException(TablesErrorCode.SessionNotLocked, "Session already locked");
            }

            this._tableSessionLockIds.Remove(request.Params!.SessionId);

            return new UnlockSessionResponse(request.Id!);
        }

        public async Task<RecordPaymentResponse> AcceptMessageAsync(RecordPaymentRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            await Task.Yield();

            return new RecordPaymentResponse(request.Id!);
        }
    }
}