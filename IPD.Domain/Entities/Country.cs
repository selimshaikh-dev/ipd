using System.ComponentModel.DataAnnotations;

namespace IPD.Domain.Entities
{
    /// <summary>
    /// Contains details of countries.
    /// </summary>
    public class Country : BaseModel
    {
        /// <summary>
        /// Primary key of the table countries.
        /// </summary>
        [Key]
        public int CountryID { get; set; }

        /// <summary>
        /// Country name.
        /// </summary>
        [Required(ErrorMessage = "Country name is required!")]
        [StringLength(90)]
        [Display(Name = "Name")]
        public string Name { get; set; } = null!;
        public virtual IEnumerable<Patient>? Patients { get; set; }
    }
}
