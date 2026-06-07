using System.ComponentModel.DataAnnotations;

namespace IPD.Web.Models.DTO
{
    public class PatientDetailsDto
    {
        public Guid PatientDetailsID { get; set; }

        [Required(ErrorMessage = "The Passport Number is required!")]
        [StringLength(30)]
        [Display(Name = "Passport Number")]
        public string PassportNumber { get; set; }
        
        [Required(ErrorMessage = "The ID Number is required!")]
        [StringLength(30)]
        [Display(Name = "ID Number")]
        public string IDNumber { get; set; }

        [Required(ErrorMessage = "The Language is required!")]
        [StringLength(60)]
        [Display(Name = "Language")]
        public string Language { get; set; }

        [Required(ErrorMessage = "The Occupation is required!")]
        [StringLength(30)]
        [Display(Name = "Occupation")]
        public string Occupation { get; set; }

        [Required(ErrorMessage = "The Relegion is required!")]
        [StringLength(30)]
        [Display(Name = "Relegion")]
        public string Relegion { get; set; }

        [StringLength(30)]
        [Display(Name = "Employer")]
        public string Employer { get; set; }

        [Required(ErrorMessage = "The Allergies is required!")]
        [StringLength(100)]
        [Display(Name = "Allergies")]
        public string Allergies { get; set; }

        [Required(ErrorMessage = "The ChronicIllness is required!")]
        [StringLength(100)]
        [Display(Name = "ChronicIllness")]
        public string ChronicIllness { get; set; }

        [Required(ErrorMessage = "The Medication is required!")]
        [StringLength(100)]
        [Display(Name = "Medication")]
        public string Medication { get; set; }

        public Guid AdmissionID { get; set; }
        public int RegionID { get; set; }
    }
}
