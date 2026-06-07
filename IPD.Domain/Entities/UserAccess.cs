using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static IPD.Domain.Constants.Enumerators;

namespace IPD.Domain.Entities
{
    [Table("UserAccess")]
    public class UserAccess
    {
        [Key]
        public Guid UserAccessID { get; set; }

        [Required(ErrorMessage = "Required!")]
        public byte Module { get; set; }
        public Guid UserAccountID { get; set; }

        [ForeignKey("UserAccountID")]
        public virtual UserAccount? UserAccount { get; set; }
        public RowSyncStatus? SyncStatus { get; set; }
    }
}
