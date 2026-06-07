namespace IPD.Domain.Dto
{
    public class DropCreateDto
    {
        public Guid PartographID { get; set; }
        public List<long[]> Data { get; set; }
    }
}