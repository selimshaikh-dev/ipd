using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPD.Domain.Entities
{
    /// <summary>
    /// Contains details of Proteins.
    /// </summary>
    public class Protein : BaseModel
    {
        /// <summary>
        /// Primary key of Proteins table.
        /// </summary>
        [Key]
        public Guid ProteinsID { get; set; }

        /// <summary>
        /// Proteins Details of the Patients.
        /// </summary>
        [Required(ErrorMessage = "The ProteinsDetails is required!")]
        [StringLength(50)]
        [Display(Name = "Proteins Details")]
        public string ProteinsDetails { get; set; }

        /// <summary>
        /// Proteins measurement time of the patients.
        /// </summary>
        [Required(ErrorMessage = "The Time is required!")]
        [Column(TypeName = "BigInt")]
        [Display(Name = "Cervix Time")]
        public long ProteinsTime { get; set; }

        /// <summary>
        /// Foreign key, Primary key of the Partograph table.
        /// </summary>
        public Guid PartographID { get; set; }

        [ForeignKey("PartographID")]
        public virtual Partograph Partograph { get; set; }
    }
}
