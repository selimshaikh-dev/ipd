using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPD.Domain.Entities
{
    /// <summary>
    /// Holds information about the access rights to different modules of a user account. 
    /// </summary>
    public class UserRight : BaseModel
    {
        /// <summary>
        /// Primary key of the table UserRights.
        /// </summary>
        public Guid UserRightID { get; set; }

        /// <summary>
        /// Module Id
        /// </summary>
        [Required(ErrorMessage = " Module is required!")]
        [Display(Name = " Module")]
        public Byte Module { get; set; }

        /// <summary>
        /// Foreignkey, referancing the UserAccounts table.
        /// </summary>
        [ForeignKey("UserAccountID")]
        public Guid UserAccountID { get; set; }

        /// <summary>
        /// Instance of UserAccounts Table.
        /// </summary>
        public virtual UserAccount? UserAccounts { get; set; }
    }
}

