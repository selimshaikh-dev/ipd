using System.ComponentModel.DataAnnotations;

namespace IPD.Domain.Entities
{
    /// <summary>
    /// Contains details of DischargeStatuses.
    /// </summary>
    public class DischargeStatus : BaseModel
    {
        /// <summary>
        /// Primary key of DischargeStatuses table.
        /// </summary>
        [Key]
        public Guid DischargeStatusID { get; set; }

        /// <summary>
        /// Name of the DischargeStatus
        /// </summary>
        [Required(ErrorMessage = "Discharges status is required!")]
        [StringLength(90)]
        [Display(Name = "Discharges status")]
        public string DischargesStatus { get; set; }
        public virtual List<Discharge>? Discharges { get; set; }
    }
}
