namespace IPD.Domain.Dto
{
    public class PostSurgeriesDto
    {
        public Guid PostSurgeryID { get; set; }
        public string SurgeryDetails { get; set; }
        public string Findings { get; set; }
        public string PostSurgeryPlan { get; set; }
        public byte PatientsCondition { get; set; }
        public Guid SurgeryID { get; set; }
        public string? FacilityCode { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
