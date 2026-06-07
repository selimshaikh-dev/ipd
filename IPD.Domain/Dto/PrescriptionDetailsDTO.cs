namespace IPD.Domain.Dto
{
    public class PrescriptionDetailsDto
    {
        public Guid PrescriptionsID { get; set; }
        public Guid AdmissionID { get; set; }
        public string DoctorName { get; set; }
        public DateTime PrescriptionsDate { get; set; }
        public IEnumerable<MedicationPlanDetailsDto> MedicationPlans { get; set; } = new List<MedicationPlanDetailsDto>();
    }
}
