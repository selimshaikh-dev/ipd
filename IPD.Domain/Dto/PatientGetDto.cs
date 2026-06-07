using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPD.Domain.Dto
{
    public class PatientGetDto
    {
        public Guid PatientID { get; set; }

        [Display(Name = "UHID")]
        public string UHID { get; set; }

        [Required(ErrorMessage = "The National ID is required!")]
        [StringLength(20)]
        [Display(Name = "National ID")]
        public string NationalID { get; set; } = null!;

        [Required(ErrorMessage = "The First Name is required!")]
        [StringLength(30)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = null!;

        [StringLength(30)]
        [Display(Name = "Middle Name")]
        public string? MiddleName { get; set; }

        [Required(ErrorMessage = "The Last Name is required!")]
        [StringLength(30)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = null!;

        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "The Date of Birth is required!")]
        [Display(Name = "Date of birth")]
        [Column(TypeName = "smalldatetime")]
        public DateTime DOB { get; set; }

        [Required(ErrorMessage = "The Sex is required!")]
        [Display(Name = "Sex")]
        public byte Sex { get; set; }
        public string SexName { get; set; } = string.Empty;

        [Required(ErrorMessage = "The Marital Status is required!")]
        [Display(Name = "Marital Status")]
        public byte MaritalStatus { get; set; }
        public string MaritalStatusName { get; set; } = string.Empty;

        [Required(ErrorMessage = "The Contact Address is required!")]
        [StringLength(500)]
        [Display(Name = "Contact Address")]
        public string ContactAddress { get; set; } = null!;

        [StringLength(500)]
        [Display(Name = "Postal Address")]
        public string? PostalAddress { get; set; }

        [Required(ErrorMessage = "The Cellphone Country Code is required!")]
        [StringLength(3)]
        [Display(Name = "Cellphone Country Code")]
        public string CellphoneCountryCode { get; set; } = null!;

        [Required(ErrorMessage = "The Cellphone is required!")]
        [StringLength(15)]
        [Display(Name = "Cellphone")]
        public string Cellphone { get; set; } = null!;

        [StringLength(3)]
        [Display(Name = "LandPhone Country Code")]
        public string? LandPhoneCountryCode { get; set; }

        [StringLength(15)]
        [Display(Name = "Land Phone")]
        public string? LandPhone { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
        ErrorMessage = "Invalid Email format")]
        [StringLength(60)]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Is Deceased is required!")]
        [Display(Name = "Is Deceased")]
        public bool IsDeceased { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Deceased Date")]
        [Column(TypeName = "smalldatetime")]
        public DateTime? DateDeceased { get; set; }
        public DateTime? dateOfBirth { get; set; }

        public int CountryID { get; set; }
        public string CountryName { get; set; }

        [Required(ErrorMessage = "Chiefdom is required!")]
        public int ChiefdomID { get; set; }
        public string ChiefdomName { get; set; }
        public string? baseUrl { get; set; }
        public string? message { get; set; }

        [Required(ErrorMessage = "Tinkhundla is required!")]
        public int TinkhundlaID { get; set; }
        public string TinKhundlaName { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
