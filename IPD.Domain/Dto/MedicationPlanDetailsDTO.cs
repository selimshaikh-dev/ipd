namespace IPD.Domain.Dto
{
    public class MedicationPlanDetailsDto
    {
        public string Dose { get; set; }
        public string Durations { get; set; }
        public Guid MedicationsID { get; set; }
        public Guid MedicationPlanID { get; set; }
        public Guid IntervalsID { get; set; }
        public Guid DirectionsID { get; set; }
        public string Medications { get; set; }
        public string Intervals { get; set; }
        public string Directions { get; set; }
    }
}
