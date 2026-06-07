namespace IPD.Web.Models.DTO
{
    public class InterReferralDto
    {
        public Guid InterDepartmentReferralsID { get; set; }

        public Guid DepartmentID { get; set; }

        public string ReferralTo { get; set; } = null!;

        public DateTime Date { get; set; }

        public DateTime Time { get; set; }

        public string Ward { get; set; } = null!;

        public string ReasonOfReferral { get; set; } = null!;

        public string ReferralOfficer { get; set; } = null!;

        public string? Feedback { get; set; }

        public string? ConsultingOfficer { get; set; }

        public Guid AdmissionID { get; set; }
    }
}
