using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPD.Domain.Entities
{
    /// <summary>
    /// Contains details of Liquor.
    /// </summary>
    public class Liquor : BaseModel
    {
        /// <summary>
        /// Primary key of Liquor table.
        /// </summary>
        [Key]
        public Guid LiquorID { get; set; }

        /// <summary>
        /// Liquor Details of the Patients.
        /// </summary>
        [Required(ErrorMessage = "The LiquorDetails is required!")]
        [StringLength(30)]
        [Display(Name = "Liquor Details")]
        public string LiquorDetails { get; set; }

        /// <summary>
        /// Liquor measurement time of the patients.
        /// </summary>
        [Required(ErrorMessage = "The LiquorTime is required!")]
        [Column(TypeName = "BigInt")]
        [Display(Name = "Liquor Time")]
        public long LiquorTime { get; set; }

        /// <summary>
        /// Foreign key, Primary key of the Partograph table.
        /// </summary>
        public Guid PartographID { get; set; }

        [ForeignKey("PartographID")]
        public virtual Partograph Partograph { get; set; }
    }
}
