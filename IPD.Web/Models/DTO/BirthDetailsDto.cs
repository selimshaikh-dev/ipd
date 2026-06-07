namespace IPD.Web.Models.DTO
{
    public class BirthDetailsDto
    {
        public Guid BirthDetailsID { get; set; }
        public Guid AdmissionID { get; set; }
        public Byte IsSuccessfulDelivery { get; set; }
        public string Remarks { get; set; } = null!;
        public Byte Gender { get; set; }
        public string Weight { get; set; } = null!;
        public Byte TypeOfDelivery { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime BirthTime { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
