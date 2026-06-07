namespace IPD.Domain.Dto
{
    public class TemperaturesCreateDto
    {
        public Guid PartographID { get; set; }
        public List<long[]> Data { get; set; }
    }
}
