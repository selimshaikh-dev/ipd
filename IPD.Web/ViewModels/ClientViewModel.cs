namespace IPD.Web.ViewModels
{
    public class ClientViewModel
    {
        public Guid PatientID { get; set; }
        public string UHID { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string? MiddleName { get; set; }
        public string LastName { get; set; } = null!;
    }
}
