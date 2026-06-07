using System.ComponentModel.DataAnnotations;

namespace IPD.Domain.Dto
{
    /// <summary>
    /// Contains details of change password.
    /// </summary>
    public class ChangedPasswordDto
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match!")]
        public string ConfirmPassword { get; set; }
        public string UserName { get; set; }
        public string? Message { get; set; }
    }
}
