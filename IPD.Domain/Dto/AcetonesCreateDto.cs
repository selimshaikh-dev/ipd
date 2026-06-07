namespace IPD.Domain.Dto
{
    public class AcetonesCreateDto
    {
        /// <summary>
        /// Foreign key, Primary key of the Partograph table.
        /// </summary>
        public Guid PartographID { get; set; }
        public List<string[]> Data { get; set; }
    }
}