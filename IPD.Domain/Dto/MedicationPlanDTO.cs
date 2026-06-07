using IPD.Domain.Entities;

namespace IPD.Domain.Dto
{
    public class MedicationPlanDto
    {
        public Guid MedicationPlanID { get; set; }
        public string Dose { get; set; }
        public string Durations { get; set; }
        public Guid MedicationsID { get; set; }
        public Guid IntervalsID { get; set; }
        public Guid DirectionsID { get; set; }
        public Guid PrescriptionsID { get; set; }
        public DateTime? DateCreated { get; set; }
        public string? FacilityName { get; set; }
        public string? MedicationName { get; set; }
        public Guid AdmissionID { get; set; }
        public MedicationDto? Medications { get; set; }
        public DirectionDto? Directions { get; set; }
        public IntervalDto? Intervals { get; set; }
    }
}
  