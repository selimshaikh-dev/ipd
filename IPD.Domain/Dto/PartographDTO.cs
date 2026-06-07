namespace IPD.Domain.Dto
{
    public class PartographDto
    {
        public Guid PartographID { get; set; }
        public Guid AdmissionID { get; set; }
        public byte Gravida { get; set; }
        public byte Parity { get; set; }
        public string? SBOrNND { get; set; }
        public int? Abortion { get; set; }
        public DateTime EDD { get; set; }
        public string BorderlineRiskFactors { get; set; }
        public decimal? Height { get; set; }
        public DateTime RegularContractions { get; set; }
        public DateTime MembranesRuptured { get; set; }
        public DateTime InitiateDate { get; set; }
        public string InitiateTime { get; set; }
        

        // BirthDetails Dto
        public Guid BirthDetailsID { get; set; }
        public byte IsSuccessfulDelivery { get; set; }
        public string Remarks { get; set; } = null!;
        public byte Gender { get; set; }
        public string Weight { get; set; } = null!;
        public byte TypeOfDelivery { get; set; }
        public DateTime? BirthDate { get; set; } 
        public DateTime? BirthTime { get; set; }
    }
}
