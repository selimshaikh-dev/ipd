using static IPD.Domain.Constants.Enumerators;

namespace IPD.Domain.Dto
{
    public class UserAccessDto
    {
        public Guid UserAccessID { get; set; }
        public byte Module { get; set; }
        public Guid UserAccountID { get; set; }
        public RowSyncStatus? SyncStatus { get; set; }
    }
}
