using System.ComponentModel.DataAnnotations;

namespace IPD.Domain.Entities
{
    /// <summary>
    /// The PatientsExaminations table holds  the Patients Examinations detail of the patients
    /// </summary>
    public class DiagnosisExamination : BaseModel
    {
        /// <summary>
        /// Primary key of the table DigonosisExaminations
        /// </summary>
        [Key]
        public int DigonosisExaminationID { get; set; }

        /// <summary>
        /// Name of the DigonosisExaminations.
        /// </summary>
        [Required(ErrorMessage = "The Digonosis examinations name is required!")]
        [StringLength(500)]
        [Display(Name = "Digonosis examinations name")]
        public string DigonosisExaminationsName { get; set; }
        public virtual IEnumerable<ExaminationDetail>? ExaminationDetails { get; set; }
    }
}