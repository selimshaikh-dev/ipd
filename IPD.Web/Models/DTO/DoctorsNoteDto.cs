using System.ComponentModel.DataAnnotations;

namespace IPD.Web.Models.DTO
{
    public class DoctorsNoteDto
    {
        public Guid DoctorsNoteId { get; set; }

        [Required(ErrorMessage = "Date of note is required.")]
        [Display(Name = "Date of note")]
        public DateTime DateOfNote { get; set; }

        [Required(ErrorMessage = "Time of note is required.")]
        [Display(Name = "Time of note")]
        public DateTime TimeOfNote { get; set; }

        [Required(ErrorMessage = "Observation is required.")]
        [StringLength(1000)]
        [Display(Name = "Observation")]
        public string Observation { get; set; } = null!;

        [StringLength(1000)]
        [Display(Name = "Test Request")]
        public string? TestRequest { get; set; } = null!;

        [StringLength(20)]
        [Display(Name = "Facility Code")]
        public string? FacilityCode { get; set; }
        
        public Guid AdmissionId { get; set; }

        public DateTime? DateCreated { get; set; }
        public string? FacilityName { get; set; }
    }
}
