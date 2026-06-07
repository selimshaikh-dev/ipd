using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPD.Domain.Entities
{
    /// <summary>
    /// Contains details of BloodPressure.
    /// </summary>
    public class BloodPressure : BaseModel
    {

        /// <summary>
        /// Primary key of BloodPressure table.
        /// </summary>
        [Key]
        public Guid BloodPressureID { get; set; }

        /// <summary>
        /// SystolicPressure of the Patient.
        /// </summary>
        [Required(ErrorMessage = "The Systolic Pressure is required!")]
        [Display(Name = "Systolic Pressure")]
        public int SystolicPressure { get; set; }

        /// <summary>
        /// DiastolicPressure of the Patient.
        /// </summary>
        [Required(ErrorMessage = "The Diastolic Pressure is required!")]
        [Display(Name = "Diastolic Pressure")]
        public int DiastolicPressure { get; set; }

        /// <summary>
        /// BloodPressure measurement time of the patients.
        /// </summary>
        [Required(ErrorMessage = "The Time is required!")]
        [Column(TypeName = "BigInt")]
        [Display(Name = "Time")]
        public long BloodPressureTime { get; set; }

        /// <summary>
        /// Foreign key, Primary key of the Partograph table.
        /// </summary>
        public Guid PartographID { get; set; }

        [ForeignKey("PartographID")]
        public virtual Partograph Partograph { get; set; }
    }
}
