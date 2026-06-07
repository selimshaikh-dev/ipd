using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPD.Domain.Entities
{
    /// <summary>
    /// The PatientDiagnosis table holds  the Patients diagnosis detail of the patients
    /// </summary>
    public class PatientDiagnosis : BaseModel
    {
        /// <summary>
        /// Primary key of the table PatientDiagnosis.
        /// </summary>
        [Key]
        public Guid PatientDiagnosisID { get; set; }

        /// <summary>
        /// Patients diagnosis details of the patients. 
        /// </summary>
        [Required(ErrorMessage = "The Diagnosis note is required!")]
        [StringLength(1000)]
        [Display(Name = "Diagnosis note")]
        public string DiagnosisNote { get; set; }

        /// <summary>
        /// Foreign key, Primary key of the Admissions table.
        /// </summary>
        [ForeignKey("AdmissionID")]
        public Guid AdmissionID { get; set; }
        public virtual Admission Admissions { get; set; }
        public virtual IEnumerable<DiagonosisDetail> DiagonosisDetails { get; set; } = new List<DiagonosisDetail>();
    }
}
