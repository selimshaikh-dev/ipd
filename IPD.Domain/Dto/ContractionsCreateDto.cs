namespace IPD.Domain.Dto
{
    public class ContractionsCreateDto
    {
        public Guid PartographID { get; set; }
        public List<string[]> Data { get; set; }
    }
}
