using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPD.Domain.Dto
{
    /// <summary>
    ///Represents surgery entity in the database.
    /// </summary>
    public class SurgeriesDto
    {
        /// <summary>
        /// Primary key of the table surgeries.
        /// </summary>
        [Key]
        public Guid SurgeryID { get; set; }

        /// <summary>
        ///Date of the surgery.
        /// </summary>
        [Required(ErrorMessage = "The Surgery Date is required!")]
        [Column(TypeName = "SmallDateTime")]
        [Display(Name = "Surgery Date")]
        public DateTime SurgeryDate { get; set; }

        /// <summary>
        /// Time of the surgery.
        /// </summary>
        [Required(ErrorMessage = "The Surgery Time is required!")]
        [Column(TypeName = "SmallDateTime")]
        [Display(Name = "Surgery Time")]
        public DateTime SurgeryTime { get; set; }

        /// <summary>
        /// Whether the patient is aware of his operation or not.
        /// </summary>
        [Required(ErrorMessage = " Has Patients Concent is required!")]
        [Display(Name = "Has Patients Concent")]
        public bool HasPatientsConcent { get; set; }

        /// <summary>
        /// Patient diagnosis before the surgery.
        /// </summary>
        [Required(ErrorMessage = "The Diagnosis is required!")]
        [StringLength(1000)]
        [Display(Name = "Diagnosis")]
        public string Diagnosis { get; set; }

        /// <summary>
        /// Anaesthetist assessments before the surgery.
        /// </summary>
        [Required(ErrorMessage = "The Anaesthetist Assessment is required!")]
        [StringLength(1000)]
        [Display(Name = "Anaesthetist Assessment")]
        public string AnaesthetistAssessment { get; set; }

        /// <summary>
        /// Description of  the custom surgery type or which was not available in the list.
        /// </summary>
        [StringLength(1000)]
        [Display(Name = "Other Surgery Type")]
        public string OtherSurgeryType { get; set; }

        /// <summary>
        /// Clinicians who perticipated in the surgery.
        /// </summary>
        [Required(ErrorMessage = "The Surgery Team is required!")]
        [StringLength(500)]
        [Display(Name = "Surgery Team")]
        public string SurgeryTeam { get; set; }

        /// <summary>
        /// Description of the surgery procedure.
        /// </summary>
        [Required(ErrorMessage = "The Procedure Indication is required!")]
        [StringLength(1000)]
        [Display(Name = "Procedure Indication")]
        public string ProcedureIndication { get; set; }

        /// <summary>
        /// Foreignkey, Primary key of the table surgerytypes.
        /// </summary>
        [ForeignKey("SurgeryTypeID")]
        public int SurgeryTypeID { get; set; }

        /// <summary>
        /// Foreignkey, Primary key of the table surgicalprocedures.
        /// </summary>
        [ForeignKey("SurgicalProcedureID")]
        public int SurgicalProcedureID { get; set; }

        /// <summary>
        /// Foreignkey, Primary key of the table Admissions.
        /// </summary>
        [ForeignKey("AdmissionID")]
        public Guid AdmissionID { get; set; }
        public string? FacilityCode { get; set; }
        public DateTime? DateCreated { get; set; }
        public List<PostSurgeriesDto>? PostSurgeries { get; set; }
    }
}
