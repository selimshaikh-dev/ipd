using System.ComponentModel.DataAnnotations;

namespace IPD.Domain.Dto
{
    public class EditClientDto
    {
        public Guid PatientID { get; set; }
        public string UHID { get; set; } = null!;
        public string NationalID { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string? MiddleName { get; set; }
        public string LastName { get; set; } = null!;
        public DateTime DOB { get; set; }
        public byte Sex { get; set; }
        public byte MaritalStatus { get; set; }
        public string ContactAddress { get; set; } = null!;
        public string? PostalAddress { get; set; }
        public string CellphoneCountryCode { get; set; } = null!;
        public string Cellphone { get; set; } = null!;
       
        public string? LandPhoneCountryCode { get; set; }
        public string? LandPhone { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
        ErrorMessage = "Invalid Email format")]
        [StringLength(60)]
        [Display(Name = "Email")]
        public string? Email { get; set; }
        public string? baseUrl { get; set; }
        public int TinkhundlaID { get; set; }
        public int CountryID { get; set; }
        public int ChiefdomID { get; set; }
        public bool IsDeceased { get; set; }
        public DateTime? DateDeceased { get; set; }
        public IEnumerable<ChiefdomDto>? cheifDoms { get; set; }
    }
}
