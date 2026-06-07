namespace IPD.Web.Models.DTO
{
    public class DiagonosisDetailDto
    {
        public int DiagonosisDetailsID { get; set; }
        public int DiseaseID { get; set; }
        public Guid PatientDiagnosisID { get; set; }
    }
}
