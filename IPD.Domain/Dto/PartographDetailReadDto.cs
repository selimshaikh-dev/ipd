namespace IPD.Domain.Dto
{
    public class PartographDetailReadDto
    {
        public Guid PartographID { get; set; }
        public List<long[]> FetalHeartRateData { get; set; }
        public List<string[]> ContractionsData { get; set; }
        public List<string[]> LiquorData { get; set; }
        public List<string[]> MouldingData { get; set; }
        public List<long[]> CervixData { get; set; }
        public List<long[]> DescentData { get; set; }
        public List<long[]> OxytocinData { get; set; }
        public List<long[]> DropsData { get; set; }
        public List<string[]> MedicineData { get; set; }
        public List<long[]> BloodPressureData { get; set; }
        public List<long[]> PulseData { get; set; }
        public List<long[]> TemparatureData { get; set; }
        public List<string[]> ProteinData { get; set; }
        public List<string[]> AcetoneData { get; set; }
        public List<string[]> VolumeData { get; set; }
    }
}
