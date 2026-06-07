using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPD.Domain.Entities
{
    /// <summary>
    /// Contains details of DescentOfHead.
    /// </summary>
    public class DescentOfHead : BaseModel
    {
        /// <summary>
        /// Primary key of DescentOfHead table.
        /// </summary>
        [Key]
        public Guid DescentOfHeadID { get; set; }

        /// <summary>
        /// DescentOfHead Details of the Patients.
        /// </summary>
        [Required(ErrorMessage = "The DescentOfHead is required!")]
        [StringLength(30)]
        [Display(Name = "Descent Of Head")]
        public int DescentOfHeadDetails { get; set; }

        /// <summary>
        /// Cervix measurement time of the patients.
        /// </summary>
        [Required(ErrorMessage = "The DescentOfHead is required!")]
        [Column(TypeName = "BigInt")]
        [Display(Name = "Descent Of Head Time")]
        public long DescentOfHeadTime { get; set; }

        /// <summary>
        /// Foreign key, Primary key of the Partograph table.
        /// </summary>
        public Guid PartographID { get; set; }

        [ForeignKey("PartographID")]
        public virtual Partograph Partograph { get; set; }
    }
}
