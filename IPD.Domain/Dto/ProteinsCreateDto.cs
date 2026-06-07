namespace IPD.Domain.Dto
{
    public class ProteinsCreateDto
    {
        public Guid PartographID { get; set; }
        public List<string[]> Data { get; set; }
    }
}
