using System.ComponentModel.DataAnnotations;

namespace IPD.Domain.Entities
{
    /// <summary>
    /// The directions table holds the direction of medications
    /// </summary>
    public class Direction : BaseModel
    {
        /// <summary>
        /// Primary key of the table directions.
        /// </summary>
        [Key]
        public Guid DirectionID { get; set; }

        /// <summary>
        /// Direction details of the direction.
        /// </summary>
        [Required(ErrorMessage = "Direction details is required!")]
        [StringLength(2000)]
        [Display(Name = "Direction details")]
        public string DirectionDetails { get; set; }
        public virtual IEnumerable<MedicationPlan>? MedicationPlans { get; set; }
    }
}
