namespace IPD.Domain.Dto
{
    public class PrescriptionsDto
    {
        public Guid PrescriptionsID { get; set; }
        public Guid AdmissionID { get; set; }
        public string DoctorName { get; set; }
        public DateTime PrescriptionsDate { get; set; }
        public IEnumerable<MedicationPlanDto> MedicationPlans { get; set; }
    }
}
