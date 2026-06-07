using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPD.Domain.Entities
{
    /// <summary>
    /// Contains details of Volumes.
    /// </summary>
    public class Volume : BaseModel
    {
        /// <summary>
        /// Primary key of Acetones table.
        /// </summary>
        [Key]
        public Guid VolumesID { get; set; }

        /// <summary>
        /// Volumes Details of the Patients.
        /// </summary>
        [Required(ErrorMessage = "The VolumesDetails is required!")]
        [StringLength(50)]
        [Display(Name = "Volumes Details")]
        public string VolumesDetails { get; set; }

        /// <summary>
        /// Volumes measurement time of the patients.
        /// </summary>
        [Required(ErrorMessage = "The Time is required!")]
        [Column(TypeName = "BigInt")]
        [Display(Name = "Acetones Time")]
        public long VolumesTime { get; set; }

        /// <summary>
        /// Foreign key, Primary key of the Partograph table.
        /// </summary>
        public Guid PartographID { get; set; }

        [ForeignKey("PartographID")]
        public virtual Partograph Partograph { get; set; }
    }
}