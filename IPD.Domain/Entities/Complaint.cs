using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPD.Domain.Entities
{
    /// <summary>
    /// The complaints table hold the complaints of the patients
    /// </summary>
    public class Complaint : BaseModel
    {
        /// <summary>
        /// Primary key of the table complaint.
        /// </summary>
        [Key]
        public Guid ComplaintID { get; set; }

        /// <summary>
        /// Name of the complaints
        /// </summary>
        [Required(ErrorMessage = "The complaint name is required!")]
        [StringLength(2000)]
        [Display(Name = "Complaint name")]
        public string ComplaintName { get; set; }

        /// <summary>
        /// History of chief complaint of the patients
        /// </summary>
        [Required(ErrorMessage = "The complaint history is required!")]
        [StringLength(1000)]
        [Display(Name = "Complaint history")]
        public string ComplaintHistory { get; set; }

        /// <summary>
        /// Other systems review of the patients
        /// </summary>
        [StringLength(1000)]
        [Display(Name = "Systems review")]
        public string SystemsReview { get; set; }

        /// <summary>
        /// Whether the patients has diabetes
        /// </summary>
        [Required(ErrorMessage = " Diabetes is required!")]
        [Display(Name = "Diabetes")]
        public byte Diabetes { get; set; }

        /// <summary>
        /// Whether the patients has hypertention
        /// </summary>
        [Required(ErrorMessage = " Hypertention is required!")]
        [Display(Name = "Hypertention")]
        public byte Hypertention { get; set; }

        /// <summary>
        /// Whether the hypertention has epilepsy 
        /// </summary>
        [Required(ErrorMessage = " Epilepsy is required!")]
        [StringLength(1000)]
        [Display(Name = "Epilepsy")]
        public byte Epilepsy { get; set; }

        /// <summary>
        /// Special note regarding complaints
        /// </summary>
       
        [StringLength(1000)]
        [Display(Name = "Special note")]
        public string? SpecialNote { get; set; }

        /// <summary>
        /// Foreign key, primary key of the admissions table.
        /// </summary>
        [ForeignKey("AdmissionID")]
        public Guid AdmissionID { get; set; }
        public virtual Admission? Admissions { get; set; }
        public virtual IEnumerable<PatientsNcd> PatientsNcds { get; set; } = new List<PatientsNcd>();
        public virtual IEnumerable<PatientAllergy> PatientAllergy { get; set; } = new List<PatientAllergy>();
    }
}
