using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPD.Domain.Entities
{
    /// <summary>
    /// The PatientsNcds table holds the name of the patients  NCDs
    /// </summary>
    public class PatientsNcd :BaseModel
    {
        /// <summary>
        /// Primary key of the table PatientsNcds.
        /// </summary>
        [Key]
        public Guid PatientNcdsID { get; set; }

        /// <summary>
        /// Foreign key, Primary key of the Ncds table.
        /// </summary>
        [ForeignKey("NcdsID")]
        public int NcdsID { get; set; }

        /// <summary>
        /// Foreign key, Primary key of the Complaints table.
        /// </summary>
        [ForeignKey("ComplaintID")]
        public Guid ComplaintID { get; set; }
        public virtual Ncd Ncds { get; set; }
        public virtual Complaint Complaint { get; set; }
    }
}
