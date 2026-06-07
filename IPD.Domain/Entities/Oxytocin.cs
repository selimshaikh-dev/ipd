using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPD.Domain.Entities
{
    /// <summary>
    /// Contains details of Oxytocin.
    /// </summary>
    public class Oxytocin : BaseModel
    {
        /// <summary>
        /// Primary key of Oxytocin table.
        /// </summary>
        [Key]
        public Guid OxytocinID { get; set; }

        /// <summary>
        /// Oxytocin Details of the Patients.
        /// </summary>
        [Required(ErrorMessage = "The OxytocinDetails is required!")]
        [Display(Name = "Oxytocin Details")]
        public int OxytocinDetails { get; set; }

        /// <summary>
        /// Oxytocin measurement time of the patients.
        /// </summary>
        [Required(ErrorMessage = "The OxytocinTime is required!")]
        [Column(TypeName = "BigInt")]
        [Display(Name = "Oxytocin Time")]
        public long OxytocinTime { get; set; }

        /// <summary>
        /// Foreign key, Primary key of the Partograph table.
        /// </summary>
        public Guid PartographID { get; set; }

        [ForeignKey("PartographID")]
        public virtual Partograph Partograph { get; set; }
    }
}
