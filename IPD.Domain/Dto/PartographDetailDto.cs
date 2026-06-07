namespace IPD.Domain.Dto
{
    public class PartographDetailDto
    {
        public Guid PartographDetailsID { get; set; }
        public Guid PartographID { get; set; }
        public string Liquor { get; set; }
        public DateTime LiquorTime { get; set; }
        public string Moulding { get; set; }
        public DateTime MouldingTime { get; set; }
        public string Cervix { get; set; }
        public DateTime CervixTime { get; set; }
        public int DescentOfHead { get; set; }
        public DateTime DescentOfHeadTime { get; set; }
        public int Contractions { get; set; }
        public string ContractionsDuration { get; set; }
        public DateTime ContractionsTime { get; set; }
        public int Oxytocin { get; set; }
        public DateTime OxytocinTime { get; set; }
        public int Drops { get; set; }
        public DateTime DropsTime { get; set; }
        public string Medicine { get; set; }
        public DateTime MedicineTime { get; set; }
        public int Systolic { get; set; }
        public int Diastolic { get; set; }
        public DateTime BpTime { get; set; }
        public int Pulse { get; set; }
        public DateTime PulseTime { get; set; }
        public int Temp { get; set; }
        public DateTime TempTime { get; set; }
        public string Protein { get; set; }
        public DateTime ProteinTime { get; set; }
        public string Acetone { get; set; }
        public DateTime AcetoneTime { get; set; }
        public int Volume { get; set; }
        public DateTime VolumeTime { get; set; }
        public int FetalRate { get; set; }
        public DateTime FetalRateTime { get; set; }
    }
}
