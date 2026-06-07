namespace IPD.Web.Models.DTO
{
    public class MedicationPlanDTO
    {
        public Guid[] MedicationPlanID { get; set; }
        public string DoctorName { get; set; }
        public string[] Dose { get; set; }
        public string[] Durations { get; set; }
        public Guid[] MedicationsID { get; set; }
        public Guid[] IntervalsID { get; set; }
        public Guid[] DirectionsID { get; set; }
        public Guid PrescriptionsID { get; set; }
        public Guid AdmissionID { get; set; }
        public string? FacilityName { get; set; }
        public DateTime? DateCreated { get; set; }
        public List<MedicationDto> medicationDtos { get; set; }
        public List<IntervalDto>? intervalDTOs { get; set; }
        public List<DirectionDto>? directionDTOs { get; set; }
        public MedicationDto? Medications { get; set; }
        public DirectionDto? Directions { get; set; }
        public IntervalDto? Intervals { get; set; }
    }

    public class PrescriptionCreateDTO
    {
        public Guid PrescriptionsID { get; set; }
        public Guid AdmissionID { get; set; }
        public string DoctorName { get; set; }
        public DateTime PrescriptionsDate { get; set; }
        public IEnumerable<PrescriptionDTO> MedicationPlans { get; set; } = new List<PrescriptionDTO>();
    }

    public class PrescriptionDTO
    {
        public Guid MedicationPlanID { get; set; }
        public string Dose { get; set; }
        public string Durations { get; set; }
        public Guid MedicationsID { get; set; }
        public Guid IntervalsID { get; set; }
        public Guid DirectionsID { get; set; }
        public Guid PrescriptionsID { get; set; }
        public Guid AdmissionID { get; set; }
        public DateTime? DateCreated { get; set; }
        public List<MedicationDto>? medicationDtos { get; set; }
        public List<IntervalDto>? intervalDTOs { get; set; }
        public List<DirectionDto>? directionDTOs { get; set; }
        public MedicationDto? Medications { get; set; }
        public DirectionDto? Directions { get; set; }
        public IntervalDto? Intervals { get; set; }
    }
}
