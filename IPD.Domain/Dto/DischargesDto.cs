using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPD.Domain.Dto
{
    public class DischargesDto
    {
        public Guid DischargeID { get; set; }

        [Required(ErrorMessage = "The Discharge date is required!")]
        [Column(TypeName = "SmallDateTime")]
        [Display(Name = "DischargeDate")]
        public DateTime DischargeDate { get; set; }

        [Required(ErrorMessage = "The Discharge time is required!")]
        [Column(TypeName = "SmallDateTime")]
        [Display(Name = "Discharge Time")]
        public DateTime DischargeTime { get; set; }

        [Required(ErrorMessage = "The Advice is required!")]
        [StringLength(5000)]
        [Display(Name = "Advice")]
        public string Advice { get; set; }

        [Required(ErrorMessage = "The diet nutrition advice is required!")]
        [StringLength(5000)]
        [Display(Name = "Diet Nutrition Advice")]
        public string DietNutritionAdvice { get; set; }

        [Required(ErrorMessage = "The Medication advice is required!")]
        [StringLength(5000)]
        [Display(Name = "Medication advice")]
        public string MedicationAdvice { get; set; }

        [Required(ErrorMessage = "The Remarks is required!")]
        [StringLength(5000)]
        [Display(Name = "Remarks")]
        public string Remarks { get; set; }

        [Required(ErrorMessage = "The Final diagnosis is required!")]
        [StringLength(5000)]
        [Display(Name = "Final Diagnosis")]
        public string FinalDiagnosis { get; set; }
        public Guid DischargeStatusID { get; set; }
        public Guid AdmissionID { get; set; }
        public string? FacilityCode { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
