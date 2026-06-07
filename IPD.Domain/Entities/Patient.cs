using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPD.Domain.Entities
{
    /// <summary>
    /// Contains details of all registerd patients informations.
    /// </summary>
    public class Patient : BaseModel
    {
        /// <summary>
        /// Primary key of Patients table.
        /// </summary>
        public Guid PatientID { get; set; }

        /// <summary>
        /// Unique Hospital ID.
        /// </summary>
        [Required(ErrorMessage = "The UHID is required!")]
        [StringLength(20)]
        [Display(Name = "UHID")]
        public string UHID { get; set; } = null!;

        /// <summary>
        /// Contains PIN, Code 9, Code 1.
        /// </summary>
        [Required(ErrorMessage = "The National ID is required!")]
        [StringLength(20)]
        [Display(Name = "National ID")]
        public string NationalID { get; set; } = null!;

        /// <summary>
        /// First name of the patient.
        /// </summary>
        [Required(ErrorMessage = "The First Name is required!")]
        [StringLength(30)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = null!;

        /// <summary>
        /// Middle name of the patient.
        /// </summary>
        [StringLength(30)]
        [Display(Name = "Middle Name")]
        public string? MiddleName { get; set; }

        /// <summary>
        /// Last name of the patient.
        /// </summary>
        [Required(ErrorMessage = "The Last Name is required!")]
        [StringLength(30)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = null!;

        /// <summary>
        /// Date of birth of the patient.
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "The Date of Birth is required!")]
        [Display(Name = "Date of birth")]
        [Column(TypeName = "smalldatetime")]
        public DateTime DOB { get; set; }

        /// <summary>
        /// Sex of the patient.
        /// </summary>
        [Required(ErrorMessage = "The Sex is required!")]
        [Display(Name = "Sex")]
        public Byte Sex { get; set; }

        /// <summary>
        /// Marital status of the patient.
        /// </summary>
        [Required(ErrorMessage = "The Marital Status is required!")]
        [Display(Name = "Marital Status")]
        public Byte MaritalStatus { get; set; }

        /// <summary>
        /// Contact address of the patient.
        /// </summary>
        [Required(ErrorMessage = "The Contact Address is required!")]
        [StringLength(500)]
        [Display(Name = "Contact Address")]
        public string ContactAddress { get; set; } = null!;

        /// <summary>
        /// Postal address of the patient.
        /// </summary>
        [StringLength(500)]
        [Display(Name = "Postal Address")]
        public string? PostalAddress { get; set; }

        /// <summary>
        /// Country code of the cellphone.
        /// </summary>
        [Required(ErrorMessage = "The Cellphone Country Code is required!")]
        [StringLength(3)]
        [Display(Name = "Cellphone Country Code")]
        public string CellphoneCountryCode { get; set; } = null!;

        /// <summary>
        /// Cellphone number of the patient.
        /// </summary>
        [Required(ErrorMessage = "The Cellphone is required!")]
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
        /// Land phone number of the patient.
        /// </summary>
        [StringLength(15)]
        [Display(Name = "Land Phone")]
        public string? LandPhone { get; set; }

        /// <summary>
        /// Email address of the patient.
        /// </summary>
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
        ErrorMessage = "Invalid Email format")]
        [StringLength(60)]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        /// <summary>
        /// Describes the patient is deceased or not. True indicates patient is deceased.
        /// </summary>
        [Required(ErrorMessage = "Is Deceased is required!")]
        [Display(Name = "Is Deceased")]
        public bool IsDeceased { get; set; }

        /// <summary>
        /// Date when patient deceased.
        /// </summary>
        
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Deceased Date")]
        [Column(TypeName = "smalldatetime")]
        public DateTime? DateDeceased { get; set; }

        /// <summary>
        ///  Forengn key, Primary key of the table Countries.
        /// </summary>
        [Required(ErrorMessage = "Country is required!")]
        [Display(Name = "Country ID")]
        public int CountryID { get; set; }
        [ForeignKey("CountryID")]
        public virtual Country? Countries { get; set; }

        /// <summary>
        /// Forengn key, Primary key of the table Chiefdoms.
        /// </summary>
        [Required(ErrorMessage = "Chiefdom is required!")]
        [Display(Name = "Chiefdom ID")]
        public int ChiefdomID { get; set; }
        [ForeignKey("ChiefdomID")]
        public virtual Chiefdom? Chiefdoms { get; set; }
        public virtual List<Admission>? Admissions { get; set; }
    }
}
