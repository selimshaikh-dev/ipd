using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPD.Domain.Entities
{
    /// <summary>
    /// Contains details of Contractions.
    /// </summary>
    public class Contraction : BaseModel
    {
        /// <summary>
        /// Primary key of Contractions table.
        /// </summary>
        [Key]
        public Guid ContractionsID { get; set; }

        /// <summary>
        /// Contractions Details of the Patients.
        /// </summary>
        [Required(ErrorMessage = "The ContractionsDetails is required!")]
        [Display(Name = "Contractions Details")]
        public int ContractionsDetails { get; set; }

        /// <summary>
        /// Contractions measurement time of the patients.
        /// </summary>
        [Required(ErrorMessage = "The ContractionsTime is required!")]
        [Column(TypeName = "BigInt")]
        [Display(Name = "Contractions Time")]
        public long ContractionsTime { get; set; }

        /// <summary>
        /// Contractions duration of the patient.
        /// </summary>
        [Required(ErrorMessage = "The Duration of contraction is required!")]
        [StringLength(200)]
        [Display(Name = "Duration")]
        public string Duration { get; set; }

        /// <summary>
        /// Foreign key, Primary key of the Partograph table.
        /// </summary>
        public Guid PartographID { get; set; }

        [ForeignKey("PartographID")]
        public virtual Partograph Partograph { get; set; }
    }
}
