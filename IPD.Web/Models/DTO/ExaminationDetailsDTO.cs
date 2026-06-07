namespace IPD.Web.Models.DTO
{
    public class ExaminationDetailsDTO
    {
            public Guid ExaminationDetailID { get; set; }
            public int DigonosisExaminationID { get; set; }
            public Guid PatientExaminationID { get; set; }
            public string? FacilityName { get; set; }
            public DateTime? DateCreated { get; set; }

    }
}
