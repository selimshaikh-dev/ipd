using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPD.Domain.Entities
{
    /// <summary>
    ///  Contains details of interDepartmentReferral.
    /// </summary>
    public class InterDepartmentReferral:BaseModel
    {
        /// <summary>
        /// Primary key of InterDepartmentReferrals table.
        /// </summary>
        [Key]
        public Guid InterDepartmentReferralsID { get; set; }

        /// <summary>
        /// Foreign key, Primary key of the Departments table.
        /// </summary>
        [ForeignKey("DepartmentID")]
        public Guid DepartmentID { get; set; }

        /// <summary>
        /// Patient refer from.
        /// </summary>
        [Required(ErrorMessage = "The ReferralTo is required!")]
        [StringLength(1000)]
        [Display(Name = "ReferralTo")]
        public string ReferralTo { get; set; } = null!;

        /// <summary>
        /// Date of Referral.
        /// </summary>
        [Required(ErrorMessage = "The Date is required!")]
        [Display(Name = "Date")]
        [Column(TypeName = "SmallDateTime")]
        public DateTime Date { get; set; }

        /// <summary>
        /// Time of Referral.
        /// </summary>
        [Required(ErrorMessage = "The Time is required!")]
        [Display(Name = "Time")]
        [Column(TypeName = "SmallDateTime")]
        public DateTime Time { get; set; }

        /// <summary>
        ///  Ward number of the Patient.
        /// </summary>
        [Required(ErrorMessage = "The Ward is required!")]
        [StringLength(30)]
        [Display(Name = "Ward")]
        public string Ward { get; set; } = null!;

        /// <summary>
        /// Reason Of Referral of the Patients.
        /// </summary>
        [Required(ErrorMessage = "The Reason Of Referral is required!")]
        [StringLength(1000)]
        [Display(Name = "Reason of referral")]
        public string ReasonOfReferral { get; set; } = null!;

        /// <summary>
        /// Name of the Referral officer.
        /// </summary>
        [Required(ErrorMessage = "The Referral Officer is required!")]
        [StringLength(92)]
        [Display(Name = "Referral officer")]
        public string ReferralOfficer { get; set; } = null!;

        /// <summary>
        /// Feedback against Referral of the Patients.
        /// </summary>
        [StringLength(1000)]
        [Display(Name = "Feedback")]
        public string? Feedback { get; set; }

        /// <summary>
        /// ConsultingOfficer of Referral.
        /// </summary>
        [StringLength(92)]
        [Display(Name = "Consulting officer")]
        public string? ConsultingOfficer { get; set; }

        /// <summary>
        /// Foreign key, Primary key of the Admissions table.
        /// </summary>
        [ForeignKey("AdmissionID")]
        public Guid AdmissionID { get; set; }
        public virtual Department? Departments { get; set; }
        public virtual Admission? Admissions { get; set; }
    }
}
