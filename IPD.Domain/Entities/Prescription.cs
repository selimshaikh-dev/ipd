using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPD.Domain.Entities
{
    public class Prescription : BaseModel
    {
        [Key]
        public Guid PrescriptionsID { get; set; }
        public string DoctorName { get; set; }
        public DateTime PrescriptionsDate { get; set; }

        /// <summary>
        /// Foreign key, Primary key of the Admissions table.
        /// </summary>
        [ForeignKey("AdmissionID")]
        public Guid AdmissionID { get; set; }
        public virtual Admission Admissions { get; set; }
        public virtual IEnumerable<MedicationPlan>? MedicationPlans { get; set; }
    }
}
