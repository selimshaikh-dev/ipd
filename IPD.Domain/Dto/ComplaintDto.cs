using System.ComponentModel.DataAnnotations;

namespace IPD.Domain.Dto
{
    public class ComplaintDto
    {
        public Guid ComplaintID { get; set; }
        public string? ComplaintName { get; set; }
        public string? ComplaintHistory { get; set; }
        public string? SystemsReview { get; set; }
        public byte Diabetes { get; set; }
        public byte Hypertention { get; set; }
        public byte Epilepsy { get; set; }
        public string? SpecialNote { get; set; }
        public Guid AdmissionID { get; set; }
        public List<PatientsNcdDto>? PatientsNcds { get; set; }
        public List<int> NcdsId { get; set; }
        public List<PatientAllergyDto>? PatientAllergy { get; set; }
        public List<AllergiesDto>? allergyDto { get; set; }
        public List<NcdDto>? ncdsDto { get; set; }
        public string? FacilityName { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
