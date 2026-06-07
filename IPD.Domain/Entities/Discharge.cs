using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPD.Domain.Entities
{
    /// <summary>
    /// Contains details of Discharges.
    /// </summary>
    public class Discharge : BaseModel
    {
        /// <summary>
        /// Primary key of Discharges table.
        /// </summary>
        [Key]
        public Guid DischargeID { get; set; }

        /// <summary>
        /// Date of discharge.
        /// </summary>
        [Required(ErrorMessage = "The Discharge date is required!")]
        [Column(TypeName = "SmallDateTime")]
        [Display(Name = "DischargeDate")]
        public DateTime DischargeDate { get; set; }

        /// <summary>
        /// Time of patient's discharge.
        /// </summary>
        [Required(ErrorMessage = "The Discharge time is required!")]
        [Column(TypeName = "SmallDateTime")]
        [Display(Name = "Discharge Time")]
        public DateTime DischargeTime { get; set; }

        /// <summary>
        /// Advice for the patient made by the clinician during the time of discharge.
        /// </summary>
        [Required(ErrorMessage = "The Advice is required!")]
        [StringLength(5000)]
        [Display(Name = "Advice")]
        public string Advice { get; set; }

        /// <summary>
        /// Diet and nutrition advice for the patient made by the clinician on discharge.
        /// </summary>
        [Required(ErrorMessage = "The diet nutrition advice is required!")]
        [StringLength(5000)]
        [Display(Name = "Diet Nutrition Advice")]
        public string DietNutritionAdvice { get; set; }

        /// <summary>
        /// Medication advice for the patient.
        /// </summary>
        [Required(ErrorMessage = "The Medication advice is required!")]
        [StringLength(5000)]
        [Display(Name = "Medication advice")]
        public string MedicationAdvice { get; set; }

        /// <summary>
        /// Remarks of clinician regarding the overall treatment of the patient. 
        /// </summary>
        [Required(ErrorMessage = "The Remarks is required!")]
        [StringLength(5000)]
        [Display(Name = "Remarks")]
        public string Remarks { get; set; }

        /// <summary>
        /// Final diagnosis of the patient just before the discharge.
        /// </summary>
        [Required(ErrorMessage = "The Final diagnosis is required!")]
        [StringLength(5000)]
        [Display(Name = "Final Diagnosis")]
        public string FinalDiagnosis { get; set; }

        /// <summary>
        /// Foreign key, Primary key of the DischargeStatuses table.
        /// </summary>
        [ForeignKey("DischargeStatusID")]
        public Guid DischargeStatusID { get; set; }

        /// <summary>
        /// Foreign key, Primary key of the Admissions table.
        /// </summary>
        [ForeignKey("AdmissionID")]
        public Guid AdmissionID { get; set; }
        public virtual DischargeStatus? DischargeStatuses { get; set; }
        public virtual Admission Admission { get; set; }
    }
}
