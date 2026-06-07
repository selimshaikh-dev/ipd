namespace IPD.Domain.Dto
{
    public class MouldingCreateDto
    {
        public Guid PartographID { get; set; }
        public List<string[]> Data { get; set; }
    }
}
