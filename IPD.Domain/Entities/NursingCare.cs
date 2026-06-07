using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPD.Domain.Entities
{
    /// <summary>
    /// Represents nursingCares entity in the database.
    /// </summary>
    public class NursingCare : BaseModel
    {
        /// <summary>
        /// Primary key of the nursingCares entity.
        /// </summary>
        [Key]
        public Guid NursingCareID { get; set; }

        /// <summary>
        /// Date when the care given to the patient.
        /// </summary>
        [Required(ErrorMessage = "Date of care is required.")]
        [Column(TypeName = "smalldatetime")]
        [Display(Name = "Date of care")]
        public DateTime DateOfCare { get; set; }

        /// <summary>
        /// Time when the care given to the patient.
        /// </summary>
        [Required(ErrorMessage = "Time of care is required.")]
        [Column(TypeName = "smalldatetime")]
        [Display(Name = "Time of care")]
        public DateTime TimeOfCare { get; set; }

        /// <summary>
        /// Complain of the patient.
        /// </summary>       
        [Required(ErrorMessage = "Problem is required.")]
        [StringLength(1000)]
        [Display(Name = "Problem")]
        public string Problem { get; set; } = null!;

        /// <summary>
        /// Nurse's diagonosis about the stated problem.
        /// </summary>
        [Required(ErrorMessage = "Diagnosis is required.")]
        [StringLength(1000)]
        [Display(Name = "Diagnosis")]
        public string Diagnosis { get; set; } = null!;

        /// <summary>
        /// Objective of the treatment.
        /// </summary>
        [Required(ErrorMessage = "Objective is required.")]
        [StringLength(1000)]
        [Display(Name = "Objective")]
        public string Objective { get; set; } = null!;

        /// <summary>
        /// Taken action against the stated problem.
        /// </summary>
        [Required(ErrorMessage = "Intervension is required.")]
        [StringLength(1000)]
        [Display(Name = "Intervension")]
        public string Intervension { get; set; } = null!;

        /// <summary>
        /// Logic or reason that supports the taken intervension.
        /// </summary>
        [StringLength(1000)]
        [Display(Name = "Rational")]
        public string? Rational { get; set; }

        /// <summary>
        /// Condition of the patient after the intervention.
        /// </summary>
        [StringLength(1000)]
        [Display(Name = "Evaluation")]
        public string? Evaluation { get; set; }

        /// <summary>
        /// Code of the health facility.
        /// </summary>
        [StringLength(20)]
        [Display(Name = "Facility code")]
        public string? FacilityCode { get; set; }

        /// <summary>
        /// Foreignkey, Primary key of the table admissions.
        /// </summary>
        public Guid AdmissionID { get; set; }
        [ForeignKey("AdmissionID")]
        public virtual Admission Admission { get; set; }
    }
}