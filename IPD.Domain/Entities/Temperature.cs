using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPD.Domain.Entities
{
    /// <summary>
    /// Contains details of Temperatures.
    /// </summary>
    public class Temperature : BaseModel
    {
        /// <summary>
        /// Primary key of Temperatures table.
        /// </summary>
        [Key]
        public Guid TemperaturesID { get; set; }

        /// <summary>
        /// Temperatures Details of the Patients.
        /// </summary>
        [Required(ErrorMessage = "The TemperaturesDetails is required!")]
        [Display(Name = "Temperatures Details")]
        public int TemperaturesDetails { get; set; }

        /// <summary>
        /// Temperatures measurement time of the patients.
        /// </summary>
        [Required(ErrorMessage = "The Time is required!")]
        [Column(TypeName = "BigInt")]
        [Display(Name = "Temperatures Time")]
        public long TemperatureTime { get; set; }

        /// <summary>
        /// Foreign key, Primary key of the Partograph table.
        /// </summary>
        public Guid PartographID { get; set; }

        [ForeignKey("PartographID")]
        public virtual Partograph Partograph { get; set; }
    }
}
