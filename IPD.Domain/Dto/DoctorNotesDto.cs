using IPD.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace IPD.Domain.Dto
{
    public class DoctorNotesDto
    {
        public Guid DoctorsNoteID { get; set; }
        public DateTime DateOfNote { get; set; }
        public DateTime TimeOfNote { get; set; }

        [StringLength(1000)]
        public string Observation { get; set; } = null!;

        [StringLength(1000)]
        public string? TestRequest { get; set; } = null!;

        [StringLength(20)]
        public string? FacilityCode { get; set; }
        public Guid AdmissionID { get; set; }
        public DateTime? DateCreated { get; set; }
        public string? FacilityName { get; set; }
    }
}
