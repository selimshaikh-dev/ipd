using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPD.Domain.Entities
{
    /// <summary>
    /// Contains details of Cervix.
    /// </summary>
    public class Cervix : BaseModel
    {

        /// <summary>
        /// Primary key of Cervix table.
        /// </summary>
        [Key]
        public Guid CervixID { get; set; }

        /// <summary>
        /// Cervix Details of the Patients.
        /// </summary>
        [Required(ErrorMessage = "The CervixDetails is required!")]
        [Display(Name = "Cervix Details")]
        public int CervixDetails { get; set; }

        /// <summary>
        /// Cervix measurement time of the patients.
        /// </summary>
        [Required(ErrorMessage = "The CervixTime is required!")]
        [Column(TypeName = "BigInt")]
        [Display(Name = "Cervix Time")]
        public long CervixTime { get; set; }

        /// <summary>
        /// Foreign key, Primary key of the Partograph table.
        /// </summary>
        public Guid PartographID { get; set; }

        [ForeignKey("PartographID")]
        public virtual Partograph Partograph { get; set; }
    }
}
