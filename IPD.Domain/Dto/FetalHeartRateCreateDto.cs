namespace IPD.Domain.Dto
{
    public class FetalHeartRateCreateDto
    {
        public Guid PartographID { get; set; }
        public List<long[]> Data { get; set; }
    }
}
