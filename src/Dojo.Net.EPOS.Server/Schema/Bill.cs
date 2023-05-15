using Newtonsoft.Json;

namespace Dojo.Net.EPOS.Server.Schema
{
    public class Bill
    {
        public required int TotalAmount { get; init; }
        
        public int ServiceCharge { get; set; }
        
        public required int TaxAmount { get; init; }
        
        public required int PaidAmount { get; init; }
        
        public required string Currency { get; init; }
        
        public required List<BillItem> Items { get; init; }
        
        public string? Note { get; set; }
        
        public required string SessionId { get; init; }
    }

    public class BillItemModifier 
    {
        public required string Id { get; init; }
        
        public required string Name { get; init; }
        
        public required int AmountPerModifier { get; init; }
        
        public required int Quantity { get; init; }
    }

    public class BillItem
    {
        public required string Id { get; init; }
        
        public required string Name { get; init; }
        
        public required List<string> Category { get; init; }
        
        public required int Quantity { get; init; }
        
        public required int AmountPerItem { get; init; }
        
        public required DateTime LastOrderedAt { get; init; }

        public List<BillItemModifier>? Modifiers { get; set; }
    }
}