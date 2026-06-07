using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPD.Domain.Entities
{
    /// <summary>
    /// Contains details of Acetones.
    /// </summary>
    public class Acetone : BaseModel
    {
        /// <summary>
        /// Primary key of Acetones table.
        /// </summary>
        [Key]
        public Guid AcetonesID { get; set; }

        /// <summary>
        /// Acetones Details of the Patients.
        /// </summary>
        [Required(ErrorMessage = "The AcetonesDetails is required!")]
        [StringLength(50)]
        [Display(Name = "Acetones Details")]
        public string AcetonesDetails { get; set; }

        /// <summary>
        /// Acetones measurement time of the patients.
        /// </summary>
        [Required(ErrorMessage = "The Time is required!")]
        [Column(TypeName = "BigInt")]
        [Display(Name = "Acetones Time")]
        public long AcetoneTime { get; set; }

        /// <summary>
        /// Foreign key, Primary key of the Partograph table.
        /// </summary>
        public Guid PartographID { get; set; }

        [ForeignKey("PartographID")]
        public virtual Partograph Partograph { get; set; }
    }
}
