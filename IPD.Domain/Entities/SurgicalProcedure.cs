using System.ComponentModel.DataAnnotations;
namespace IPD.Domain.Entities
{
    /// <summary>
    ///  Represents surgicalprocedure entity in the database.
    /// </summary>
    public class SurgicalProcedure :BaseModel
    {
        /// <summary>
        /// Primary key of the table surgicalprocedure,
        /// </summary>
        [Key]
        public int SurgicalProcedureID { get; set; }

        /// <summary>
        /// Surgery type name.
        /// </summary>
        [Required(ErrorMessage = "The Procedure Name is required!")]
        [StringLength(90)]
        [Display(Name = "Procedure Name")]
        public string ProcedureName { get; set; } 
        public virtual List<Surgery>? Surgeries { get; set; }
    }
}
