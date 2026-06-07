using System.ComponentModel.DataAnnotations;

namespace IPD.Web.Models.DTO
{
    public class UserDto
    {
        [Required]
        public int FacilityId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
