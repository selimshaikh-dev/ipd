using System.ComponentModel.DataAnnotations;

namespace IPD.Domain.Dto
{
    /// <summary>
    /// The allergies table holds the name of all allergies of the patients.
    /// </summary>
    public class AllergiesDto
    {
        /// <summary>
        /// Primary key of the table Allergy.
        /// </summary>
        [Key]
        public int AllergiesID { get; set; }

        /// <summary>
        /// Name of the allergy.
        /// </summary>
        [Required(ErrorMessage = "The allergies Name is required!")]
        [StringLength(90)]
        [Display(Name = "Allergies Name")]
        public string AllergiesName { get; set; }
    }
}
