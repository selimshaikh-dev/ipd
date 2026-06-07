namespace IPD.Domain.Dto
{
    public class PatientsNcdDto
    {
        public Guid PatientNcdsID { get; set; }
        public int NcdsID { get; set; }
        public string NcdsName { get; set; }
        public Guid ComplaintID { get; set; }
    }
}
