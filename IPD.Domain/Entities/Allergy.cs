using System.ComponentModel.DataAnnotations;

namespace IPD.Domain.Entities
{
    /// <summary>
    /// The Allergies table holds the name of all Allergies of the patients
    /// </summary>
    public class Allergy :BaseModel
    {
        /// <summary>
        /// Primary key of the table Allergy.
        /// </summary>
        [Key]
        public int AllergiesID { get; set; }

        /// <summary>
        /// Name of the Allergy.
        /// </summary>
        [Required(ErrorMessage = "The Allergies Name is required!")]
        [StringLength(90)]
        [Display(Name = "Allergies Name")]
        public string AllergiesName { get; set; }
        public virtual IEnumerable<PatientAllergy>? PatientAllergy { get; set; }
    }
}
