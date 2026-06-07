using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPD.Web.Models.DTO
{
    public class DeathCertificateDTO
    {
        public Guid DeathCertificateID { get; set; }

        [Required(ErrorMessage = "The Indvuna is required!")]
        [StringLength(250)]
        [Display(Name = "Indvuna")]
        public string Indvuna { get; set; } = null!;

        [Required(ErrorMessage = "The Physical Address is required!")]
        [StringLength(1000)]
        [Display(Name = "Physical Address")]
        public string PhysicalAddress { get; set; } = null!;

        [Required(ErrorMessage = "The DateOfDeath is required!")]
        [Display(Name = "Date Of Death")]
        [Column(TypeName = "SmallDateTime")]
        public DateTime DateOfDeath { get; set; }

        [Required(ErrorMessage = "The TimeOfDeath is required!")]
        [Column(TypeName = "SmallDateTime")]
        [Display(Name = "Time Of Death")]
        public DateTime TimeOfDeath { get; set; }

        [StringLength(1000)]
        public string? CauseOfDeath { get; set; }

        [StringLength(1000)]
        public string? OtherReason { get; set; }

        [StringLength(1000)]
        public string? Interval { get; set; }

        [StringLength(1000)]
        public string? SpecialInvistigation { get; set; }

        [StringLength(1000)]
        public string? HandOn { get; set; }

        [StringLength(1000)]
        [Display(Name = "Hand Over To")]
        public string? HandOver { get; set; } = null!;

        [Required(ErrorMessage = "The ResidentOf is required!")]
        [StringLength(1000)]
        [Display(Name = "Resident Of")]
        public string? Resident { get; set; }

        public Guid AdmissionID { get; set; }
    }
}
