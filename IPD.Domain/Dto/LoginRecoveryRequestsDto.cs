using System.ComponentModel.DataAnnotations;

namespace IPD.Domain.Dto
{
    public class LoginRecoveryRequestsDto
    {
        public string? UserName { get; set; }
        public string? NationaliD { get; set; }

        [Required(ErrorMessage = "Cellphone number is a required field!")]
        [StringLength(15)]
        [Display(Name = "Cellphone")]
        public string CellPhone { get; set; }
    }
}
