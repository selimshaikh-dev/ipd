using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPD.Domain.Entities
{
    /// <summary>
    /// Represents doctorsNotes entity in the database.
    /// </summary>
    public class DoctorsNote : BaseModel
    {
        /// <summary>
        /// Primary key of the doctorsNotes entity.
        /// </summary>
        [Key]
        public Guid DoctorsNoteID { get; set; }

        /// <summary>
        /// Date when doctore recored the note.
        /// </summary>
        [Required(ErrorMessage = "Date of note is required.")]
        [Column(TypeName = "smalldatetime")]
        [Display(Name = "Date of note")]
        public DateTime DateOfNote { get; set; }

        /// <summary>
        /// Time when doctor record the note.
        /// </summary>
        [Required(ErrorMessage = "Time of note is required.")]
        [Column(TypeName = "smalldatetime")]
        [Display(Name = "Time of note")]
        public DateTime TimeOfNote { get; set; }

        /// <summary>
        /// Doctor's observation about the patient.
        /// </summary>
        [Required(ErrorMessage = "Observation is required.")]
        [StringLength(1000)]
        [Display(Name = "Observation")]
        public string Observation { get; set; } = null!;

        /// <summary>
        /// Doctors request for Test/Radiology
        /// </summary>
        [StringLength(1000)]
        [Display(Name = "Test Request")]
        public string? TestRequest { get; set; } = null!;

        /// <summary>
        /// Code of the health facility.
        /// </summary>        
        [StringLength(20)]
        [Display(Name = "Facility Code")]
        public string? FacilityCode { get; set; }

        /// <summary>
        /// Foreignkey, Primary key of the table admissions.
        /// </summary>
        public Guid AdmissionID { get; set; }
        [ForeignKey("AdmissionID")]
        public virtual Admission Admission { get; set; }
    }
}
