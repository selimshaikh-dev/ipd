using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPD.Domain.Entities
{
    /// <summary>
    /// The Languages table holds the name of the patients allergies
    /// </summary>
    public class PatientAllergy : BaseModel
    {
        /// <summary>
        /// Primary key of the table PatientAllergies.
        /// </summary>
        [Key]
        public Guid PatientAllergiesID { get; set; }

        /// <summary>
        /// Foreign key, Primary key of the Allergies table.
        /// </summary>
        [ForeignKey("AllergiesID")]
        public int AllergiesID { get; set; }

        /// <summary>
        /// Foreign key, Primary key of the Complaints table.
        /// </summary>
        [ForeignKey("ComplaintID")]
        public Guid ComplaintID { get; set; }
        public virtual Complaint Complaints { get; set; }
        public virtual Allergy Allergies { get; set; }
    }
}