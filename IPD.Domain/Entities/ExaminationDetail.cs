using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPD.Domain.Entities
{
    /// <summary>
    /// The ExaminationDetails table holds the name of all digonosis examination details of the patients
    /// </summary>
    public class ExaminationDetail:BaseModel
    {
        /// <summary>
        /// Primary key of the table ExaminationDetails.
        /// </summary>
        [Key]
        public Guid ExaminationDetailID { get; set; }

        /// <summary>
        /// Foreign key, Primary key of the DigonosisExaminations table.
        /// </summary>
        public int DigonosisExaminationID { get; set; }

        /// <summary>
        /// Foreign key, Primary key of the PatientExaminations table.
        /// </summary>
        public Guid PatientExaminationID { get; set; }

        [ForeignKey("DigonosisExaminationID")]
        public virtual DiagnosisExamination DiagnosisExamination { get; set; }


        [ForeignKey("PatientExaminationID")]
        public virtual PatientExamination PatientExamination { get; set; }
    }
}
