using IPD.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPD.Domain.Dto
{
    public class InternationalReferralsDto
    {
        [Key]
        public Guid InternationalReferralID { get; set; }

        /// <summary>
        /// Phalala fund is to assist deserving swazi citizens
        /// </summary>
        [StringLength(100)]
        [Display(Name = "Phalala")]
        public string? Phalala { get; set; }

        /// <summary>
        /// Describe the patients is civilServent information
        /// </summary>
        [StringLength(100)]
        [Display(Name = "Civil servent")]
        public string? CivilServent { get; set; }

        /// <summary>
        ///Patients employment Number.
        /// </summary>
        [StringLength(50)]
        [Display(Name = "Employment number")]
        public string? EmploymentNumber { get; set; }

        /// <summary>
        ///  Type of referral is it local or international.
        /// </summary>
        [Required(ErrorMessage = "The Referral type is required!")]
        [Display(Name = "Referral type")]
        public byte ReferralType { get; set; }

        /// <summary>
        /// Referring specialist name under this referral.
        /// </summary
        [Required(ErrorMessage = "The Referring specialist is required!")]
        [StringLength(92)]
        [Display(Name = "Referring specialist")]
        public string? ReferringSpecialist { get; set; }

        /// <summary>
        /// Patients practice  number.
        /// </summary>
        [Required(ErrorMessage = "The Practice number is required!")]
        [StringLength(100)]
        [Display(Name = "Practice number")]
        public string? PracticeNumber { get; set; }

        /// <summary>
        /// Patients Referring to: Discipline / Specialty 
        /// </summary>
        [StringLength(100)]
        [Display(Name = "Discipline")]
        public string? Discipline { get; set; }

        /// <summary>
        /// Reason of Referral of the patients.
        /// </summary>
        [StringLength(1000)]
        [Display(Name = "Reason referral")]
        public string? ReasonReferral { get; set; }

        /// <summary>
        /// Short history & diagnosis of the patients 
        /// </summary>
        [StringLength(1000)]
        [Display(Name = "Short history")]
        public string? ShortHistory { get; set; }

        /// <summary>
        /// Investigation done report of the patients.
        /// </summary>
        [StringLength(1000)]
        [Display(Name = "Investigation")]
        public string? Investigation { get; set; }

        /// <summary>
        /// Contract details of the patients
        /// </summary>
        [Required(ErrorMessage = "The Contact details is required!")]
        [StringLength(1000)]
        [Display(Name = "Contact details")]
        public string? ContactDetails { get; set; }

        /// <summary>
        /// Date of referral.
        /// </summary>
        [Required(ErrorMessage = "The date is required!")]
        [Column(TypeName = "SmallDateTime")]
        [Display(Name = "Date")]
        public DateTime Date { get; set; }

        /// <summary>
        /// Time of referral.
        /// </summary>
        [Required(ErrorMessage = "TheTime is required!")]
        [Column(TypeName = "SmallDateTime")]
        [Display(Name = "Time")]
        public DateTime Time { get; set; }

        /// <summary>
        ///  Type of referral is it local or international.
        /// </summary>

        [Required(ErrorMessage = "The PatientsTransferApparatus is required!")]
        [Display(Name = "Patients Transfer Apparatus")]
        public byte PatientsTransferApparatus { get; set; }

        /// <summary>
        /// Passport Number of the patients
        /// </summary>

        [Required(ErrorMessage = "The Passport Number is required!")]
        [StringLength(30)]
        [Display(Name = "Passport Number")]
        public string? PassportNumber { get; set; }

        /// <summary>
        /// ID Number of the patients
        /// </summary>

        [Required(ErrorMessage = "The ID Number is required!")]
        [StringLength(30)]
        [Display(Name = "ID Number")]
        public string? IDNumber { get; set; }

        /// <summary>
        /// Language of the patients
        /// </summary>

        [Required(ErrorMessage = "The Language is required!")]
        [StringLength(60)]
        [Display(Name = "Language")]
        public string? Language { get; set; }

        /// <summary>
        /// Occupation of the patients
        /// </summary>

        [Required(ErrorMessage = "The Occupation is required!")]
        [StringLength(30)]
        [Display(Name = "Occupation")]
        public string? Occupation { get; set; }

        /// <summary>
        /// Relegion of the patients
        /// </summary>

        [Required(ErrorMessage = "The Relegion is required!")]
        [StringLength(30)]
        [Display(Name = "Relegion")]
        public string? Relegion { get; set; }

        /// <summary>
        /// Employer of the patients
        /// </summary>

        [StringLength(30)]
        [Display(Name = "Employer")]
        public string? Employer { get; set; }


        /// <summary>
        /// Allergies of the patients
        /// </summary>

        [Required(ErrorMessage = "The Allergies is required!")]
        [StringLength(100)]
        [Display(Name = "Allergies")]
        public string? Allergies { get; set; }

        /// <summary>
        /// ChronicIllness of the patients
        /// </summary>

        [Required(ErrorMessage = "The ChronicIllness is required!")]
        [StringLength(100)]
        [Display(Name = "ChronicIllness")]
        public string? ChronicIllness { get; set; }

        /// <summary>
        /// Medication of the patients
        /// </summary>

        [Required(ErrorMessage = "The Medication is required!")]
        [StringLength(100)]
        [Display(Name = "Medication")]
        public string? Medication { get; set; }

        /// <summary>
        ///  Foreign key, primary key of the procedure table.
        /// </summary>

        public int ProcedureID { get; set; }

        /// <summary>
        /// Foreign key, primary key of the Admission table.
        /// </summary>
        [ForeignKey("AdmissionID")]
        public Guid AdmissionID { get; set; }

        /// <summary>
        /// Foreign key, primary key of the Region table.
        /// </summary>

        public int RegionID { get; set; }

        [ForeignKey("RegionID")]
        public virtual Region? Regions { get; set; }
        [ForeignKey("ProcedureID")]
        public virtual Procedure? Procedures { get; set; }
        public virtual Admission? Admissions { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
