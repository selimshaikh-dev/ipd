using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPD.Domain.Entities
{
    /// <summary>
    /// The DigonosisExaminations table holds the name of all digonosis examinations of the patients
    /// </summary>
    public class PatientExamination : BaseModel
    {
        /// <summary>
        /// Primary key of the table PatientExaminations.
        /// </summary>
        [Key]
        public Guid PatientExaminationID { get; set; }

        /// <summary>
        /// Patients examination details of the patients.
        /// </summary>
        [Required(ErrorMessage = "The Findings is required!")]
        [StringLength(1000)]
        [Display(Name = "Findings")]
        public string Findings { get; set; }

        /// <summary>
        /// Foreign key, Primary key of the Admissions table.
        /// </summary>
        public Guid AdmissionID { get; set; }
        [ForeignKey("AdmissionID")]
        public virtual Admission Admissions { get; set; }
        public virtual IEnumerable<ExaminationDetail> ExaminationDetails { get; set; } = new List<ExaminationDetail>();

    }
}