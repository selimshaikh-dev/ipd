using System.ComponentModel.DataAnnotations;

namespace IPD.Domain.Entities
{
    /// <summary>
    /// Contains details of Tinkhundla.
    /// </summary>
    public class Tinkhundla : BaseModel
    {
        /// <summary>
        /// Primary key of the table Tinkhundla.
        /// </summary>
        [Key]
        public int TinkhundlaID { get; set; }

        /// <summary>
        /// Inkundla name.
        /// </summary>
        [Required(ErrorMessage = "The Name is required!")]
        [StringLength(90)]
        [Display(Name = "Name")]
        public string Name { get; set; } = null!;
        public virtual IEnumerable<Chiefdom>? Chiefdoms { get; set; }
    }
}
