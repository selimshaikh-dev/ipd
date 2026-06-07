using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPD.Domain.Entities
{
    /// <summary>
    /// The MedicationPlans table holds  the Patients medication plan detail of the patients
    /// </summary>
    public class MedicationPlan : BaseModel
    {
        /// <summary>
        /// Primary key of the table MedicationPlans.
        /// </summary>
        [Key]
        public Guid MedicationPlanID { get; set; }

        /// <summary>
        /// Description the dose of medications.
        /// </summary>
        [Required(ErrorMessage = "Dose is required!")]
        [StringLength(90)]
        [Display(Name = "Dose")]
        public string Dose { get; set; }

        /// <summary>
        /// Duration the dose of medications.
        /// </summary>
        [Required(ErrorMessage = "Durations is required!")]
        [StringLength(90)]
        [Display(Name = "Durations")]
        public string Durations { get; set; }

        /// <summary>
        /// Foreign key, Primary key of the Medications table.
        /// </summary>
        [ForeignKey("MedicationsID")]
        public Guid MedicationsID { get; set; }

        /// <summary>
        /// Foreign key, Primary key of the Intervals table.
        /// </summary>
        [ForeignKey("IntervalsID")]
        public Guid IntervalsID { get; set; }

        /// <summary>
        /// Foreign key, Primary key of the directions table.
        /// </summary>
        [ForeignKey("DirectionsID")]
        public Guid DirectionsID { get; set; }

        [ForeignKey("PrescriptionsID")]
        public Guid PrescriptionsID { get; set; }
        public virtual Direction Directions { get; set; }
        public virtual Interval Intervals { get; set; }
        public virtual Medication Medications { get; set; }
        public virtual Prescription Prescriptions { get; set; }
    }
}