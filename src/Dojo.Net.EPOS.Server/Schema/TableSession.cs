using Newtonsoft.Json;

namespace Dojo.Net.EPOS.Server.Schema
{
    public class TableSession
    {
        public required Guid Id { get; init; }
        
        public required string Name { get; init; }
        
        public string? TableName { get; set; }
        
        public Waiter? Waiter {get;set;}
        
        public int? NumberOfCovers { get; set; }
        
        public required DateTime CreatedAt {get; init;}
        
        public DateTime? FinishedAt {get;set;}

        public required bool IsPayable {get;init;}
    }
}