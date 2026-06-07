namespace IPD.Web.Models.DTO
{
    public class RequestForReferralDto
    {
        public PatientDetailsDto? PatientDetail { get; set; }
        public LocalReferralDto? LocalReferral { get; set; }
        public InternationalReferralDto? InternationalReferral { get; set;}
    }
}
