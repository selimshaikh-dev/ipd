namespace IPD.Domain.Dto
{
    public class PatientDetailDto
    {
        public Guid PatientDetailsID { get; set; }
        public string PassportNumber { get; set; }
        public string IDNumber { get; set; }
        public string Language { get; set; }
        public string Occupation { get; set; }
        public string Relegion { get; set; }
        public string Employer { get; set; }
        public string Allergies { get; set; }
        public string ChronicIllness { get; set; }
        public string Medication { get; set; }
        public Guid AdmissionID { get; set; }
        public int RegionID { get; set; }

    }
}
