using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPD.Web.Models.DTO
{
    public class LocalReferralDto
    {
        public Guid LocalReferralID { get; set; }

        [StringLength(100)]
        [Display(Name = "Phalala")]
        public string? Phalala { get; set; }

        [StringLength(100)]
        [Display(Name = "Civil servent")]
        public string? CivilServent { get; set; }

        [StringLength(50)]
        [Display(Name = "Employment number")]
        public string? EmploymentNumber { get; set; }

        [Required(ErrorMessage = "The Referral type is required!")]
        [Display(Name = "Referral type")]
        public byte ReferralType { get; set; }

        [Required(ErrorMessage = "The Referring specialist is required!")]
        [StringLength(92)]
        [Display(Name = "Referring specialist")]
        public string ReferringSpecialist { get; set; }

        [Required(ErrorMessage = "The Practice number is required!")]
        [StringLength(100)]
        [Display(Name = "Practice number")]
        public string PracticeNumber { get; set; }

        [StringLength(100)]
        [Display(Name = "Discipline")]
        public string Discipline { get; set; }

        [StringLength(1000)]
        [Display(Name = "Reason referral")]
        public string ReasonReferral { get; set; }

        [StringLength(1000)]
        [Display(Name = "Short history")]
        public string ShortHistory { get; set; }

        [StringLength(1000)]
        [Display(Name = "Investigation")]
        public string Investigation { get; set; }

        [Required(ErrorMessage = "The Contact details is required!")]
        [StringLength(1000)]
        [Display(Name = "Contact details")]
        public string ContactDetails { get; set; }

        [Required(ErrorMessage = "The date is required!")]
        [Column(TypeName = "SmallDateTime")]
        [Display(Name = "Date")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "TheTime is required!")]
        [Column(TypeName = "SmallDateTime")]
        [Display(Name = "Time")]
        public DateTime Time { get; set; }

        [Required(ErrorMessage = "The PatientsTransferApparatus is required!")]
        [Display(Name = "Patients Transfer Apparatus")]
        public byte PatientsTransferApparatus { get; set; }
        public Guid AdmissionID { get; set; }
        public int ProcedureID { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
