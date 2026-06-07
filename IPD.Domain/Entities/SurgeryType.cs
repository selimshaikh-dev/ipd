using System.ComponentModel.DataAnnotations;
namespace IPD.Domain.Entities
{
    /// <summary>
    /// Represents SurgeryType entity in the database.
    /// </summary>
    public class SurgeryType :BaseModel
    {
        /// <summary>
        /// Primary key of the table SurgeryType,
        /// </summary>
        [Key]
        public int SurgeryTypeID { get; set; }

        /// <summary>
        /// Surgery type name.
        /// </summary>
        [Required(ErrorMessage = "The Type Name is required!")]
        [StringLength(90)]
        [Display(Name = "Type Name")]
        public string TypeName { get; set; }
        public virtual List<Surgery>? Surgeries { get; set; }
    }
}
