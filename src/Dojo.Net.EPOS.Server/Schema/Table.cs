using System.Runtime.Serialization;

namespace Dojo.Net.EPOS.Server.Schema
{
    public enum TableStatus
    {
        [EnumMember(Value = "TABLE_STATUS_NOT_IN_USE")]
        NotInUse,
        [EnumMember(Value = "TABLE_STATUS_PENDING_AVAILABLE")]
        PendingAvailable,
        [EnumMember(Value = "TABLE_STATUS_AVAILABLE")]
        Available,
        [EnumMember(Value = "TABLE_STATUS_OCCUPIED")]
        Occupied
    }

    public class Table
    {
        public required string Name { get; init; }
        public required int MaxCovers { get; init; }
        public required TableStatus Status { get; init; }
    }
}