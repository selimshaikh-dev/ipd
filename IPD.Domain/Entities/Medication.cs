using System.ComponentModel.DataAnnotations;

namespace IPD.Domain.Entities
{
    /// <summary>
    /// The Medications table holds the name of all medications of the patients
    /// </summary>
    public class Medication : BaseModel
    {
        /// <summary>
        /// Primary key of the table medications.
        /// </summary>
        [Key]
        public Guid MedicationID { get; set; }

        /// <summary>
        /// Name of the medications.
        /// </summary>
        [Required(ErrorMessage = "Medication name is required!")]
        [StringLength(2000)]
        [Display(Name = "Medication name")]
        public string MedicationName { get; set; }
        public virtual IEnumerable<MedicationPlan> ?MedicationPlans { get; set; }
    }
}
