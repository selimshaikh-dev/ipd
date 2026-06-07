namespace IPD.Domain.Dto
{
    public class CervixCreateDto
    {
        public Guid PartographID { get; set; }
        public List<long[]> Data { get; set; }
    }
}
