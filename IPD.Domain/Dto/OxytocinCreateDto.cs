namespace IPD.Domain.Dto
{
    public class OxytocinCreateDto
    {
        public Guid PartographID { get; set; }
        public List<long[]> Data { get; set; }
    }
}
