using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPD.Domain.Entities
{
    /// <summary>
    /// Represents postsurgery entity in the database.
    /// </summary>
    public class PostSurgery : BaseModel
    {
        /// <summary>
        /// Primary key of the table postsurgeries.
        /// </summary>
        [Key]
        public Guid PostSurgeryID { get; set; }

        /// <summary>
        /// Description of the conducted operation.
        /// </summary>
        [Required(ErrorMessage = "The Surgery details is required!")]
        [StringLength(1000)]
        [Display(Name = "Surgery Details")]
        public string SurgeryDetails { get; set; } 

        /// <summary>
        /// Finding of the operation.
        /// </summary>
        [Required(ErrorMessage = "The Findings is required!")]
        [StringLength(1000)]
        [Display(Name = "Findings")]
        public string Findings { get; set; } 

        /// <summary>
        /// After surgery paln for managing the patient.
        /// </summary>
        [Required(ErrorMessage = "The Post surgery Plan is required!")]
        [StringLength(1000)]
        [Display(Name = "PostSurgery Plan")]
        public string PostSurgeryPlan { get; set; }

        /// <summary>
        /// Condition of the patient after the surgery (stable, unstable, dead).
        /// </summary>
        [Required(ErrorMessage = " Patients condition is required!")]
        [Display(Name = "Patients Condition")]
        public byte PatientsCondition { get; set; }

        /// <summary>
        /// Foreignkey, primary key of the table surgeries.
        /// </summary>
        [ForeignKey("SurgicalProcedureID")]
        public Guid SurgeryID { get; set; }
        public virtual Surgery Surgeries { get; set; }
    }
}
