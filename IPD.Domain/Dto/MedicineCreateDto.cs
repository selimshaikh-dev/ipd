namespace IPD.Domain.Dto
{
    public class MedicineCreateDto
    {
        public Guid PartographID { get; set; }
        public List<string[]> Data { get; set; }
    }
}
