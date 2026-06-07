using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPD.Domain.Dto
{
    public class InterDepartmentReferralDto
    {
        public Guid InterDepartmentReferralsID { get; set; }
        public Guid DepartmentID { get; set; }

        [Required(ErrorMessage = "The ReferralTo is required!")]
        public string ReferralTo { get; set; } = null!;
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }

        [Required(ErrorMessage = "The Ward is required!")]
        public string Ward { get; set; } = null!;

        [Required(ErrorMessage = "The Reason Of Referral is required!")]
        public string ReasonOfReferral { get; set; } = null!;

        [Required(ErrorMessage = "The Referral Officer is required!")]
        public string ReferralOfficer { get; set; } = null!;
        public string? Feedback { get; set; }
        public string? ConsultingOfficer { get; set; }
        public Guid AdmissionID { get; set; }
    }
}
