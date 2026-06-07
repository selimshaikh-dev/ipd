namespace IPD.Domain.Dto
{
    public class PatientExaminationsDto
    {
        public Guid PatientExaminationID { get; set; }
        public string Findings { get; set; } = null!;
        public string? FacilityName { get; set; }
        public Guid AdmissionID { get; set; }
        public DateTime? DateCreated { get; set; }
        public List<int>? DigonosisExaminationIDs { get; set; }
        public List<ExaminationDetailsDto>? ExaminationDetails { get; set; }
        public List<DiagonosisExaminationDto>? DiagonosisExamination { get; set; }
    }
}
