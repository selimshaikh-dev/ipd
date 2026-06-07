using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPD.Domain.Entities
{
    /// <summary>
    /// Contains details of Pulse.
    /// </summary>
    public class Pulse : BaseModel
    {
        /// <summary>
        /// Primary key of Pulse table.
        /// </summary>
        [Key]
        public Guid PulseID { get; set; }

        /// <summary>
        /// Pulse Details of the Patients.
        /// </summary>
        [Required(ErrorMessage = "The PulseDetails is required!")]
        [Display(Name = "Pulse Details")]
        public int PulseDetails { get; set; }

        /// <summary>
        /// Pulse measurement time of the patients.
        /// </summary>
        [Required(ErrorMessage = "The Time is required!")]
        [Column(TypeName = "BigInt")]
        [Display(Name = "Pulse Time")]
        public long PulseTime { get; set; }

        /// <summary>
        /// Foreign key, Primary key of the Partograph table.
        /// </summary>
        public Guid PartographID { get; set; }

        [ForeignKey("PartographID")]
        public virtual Partograph Partograph { get; set; }
    }
}
