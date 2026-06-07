namespace IPD.Domain.Dto
{
    public class ExaminationDetailsDto
    {
        #region Public Properties
        public Guid PatientExaminationID { get; set; }
        public int DigonosisExaminationID { get; set; }
        public string DigonosisExaminationName { get; set; } = string.Empty;
        public Guid ExaminationDetailID { get; set; }
        public string? FacilityName { get; set; }
        public DateTime? DateCreated { get; set; }

        #endregion Public Properties
    }
}
