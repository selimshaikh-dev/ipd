using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPD.Domain.Entities
{
    /// <summary>
    /// Contains details of delivery information.
    /// </summary>
    public class BirthDetail : BaseModel
    {
        /// <summary>
        /// Primary key of BirthDetails table.
        /// </summary>
        [Key]
        public Guid BirthDetailsID { get; set; }

        /// <summary>
        /// Foreign key, Primary key of the Admissions table.
        /// </summary>
        public Guid AdmissionID { get; set; }

        /// <summary>
        /// Check the delivery is successful or not
        /// </summary>
        [Display(Name = "Is successful delivery")]
        public byte IsSuccessfulDelivery { get; set; }

        /// <summary>
        /// Details of the birth history
        /// </summary>
        [StringLength(500)]
        [Display(Name = "Remarks")]
        public string Remarks { get; set; } = null!;

        /// <summary>
        /// Gender details of the patients
        /// </summary>       
        [Display(Name = "Gender")]
        public Byte Gender { get; set; }

        /// <summary>
        /// Weight of the baby at the time of birth
        /// </summary>
        [StringLength(15)]
        [Display(Name = "Weight")]
        public string Weight { get; set; } = null!;

        /// <summary>
        /// Check the delivery type
        /// </summary>
        [Display(Name = "Type of delivery")]
        public Byte TypeOfDelivery { get; set; }

        /// <summary>
        /// Record the delivery date of the baby
        /// </summary>        
        [Column(TypeName = "smalldatetime")]
        [Display(Name = "Birth Date")]
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// Record the delivery time of the baby
        /// </summary>
        [Column(TypeName = "smalldatetime")]
        [Display(Name = "Birth Time")]
        public DateTime? BirthTime { get; set; }

        [ForeignKey("AdmissionID")]
        public virtual Admission Admissions { get; set; }
    }
}