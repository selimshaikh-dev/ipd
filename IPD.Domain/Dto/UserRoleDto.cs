using System.ComponentModel.DataAnnotations;
using static IPD.Domain.Constants.Enumerators;

namespace IPD.Domain.Dto
{
    public class UserRoleDto
    {
        public Guid UserID { get; set; }

        [Display(Name = "Name")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Display(Name = "Username")]
        [DataType(DataType.Text)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Required!")]
        [MinLength(5, ErrorMessage = "Password must have atleast 5 characters!")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        //[Required(ErrorMessage = "Required!")]
        [MinLength(5, ErrorMessage = "Password must have atleast 5 characters!")]
        [Compare("Password", ErrorMessage = "Confirmed password does not match!")]
        [Display(Name = "Confirm password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = null!;

        public UserType UserType { get; set; }
    }
}