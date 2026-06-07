using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPD.Domain.Dto
{
    /// <summary>
    /// Contains details of deathCertificate.
    /// </summary>
    public class DeathCertificateDto
    {
        /// <summary>
        /// Primary key of the table deathCertificate.
        /// </summary>
        [Key]
        public Guid DeathCertificateID { get; set; }

        /// <summary>
        /// The indvuna name of the patients
        /// </summary>
        [Required(ErrorMessage = "The indvuna is required!")]
        [StringLength(250)]
        [Display(Name = "Indvuna")]
        public string Indvuna { get; set; } = null!;

        /// <summary>
        /// The address of the patients
        /// </summary>
        [Required(ErrorMessage = "The physical address is required!")]
        [StringLength(1000)]
        [Display(Name = "Physical address")]
        public string PhysicalAddress { get; set; } = null!;

        /// <summary>
        /// The date of death patients
        /// </summary>
        [Required(ErrorMessage = "The Date of Death is required!")]
        [Display(Name = "Date of death")]
        [Column(TypeName = "SmallDateTime")]
        public DateTime DateOfDeath { get; set; }

        /// <summary>
        /// Time of death of the patient
        /// </summary>
        [Required(ErrorMessage = "The TimeOfDeath is required!")]
        [Column(TypeName = "SmallDateTime")]
        [Display(Name = "Time Of Death")]
        public DateTime TimeOfDeath { get; set; }

        /// <summary>
        /// Cause of death 
        /// </summary>
        [StringLength(1000)]
        public string? CauseOfDeath { get; set; }

        /// <summary>
        /// Contributing/antecendent cause or illness
        /// </summary>
        [StringLength(1000)]
        public string? OtherReason { get; set; }

        /// <summary>
        /// Approximate interval between onset and death
        /// </summary>
        [StringLength(1000)]
        public string? Interval { get; set; }

        /// <summary>
        /// Special invistigation, pathological examin intention of post mortem etc
        /// </summary>
        [StringLength(1000)]
        public string? SpecialInvistigation { get; set; }

        /// <summary>
        /// Done under my hand on this
        /// </summary>
        [StringLength(1000)]
        public string? HandOn { get; set; }

        /// <summary>
        /// The specific name to whom handover 
        /// </summary>
        [StringLength(1000)]
        [Display(Name = "Hand over to")]
        public string? HandOver { get; set; } = null!;

        /// <summary>
        /// The resident of the patient
        /// </summary>
        [Required(ErrorMessage = "The resident of is required!")]
        [StringLength(1000)]
        [Display(Name = "Resident of")]
        public string? Resident { get; set; }

        public Guid AdmissionID { get; set; }
    }
}
