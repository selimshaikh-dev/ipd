namespace IPD.Web.Models.DTO
{
    public class SurgeryDto
    {
        public Guid SurgeryID { get; set; }
        public DateTime SurgeryDate { get; set; }
        public DateTime SurgeryTime { get; set; }
        public bool HasPatientsConcent { get; set; }
        public string Diagnosis { get; set; }
        public string AnaesthetistAssessment { get; set; }
        public string OtherSurgeryType { get; set; }
        public string SurgeryTeam { get; set; }
        public string ProcedureIndication { get; set; }
        public int SurgeryTypeID { get; set; }
        public int SurgicalProcedureID { get; set; }
        public Guid AdmissionID { get; set; }
        public DateTime? DateCreated { get; set; }
        public List<PostSurgeryDto>? PostSurgeries { get; set; }
    }
}
