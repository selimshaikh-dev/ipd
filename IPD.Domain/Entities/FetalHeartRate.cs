using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPD.Domain.Entities
{
    /// <summary>
    /// Contains details of FetalHeartRates.
    /// </summary>
    public class FetalHeartRate : BaseModel
    {
        /// <summary>
        /// Primary key of FetalHeartRates table.
        /// </summary>
        [Key]
        public Guid FetalHeartRateID { get; set; }

        /// <summary>
        /// Fetal Rate description of the baby.
        /// </summary>
        [Required(ErrorMessage = "The FetalRate is required!")]
        [Display(Name = "Fetal Rate")]
        public int FetalRate { get; set; }

        /// <summary>
        /// Fatal rate measurement time of the baby.
        /// </summary>
        [Required(ErrorMessage = "The FetalRateTime is required!")]
        [Column(TypeName = "BigInt")]
        [Display(Name = "Fetal Rate Time")]
        public long FetalRateTime { get; set; }

        /// <summary>
        /// Foreign key, Primary key of the Partograph table.
        /// </summary>
        public Guid PartographID { get; set; }

        [ForeignKey("PartographID")]
        public virtual Partograph Partograph { get; set; }

    }
}
