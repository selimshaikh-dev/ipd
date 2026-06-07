using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPD.Domain.Entities
{
    /// <summary>
    /// Contains details of Medicines.
    /// </summary>
    public class Medicine : BaseModel
    {
        /// <summary>
        /// Primary key of Medicines table.
        /// </summary>
        [Key]
        public Guid MedicinesID { get; set; }

        /// <summary>
        /// Medicines name that given to the patients.
        /// </summary>
        [Required(ErrorMessage = "The MedicinesName is required!")]
        [StringLength(200)]
        [Display(Name = "Medicines Name")]
        public string MedicinesName { get; set; }

        /// <summary>
        /// Medicine given time to the Patients.
        /// </summary>
        [Required(ErrorMessage = "The Time is required!")]
        [Column(TypeName = "BigInt")]
        [Display(Name = "Time")]
        public long MedicinesTime { get; set; }

        /// <summary>
        /// Foreign key, Primary key of the Partograph table.
        /// </summary>
        public Guid PartographID { get; set; }

        [ForeignKey("PartographID")]
        public virtual Partograph Partograph { get; set; }
    }
}
