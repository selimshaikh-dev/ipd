namespace IPD.Domain.Dto
{
    public class DiagonosisDetailsDto
    {
        public int DiagonosisDetailsID { get; set; }
        public int DiseaseID { get; set; }
        public Guid PatientDiagnosisID { get; set; }
    }
}
