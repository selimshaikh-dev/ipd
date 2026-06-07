namespace IPD.Web.Models.DTO
{
    public class PatientExaminationDTO
    {
        public Guid PatientExaminationID { get; set; }
        public string Findings { get; set; } = null!;
        public Guid AdmissionID { get; set; }
        public string? FacilityName { get; set; }
        public DateTime? DateCreated { get; set; }
        public List<int>? DigonosisExaminationIDs { get; set; }
        public List<DiagonosisExaminationDTO>? DiagonosisExamination { get; set; }
        public List<ExaminationDetailsDTO>? ExaminationDetails { get; set; }
        
    }
}