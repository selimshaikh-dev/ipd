namespace IPD.Domain.Dto
{
    public class ICDDigonosisCodeDto
    {
        public int DiseaseID { get; set; }
        public string ICDCode { get; set; }
        public string Description { get; set; }
        public int ParentsID { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
