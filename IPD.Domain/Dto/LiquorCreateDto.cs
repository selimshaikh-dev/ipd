namespace IPD.Domain.Dto
{
    public class LiquorCreateDto
    {
        public Guid PartographID { get; set; }
        public List<string[]> Data { get; set; }
    }
}
