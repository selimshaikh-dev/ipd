using System.ComponentModel.DataAnnotations;

namespace IPD.Web.Models.DTO
{
    public class DiabeticProfileDto
    {
        public Guid DiabeticProfileID { get; set; }

        [Required(ErrorMessage = "Date collected is required.")]
        [Display(Name = "Date collected")]
        public DateTime DateCollected { get; set; }

        [Required(ErrorMessage = "Time collected is required.")]
        [Display(Name = "Time collected")]
        public DateTime TimeCollected { get; set; }

        [Required(ErrorMessage = "Blood suger is required.")]
        [Range(4, 7.8, ErrorMessage = "Blood suger should be between 4 to 7.8 mmol/L")]
        [Display(Name = "Blood suger")]
        public decimal BloodSuger { get; set; }

        
        [Required(ErrorMessage = "Urin suger is required.")]
        [Range(0, 0.8, ErrorMessage = "Urin suger should be between 0 to 0.8 mmol/L")]
        [Display(Name = "Urin suger")]
        public decimal UrinSuger { get; set; }

        [Required(ErrorMessage = "Urin ketones is required.")]
        [Range(0.6, 2.9, ErrorMessage = "Urin ketones should be between normal range 0.6 to 2.9 mmol/L")]
        [Display(Name = "Urin ketones")]
        public decimal UrinKetones { get; set; }

        [Required(ErrorMessage = "Insulin dose is required.")]
        [Display(Name = "Insulin dose")]
        public decimal InsulinDose { get; set; }

        [StringLength(20)]
        [Display(Name = "Facility code")]
        public string? FacilityCode { get; set; }
        public Guid AdmissionID { get; set; }
        public DateTime? DateCreated { get; set; }
        public string? FacilityName { get; set; }
    }
}
