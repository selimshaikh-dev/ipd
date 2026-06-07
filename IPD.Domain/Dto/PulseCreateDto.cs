namespace IPD.Domain.Dto
{
    public class PulseCreateDto
    {
        public Guid PartographID { get; set; }
        public List<long[]> Data { get; set; }
    }
}

