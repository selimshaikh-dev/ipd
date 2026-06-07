using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPD.Domain.Entities
{
    /// <summary>
    /// Contains details of Moulding.
    /// </summary>
    public class Moulding : BaseModel
    {
        /// <summary>
        /// Primary key of Moulding table.
        /// </summary>
        [Key]
        public Guid MouldingID { get; set; }

        /// <summary>
        /// Moulding Details of the Patients.
        /// </summary>
        [Required(ErrorMessage = "The MouldingDetails is required!")]
        [StringLength(30)]
        [Display(Name = "Moulding Details")]
        public string MouldingDetails { get; set; }

        /// <summary>
        /// Moulding measurement time of the patients.
        /// </summary>
        [Required(ErrorMessage = "The MouldingTime is required!")]
        [Column(TypeName = "BigInt")]
        [Display(Name = "Moulding Time")]
        public long MouldingTime { get; set; }

        /// <summary>
        /// Foreign key, Primary key of the Partograph table.
        /// </summary>
        public Guid PartographID { get; set; }

        [ForeignKey("PartographID")]
        public virtual Partograph Partograph { get; set; }
    }
}