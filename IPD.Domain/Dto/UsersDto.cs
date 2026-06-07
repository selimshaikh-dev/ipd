using System.ComponentModel.DataAnnotations;
using static IPD.Domain.Constants.Enumerators;

namespace IPD.Domain.Dto
{
    public class UsersDto
    {
        public Guid UserAccountID { get; set; }

        public string? NationalID { get; set; }

        public string? FirstName { get; set; }

        public string? MiddleName { get; set; }

        public string? LastName { get; set; }

        public DateTime? DOB { get; set; }
        public DateTime? dateOfBirth { get; set; }

        public byte Sex { get; set; }
        public byte Gender { get; set; }

        public string? CellphoneCountryCode { get; set; }

        public string? Cellphone { get; set; }

        public string? LandPhoneCountryCode { get; set; }

        public string? LandPhone { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
        ErrorMessage = "Invalid Email format")]
        [StringLength(60)]
        [Display(Name = "Email")]

        public string? Email { get; set; }

        public string? ContactAddress { get; set; }
        public string? UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string? Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The new password and confirmation password do not match!")]

        public string? ConfirmPassword { get; set; }
        public string? NewPassword { get; set; }
        public int? FacilityID { get; set; }
        public bool? IsAdministrator { get; set; } = true;

        public bool? IsAccountActive { get; set; } = true;

        public string? BaseUrl { get; set; }
        public UserType UserType { get; set; } = UserType.Administrator;
        public List<UserAccessDto>? UserAccess { get; set; }
    }

    public class UserModel
    {
        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }


}
