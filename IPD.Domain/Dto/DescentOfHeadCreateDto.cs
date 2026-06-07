namespace IPD.Domain.Dto
{
    public class DescentOfHeadCreateDto
    {
        public Guid PartographID { get; set; }
        public List<long[]> Data { get; set; }
    }
}
