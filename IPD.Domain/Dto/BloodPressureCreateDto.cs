namespace IPD.Domain.Dto
{
    public class BloodPressureCreateDto
    {
        public Guid PartographID { get; set; }
        public List<long[]> Data { get; set; }
    }
}