using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPD.Domain.Dto
{
    /// <summary>
    /// Represents diabeticProfiles entity in the database.
    /// </summary>
    public class DiabeticsProfileDto
    {
        /// <summary>
        /// Primary key of the diabetic profiles entity.
        /// </summary>
        [Key]
        public Guid DiabeticProfileID { get; set; }

        /// <summary>
        /// Date of the diabetic profile.
        /// </summary>
        [Required(ErrorMessage = "Date collected is required.")]
        [Column(TypeName = "smalldatetime")]
        [Display(Name = "Date collected")]
        public DateTime DateCollected { get; set; }

        /// <summary>
        /// Time of the diabetic profile.
        /// </summary>
        [Required(ErrorMessage = "Time collected is required.")]
        [Column(TypeName = "smalldatetime")]
        [Display(Name = "Time collected")]
        public DateTime TimeCollected { get; set; }

        /// <summary>
        /// Unit is mmol/L (millimoles per liter)
        /// </summary>
        [Required(ErrorMessage = "Blood suger is required.")]
        [Display(Name = "Blood suger")]
        public decimal BloodSuger { get; set; }

        /// <summary>
        /// Unit is mmol/L (millimoles per liter)
        /// </summary>
        [Required(ErrorMessage = "Urin suger is required.")]
        [Display(Name = "Urin suger")]
        public decimal UrinSuger { get; set; }

        /// <summary>
        /// Less than 0.6 = normal. 0.6 - 1.0 = slightly high. 1.0 - 3.0 = moderately high. Higher than 3.0 = very high.
        /// </summary>
        [Required(ErrorMessage = "Urin ketones is required.")]
        [Display(Name = "Urin ketones")]
        public decimal UrinKetones { get; set; }

        /// <summary>
        /// Dose for the insulin.
        /// </summary>
        [Required(ErrorMessage = "Insulin dose is required.")]
        [Display(Name = "Insulin dose")]
        public decimal InsulinDose { get; set; }

        /// <summary>
        /// Code of the health facility.
        /// </summary>
        [StringLength(20)]
        [Display(Name = "Facility code")]
        public string? FacilityCode { get; set; }
        public Guid AdmissionID { get; set; }
        public DateTime? DateCreated { get; set; }
        public string? FacilityName { get; set; }
    }
}
