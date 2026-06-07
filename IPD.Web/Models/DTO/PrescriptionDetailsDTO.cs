namespace IPD.Web.Models.DTO
{
    public class PrescriptionDetailsDTO
    {
        public Guid PrescriptionsID { get; set; }
        public Guid AdmissionID { get; set; }
        public string DoctorName { get; set; }
        public DateTime PrescriptionsDate { get; set; }
        public IEnumerable<MedicationPlanDetailsDTO> MedicationPlans { get; set; }
    }
}
