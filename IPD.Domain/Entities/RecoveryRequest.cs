using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPD.Domain.Entities
{
    /// <summary>
    /// Holds all the login recovery requests from clinicians.
    /// </summary>
    public class RecoveryRequest : BaseModel
    {
        /// <summary>
        /// Primary key of the table RecoveryRequests.
        /// </summary>
        public Guid RecoveryRequestID { get; set; }
        /// <summary>
        /// Country code of the cellphone.
        /// </summary>
        [Required(ErrorMessage = "The Cellphone Country Code is required!")]
        [StringLength(3)]
        [Display(Name = "Cellphone Country Code")]
        public String CellphoneCountryCode { get; set; } = null!;

        /// <summary>
        /// Cellphone number of the user.
        /// </summary>
        [Required(ErrorMessage = "The Cellphone is required!")]
        [StringLength(15)]
        [Display(Name = "Cellphone")]
        public String Cellphone { get; set; } = null!;

        /// <summary>
        /// Username of the IPD user.
        /// </summary>
        [StringLength(30)]
        [Display(Name = "Username")]
        public String? Username { get; set; }

        /// <summary>
        ///National Id of the user.
        /// </summary>
        [StringLength(20)]
        [Display(Name = "National ID")]
        public String? NationalID { get; set; }

        /// <summary>
        /// Date of recovery request.
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "The Date Requested is required!")]
        [Display(Name = "Date Requested")]
        [Column(TypeName = "smalldatetime")]
        public DateTime DateRequested { get; set; }
        /// <summary>
        /// Describes the recovery request is sorted or not.
        /// </summary>
        [Required(ErrorMessage = "Is TicketOpenr is required!")]
        [Display(Name = " Is TicketOpen")]
        public Boolean IsTicketOpen { get; set; }

        /// <summary>
        ///Foreign key, referance of UserAccounts table.
        /// </summary>
        [ForeignKey("UserAccountID")]
        public Guid UserAccountID { get; set; }
        /// <summary>
        /// Instance of UserAccounts Table.
        /// </summary>
        public virtual UserAccount? UserAccounts { get; set; }

    }
}
