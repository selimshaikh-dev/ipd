using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPD.Domain.Entities
{
    public class DiagonosisDetail: BaseModel
    {
        /// <summary>
        /// Primary key of the table DiagonosisDetails.
        /// </summary>
        [Key]
        public int DiagonosisDetailsID { get; set; }

        /// <summary>
        /// Foreign key, Primary key of the ICDDigonosisCodes table.
        /// </summary>

        public int DiseaseID { get; set; }

        /// <summary>
        /// Foreign key, Primary key of the PatientDiagnosis table.
        /// </summary>
        public Guid PatientDiagnosisID { get; set; }

        [ForeignKey("DiseaseID")]
        public virtual ICDDigonosisCode ICDDigonosisCodes { get; set; }


        [ForeignKey("PatientDiagnosisID")]
        public virtual PatientDiagnosis PatientDiagnosis { get; set; }
    }
}
