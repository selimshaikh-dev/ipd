namespace IPD.Web.Models.DTO
{
    public class ChiefComplaintsDTO
    {
        public Guid ComplaintID { get; set; }
        public string ComplaintName { get; set; }
        public string ComplaintHistory { get; set; }
        public string SystemsReview { get; set; }
        public byte Diabetes { get; set; }
        public byte Hypertention { get; set; }
        public byte Epilepsy { get; set; }
        public string? SpecialNote { get; set; }
        public Guid AdmissionID { get; set; }
        public List<int>? PatientsNcdsIds { get; set; }
        public List<int>? PatientAllergyIds { get; set; }
        public List<PatientsNcdDto>? PatientsNcds { get; set; }
        public List<PatientAllergyDto>? PatientAllergy { get; set; }
        public DateTime? DateCreated { get; set; }
        public List<AllergyDto>? allergyDto { get; set; }
        public List<NcdsDto>? ncdsDto { get; set; }
        public string? FacilityName { get; set; }

    }   
}
