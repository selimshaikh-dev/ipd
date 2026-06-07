using System.ComponentModel.DataAnnotations;

namespace IPD.Web.Models.DTO
{
    public class UserRegistration
    {
        public Guid UserAccountID { get; set; }
        
        public string NationalID { get; set; } = null!;
       
        public string FirstName { get; set; } = null!;
       
        public string? MiddleName { get; set; }
       
        public string LastName { get; set; } = null!;
       
        public DateTime DOB { get; set; } = DateTime.Now;
      
        public int Sex { get; set; }
       
        public string CellphoneCountryCode { get; set; } = null!;
       
        public string Cellphone { get; set; } = null!;
      
        public string? LandPhoneCountryCode { get; set; }
       
        public string? LandPhone { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
        ErrorMessage = "Invalid Email format")]
        [StringLength(60)]
        [Display(Name = "Email")]
        public string? Email { get; set; }
        public string? BaseUrl { get; set; }
       
        public string ContactAddress { get; set; } = null!;
       
        public string Username { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The new password and confirmation password do not match!")]
        public string ConfirmPassword { get; set; } = null!;
      
        public int FacilityID { get; set; }
       
        public Boolean IsAdministrator { get; set; }
       
        public Boolean IsAccountActive { get; set; }
    }
}
