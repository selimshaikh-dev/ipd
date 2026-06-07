using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static IPD.Domain.Constants.Enumerators;

namespace IPD.Domain.Entities
{
    /// <summary>
    /// Contains details of all registerd CMIS IPD user accounts.
    /// </summary>
    public class UserAccount : BaseModel
    {
        /// <summary>
        /// Primary key of the table UserAccounts.
        /// </summary>
        public Guid UserAccountID { get; set; }

        /// <summary>
        /// Contains PIN, Code 9, Code 1.
        /// </summary>
        [Required(ErrorMessage = "Please enter your National ID!")]
        [StringLength(20)]
        [Display(Name = "National ID")]
        public string NationalID { get; set; } = null!;

        /// <summary>
        /// First name of the clinician.
        /// </summary>
        [Required(ErrorMessage = "The First Name is required!")]
        [StringLength(30)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = null!;

        /// <summary>
        /// Middle name of the clinician.
        /// </summary>
        [StringLength(30)]
        [Display(Name = "Middle Name")]
        public string? MiddleName { get; set; }

        /// <summary>
        /// Last name of the clinician.
        /// </summary>
        [Required(ErrorMessage = "The Last Name is required!")]
        [StringLength(30)]
        [Display(Name = "First Name")]
        public string LastName { get; set; } = null!;

        /// <summary>
        /// Date of birth of the clinician.
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "The Date of Birth is required!")]
        [Display(Name = "Date of birth")]
        [Column(TypeName = "smalldatetime")]
        public DateTime DOB { get; set; } = DateTime.Now;

        /// <summary>
        ///  Sex of the clinician.
        /// </summary>
        [Required(ErrorMessage = "The Sex is required!")]
        [Display(Name = "Sex")]
        public Byte Sex { get; set; }

        /// <summary>
        /// Country code of the cellphone.
        /// </summary>
        [Required(ErrorMessage = "The Cellphone country code is required!")]
        [StringLength(3)]
        [Display(Name = "Cellphone Country Code")]
        public string CellphoneCountryCode { get; set; } = null!;

        /// <summary>
        /// Cellphone number of the clinician.
        /// </summary>
        [Required(ErrorMessage = "Cellphone number is required!")]
        [StringLength(15)]
        [Display(Name = "Cellphone")]
        public string Cellphone { get; set; } = null!;
        /// <summary>
        /// Country code of the land phone.
        /// </summary>
        [StringLength(3)]
        [Display(Name = "LandPhone Country Code")]
        public string? LandPhoneCountryCode { get; set; }

        /// <summary>
        /// Land phone number of the clinician.
        /// </summary>
        [StringLength(15)]
        [Display(Name = "Land Phone")]
        public string? LandPhone { get; set; }

        /// <summary>
        /// Email adress of the clinician.
        /// </summary>
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
        ErrorMessage = "Invalid Email format")]
        [StringLength(60)]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        /// <summary>
        /// Contact address of the clinician.
        /// </summary>
        [Required(ErrorMessage = "The Contact Address is required!")]
        [StringLength(250)]
        [Display(Name = "Contact Address")]
        public string ContactAddress { get; set; } = null!;

        /// <summary>
        /// Username of the clinician's account.
        /// </summary>
        [Required(ErrorMessage = "The Username is required!")]
        [StringLength(30)]
        [Display(Name = "Username")]
        public string Username { get; set; } = null!;

        /// <summary>
        /// Password of the clinician's account.
        /// </summary>
        [Required(ErrorMessage = "The Password is required!")]
        [Display(Name = " Password")]
        [MinLength(8, ErrorMessage = "Password must have atleast 8 characters!")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        /// <summary>
        /// Facility ID, foreignkey.
        /// </summary>
        [ForeignKey("FacilityID")]
        public int FacilityID { get; set; }

        /// <summary>
        /// User type. True indicates the user is an administrator.
        /// </summary>
        [Display(Name = " Is Administrator")]
        public bool? IsAdministrator { get; set; }

        /// <summary>
        /// Describe the user account is active or not. Only active user will be able to login.
        /// </summary>
       
        [Display(Name = "Is AccountActive")]
        public bool? IsAccountActive { get; set; }
        public UserType UserType { get; set; }
        public RowStatus? AccountStatus { get; set; }
        public virtual Facility? Facilities { get; set; }
        public RowSyncStatus? SyncStatus { get; set; }

        /// <summary>
        /// Instance of UserRights Table.
        /// </summary>
        public List<UserRight>? UserRights { get; set; }

        /// <summary>
        /// List creation of RecoveryRequests table.
        /// </summary>
        public List<RecoveryRequest>? RecoveryRequests { get; set; }
        public virtual List<UserAccess>? UserAccess { get; set; }

    }
}
