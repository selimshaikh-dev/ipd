using System.ComponentModel.DataAnnotations;

namespace IPD.Domain.Dto
{
    public class LoginDto
    {
        [Required]
        public int FacilityId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
