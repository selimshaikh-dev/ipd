using System.ComponentModel.DataAnnotations;

namespace IPD.Web.Models.DTO
{
    public class EditUser
    {
        public Guid UserAccountID { get; set; }
       
        public string NationalID { get; set; } = null!;
       
        public string FirstName { get; set; } = null!;
       
        public string? MiddleName { get; set; }
       
        public string LastName { get; set; } = null!;
      
        public DateTime DOB { get; set; } = DateTime.Now;
       
        public Byte Sex { get; set; }
       
        public string CellphoneCountryCode { get; set; } = null!;
        
        public string Cellphone { get; set; } = null!;
     
        public string? LandPhoneCountryCode { get; set; }
       
        public string? LandPhone { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
       ErrorMessage = "Invalid Email format")]
        [StringLength(60)]
        [Display(Name = "Email")]

        public string? Email { get; set; }
  
        public string ContactAddress { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int FacilityID { get; set; } = 0;
    }
}
