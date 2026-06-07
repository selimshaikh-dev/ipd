using System.ComponentModel.DataAnnotations;

namespace IPD.Domain.Entities
{
    /// <summary>
    /// The Intervals table holds the interval of medications
    /// </summary>
    public class Interval:BaseModel
    {
        /// <summary>
        /// Primary key of the table Intervals.
        /// </summary>
        [Key]
        public Guid IntervalID { get; set; }

        /// <summary>
        /// Name of the Intervals.
        /// </summary>
        [Required(ErrorMessage = "Interval name is required!")]
        [StringLength(2000)]
        [Display(Name = "Interval name")]
        public string IntervalName { get; set; }
        public virtual IEnumerable<MedicationPlan>? MedicationPlans { get; set; }
    }
}
