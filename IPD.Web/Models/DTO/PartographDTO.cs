namespace IPD.Web.Models.DTO
{
    public class PartographDTO
    {
        public Guid PartographID { get; set; }
        public Guid AdmissionID { get; set; }
        public byte Gravida { get; set; }
        public byte Parity { get; set; }
        public string SBOrNND { get; set; }
        public string Abortion { get; set; }
        public DateTime EDD { get; set; }
        public string BorderlineRiskFactors { get; set; }
        public decimal Height { get; set; }
        public DateTime RegularContractions { get; set; }
        public DateTime MembranesRuptured { get; set; }
        public DateTime? DateCreated { get; set; }

        // BirthDetails Dto
        public Guid BirthDetailsID { get; set; }

        public bool IsSuccessfulDelivery { get; set; }
        public string Remarks { get; set; } = null!;
        public Byte Gender { get; set; }
        public string Weight { get; set; } = null!;
        public Byte TypeOfDelivery { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime BirthTime { get; set; }
    }
}