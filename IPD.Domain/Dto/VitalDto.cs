using System.ComponentModel.DataAnnotations;

namespace IPD.Domain.Dto
{
    public class VitalDto
    {
        public Guid VitalID { get; set; }

        [Required(ErrorMessage = "Date collected is required.")]
        [Display(Name = "Date collected")]
        public DateTime DateCollected { get; set; }

        [Required(ErrorMessage = "Time collected is required.")]
        [Display(Name = "Time collected")]
        public DateTime TimeCollected { get; set; }

        [Required(ErrorMessage = "Weight is required.")]
        [Display(Name = "Weight")]
        public decimal Weight { get; set; }

        [Display(Name = "Height")]
        public decimal? Height { get; set; }

        [Required(ErrorMessage = "Temperature is required.")]
        public decimal Temperature { get; set; }

        [Display(Name = "MUAC")]
        public decimal? MUAC { get; set; }

        [Required(ErrorMessage = "Systolic is required.")]
        [Display(Name = "Systolic")]
        public short Systolic { get; set; }

        [Required(ErrorMessage = "Diastolic is required.")]
        [Display(Name = "Diastolic")]
        public short Diastolic { get; set; }

        [Display(Name = "Respiratory rate")]
        public short? RespiratoryRate { get; set; }

        [Required(ErrorMessage = "Pulse is required.")]
        [Display(Name = "Pulse")]
        public short Pulse { get; set; }

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
        public Guid AdmissionID { get; set; }
        public DateTime? DateCreated { get; set; }
        public string? FacilityName { get; set; }
        public int? Age { get; set; }
    }
}
