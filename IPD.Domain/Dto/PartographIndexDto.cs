namespace IPD.Domain.Dto
{
    public class PartographIndexDto
    {
        #region Public Properties
        public Guid PartographID { get; set; }
        public Guid AdmissionID { get; set; }
        public DateTime InitiateDate { get; set; }
        public string InitiateTime { get; set; }
        public Guid BirthDetailsID { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? BirthTime { get; set; }
        public string Remarks { get; set; }
        public byte IsSuccessfulDelivery { get; set; }
        public Byte TypeOfDelivery { get; set; }
        public string FirstName { get; set; } = null!;
        public string MiddleName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string FacilityCode { get; set; } = null!;

        #endregion Public Properties
    }
}
