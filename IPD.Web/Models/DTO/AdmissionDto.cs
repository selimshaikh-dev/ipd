using IPD.Domain.Dto;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPD.Web.Models.DTO
{
    public class AdmissionDTO
    {
        public Guid AdmissionID { get; set; }

        [Required(ErrorMessage = "The Admission Date is required!")]
        [Display(Name = "Admission Date")]
        public DateTime AdmissionDate { get; set; }

        [Required(ErrorMessage = "The Admission Time is required!")]
        [Display(Name = "Admission Time")]
        [Column(TypeName = "SmallDateTime")]
        public DateTime AdmissionTime { get; set; }

        [Required(ErrorMessage = "The Assaign Doctor is required!")]
        [StringLength(92)]
        [Display(Name = "Assaign Doctor")]
        public string AssaignDoctor { get; set; } = null!;

        [Required(ErrorMessage = "The Relationship is required!")]
        [StringLength(95)]
        [Display(Name = "Relationship")]
        public string Relationship { get; set; } = null!;

        [Required(ErrorMessage = "The Next Of Kin is required!")]
        [StringLength(95)]
        [Display(Name = "NextOfKin")]
        public string NextOfKin { get; set; } = null!;

        [Required(ErrorMessage = "The Contact Address is required!")]
        [StringLength(500)]
        [Display(Name = "ContactAddress")]
        public string ContactAddress { get; set; } = null!;

        [Required(ErrorMessage = "The Cellphone Country Code is required!")]
        [StringLength(3)]
        [Display(Name = "Cellphone Country Code")]
        public string CellphoneCountryCode { get; set; } = null!;

        [Required(ErrorMessage = "The Cellphone is required!")]
        //[StringLength(15)]
        [Display(Name = "Cellphone")]
        public string Cellphone { get; set; } = null!;

        [Required(ErrorMessage = "IsDischarged is required!")]
        [Display(Name = "Is Discharged")]
        public bool IsDischarged { get; set; }

        public Guid PatientID { get; set; }

        public DateTime? DateCreated { get; set; }

        public List<DischargesDto>? Discharges { get; set; }
        public List<DeathCertificateDto>? DeathCertificates { get; set; }
    }
}
