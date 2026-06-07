namespace IPD.Domain.Dto
{
    public class PatientDiagnosisDto
    {
        public Guid PatientDiagnosisID { get; set; }
        public string DiagnosisNote { get; set; }
        public Guid AdmissionID { get; set; }
        public DateTime? DateCreated { get; set; }
        public string? FacilityName { get; set; }
        public List<int>? PatientsDiseaseIds { get; set; }
        public IEnumerable<DiagonosisDetailsDto> DiagonosisDetails { get; set; }
        

    }
}
