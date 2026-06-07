using System.ComponentModel.DataAnnotations;

namespace IPD.Web.Models.DTO
{
    public class VitalsDto
    {
        public Guid VitalID { get; set; }

        [Required(ErrorMessage = "Date collected is required.")]
        [Display(Name = "Date collected")]
        public DateTime DateCollected { get; set; }

        [Required(ErrorMessage = "Time collected is required.")]
        [Display(Name = "Time collected")]
        public DateTime TimeCollected { get; set; }

        [Required(ErrorMessage = "Weight is required.")]
        [Range(2, 200, ErrorMessage = "Weight should be between 2kg and 200kg")]
        [Display(Name = "Weight")]
        public decimal Weight { get; set; }

        [Range(10, 250, ErrorMessage = "Height should be between 10cm and 250cm")]
        [Display(Name = "Height")]
        public decimal? Height { get; set; }

        [Required(ErrorMessage = "Temperature is required.")]
        [Range(20, 60, ErrorMessage = "Temperature should be between 20 and 60 degrees celsius")]
        [Display(Name = "Temperature")]
        public decimal Temperature { get; set; }

        [Range(5, 60, ErrorMessage = "MUAC should be between 5cm and 60cm")]
        [Display(Name = "MUAC")]
        public decimal? MUAC { get; set; }

        [Required(ErrorMessage = "Systolic is required.")]
        [Range(50, 350, ErrorMessage = "Systolic should be between 50mmHg and 350mmHg")]
        [Display(Name = "Systolic")]
        public short Systolic { get; set; }

        [Required(ErrorMessage = "Diastolic is required.")]
        [Range(30, 150, ErrorMessage = "Diastolic should be between 40mmHg and 150mmHg")]
        [Display(Name = "Diastolic")]
        public short Diastolic { get; set; }

        [Range(5, 300, ErrorMessage = "Respiratory rate should be between 5 and 300 breaths per minute")]
        [Display(Name = "Respiratory rate")]
        public short? RespiratoryRate { get; set; }

        [Required(ErrorMessage = "Pulse is required.")]
        [Range(20, 400, ErrorMessage = "Pulse should be between 20bpm and 400bpm")]
        [Display(Name = "Pulse")]
        public short Pulse { get; set; }

        [Range(50, 100, ErrorMessage = "Oxygen saturation should be between 50% and 100%")]
        [Display(Name = "Oxygen saturation")]
        public short? OxygenSaturation { get; set; }

        [Display(Name = "BMI")]
        public decimal? BMI { get; set; }

        [StringLength(90)]
        [Display(Name = "Nutritional status")]
        public string? NutritionalStatus { get; set; }

        [StringLength(1000)]
        [Display(Name = "Other vitals")]
        public string? OtherVitals { get; set; }

        [Display(Name = "Facility code")]
        public string? FacilityCode { get; set; }
        public Guid AdmissionId { get; set; }
        public DateTime? DateCreated { get; set; }
        public string? FacilityName { get; set; }

        public int? Age { get; set; }
    }
}
