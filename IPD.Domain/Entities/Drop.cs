using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPD.Domain.Entities
{
    /// <summary>
    /// Contains details of Drops.
    /// </summary>
    public class Drop : BaseModel
    {
        /// <summary>
        /// Primary key of Drops table.
        /// </summary>
        [Key]
        public Guid DropsID { get; set; }

        /// <summary>
        /// Drops Details of the Patients.
        /// </summary>
        [Required(ErrorMessage = "The DropsDetails is required!")]
        [Display(Name = "Drops Details")]
        public int DropsDetails { get; set; }

        /// <summary>
        /// Drops measurement time of the patients.
        /// </summary>
        [Required(ErrorMessage = "The DropsTime is required!")]
        [Column(TypeName = "BigInt")]
        [Display(Name = "Drops Time")]
        public long DropsTime { get; set; }

        /// <summary>
        /// Foreign key, Primary key of the Partograph table.
        /// </summary>
        public Guid PartographID { get; set; }

        [ForeignKey("PartographID")]
        public virtual Partograph Partograph { get; set; }
    }
}
