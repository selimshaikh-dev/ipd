namespace IPD.Domain.Dto
{
    public class TreatmentPlanDto
    {
        public Guid TreatmentPlanID { get; set; }
        public string TreatementPlanDetails { get; set; }
        public Guid AdmissionID { get; set; }
        public DateTime? DateCreated { get; set; }
        public string? FacilityName { get; set; }
    }
}
