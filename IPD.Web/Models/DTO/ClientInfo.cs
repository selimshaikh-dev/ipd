namespace IPD.Web.Models.DTO
{
    public class ClientInfo
    {
        public Guid PatientID { get; set; }
        public string UHID { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string? MiddleName { get; set; }
        public string LastName { get; set; } = null!;
        public DateTime DOB { get; set; }
        public byte Sex { get; set; }
        public byte MaritalStatus { get; set; }
    }
}
