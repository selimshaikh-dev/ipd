using System.ComponentModel.DataAnnotations;

namespace IPD.Web.Models.DTO
{
    public class TreatmentPlansDto
    {
        public Guid TreatmentPlanID { get; set; }
        public string TreatementPlanDetails { get; set; }
        public Guid AdmissionID { get; set; }
        public DateTime? DateCreated { get; set; }
        public string? FacilityName { get; set; }
    }
}
