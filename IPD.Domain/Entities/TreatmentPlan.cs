using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPD.Domain.Entities
{
    /// <summary>
    /// The TreatmentPlans table holds  the patients treatment plan detail of the patients.
    /// </summary>
    public class TreatmentPlan : BaseModel
    {
        /// <summary>
        /// Primary key of the table treatmentplans.
        /// </summary>
        [Key]
        public Guid TreatmentPlanID { get; set; }

        /// <summary>
        /// Treatment plan details of the patients.
        /// </summary>
        [Required(ErrorMessage = "Treatement plan details is required!")]
        [StringLength(1000)]
        [Display(Name = "Treatement plan details")]
        public string TreatementPlanDetails { get; set; }

        /// <summary>
        /// Foreign key, Primary key of the Admissions table.
        /// </summary>
        [ForeignKey("AdmissionID")]
        public Guid AdmissionID { get; set; }
        public virtual Admission Admissions { get; set; }
    }
}
