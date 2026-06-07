using System.ComponentModel.DataAnnotations;

namespace IPD.Web.Models.DTO
{
    public class NursingCareDto
    {
        public Guid NursingCareID { get; set; }

        [Required(ErrorMessage = "Date of care is required.")]
        [Display(Name = "Date of care")]
        public DateTime DateOfCare { get; set; }

        [Required(ErrorMessage = "Time of care is required.")]
        [Display(Name = "Time of care")]
        public DateTime TimeOfCare { get; set; }

        [Required(ErrorMessage = "Problem is required.")]
        [StringLength(1000)]
        [Display(Name = "Problem")]
        public string Problem { get; set; } = null!;

        [Required(ErrorMessage = "Diagnosis is required.")]
        [StringLength(1000)]
        [Display(Name = "Diagnosis")]
        public string Diagnosis { get; set; } = null!;

        [Required(ErrorMessage = "Objective is required.")]
        [StringLength(1000)]
        [Display(Name = "Objective")]
        public string Objective { get; set; } = null!;

        [Required(ErrorMessage = "Intervension is required.")]
        [StringLength(1000)]
        [Display(Name = "Intervension")]
        public string Intervension { get; set; } = null!;

        [StringLength(1000)]
        [Display(Name = "Rational")]
        public string? Rational { get; set; }

        [StringLength(1000)]
        [Display(Name = "Evaluation")]
        public string? Evaluation { get; set; }

        [StringLength(20)]
        [Display(Name = "Facility code")]
        public string? FacilityCode { get; set; }
        public Guid AdmissionId { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
