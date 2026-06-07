using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPD.Domain.Entities
{
    /// <summary>
    /// Contains details of chiefdoms.
    /// </summary>
    public class Chiefdom : BaseModel
    {
        /// <summary>
        /// Primary key of the table chiefdom.
        /// </summary>
        [Key]
        public int ChiefdomID { get; set; }

        /// <summary>
        /// Name of the chiefdom.
        /// </summary>
        [Required(ErrorMessage = "Chiefdom name is required!")]
        [StringLength(90)]
        [Display(Name = "Name")]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Forengn key, Primary key of the table Tinkhundla.
        /// </summary>
        [Required(ErrorMessage = "The tinkhundla ID is required!")]
        [Display(Name = "Tinkhundla ID")]
        public int TinkhundlaID { get; set; }

        [ForeignKey("TinkhundlaID")]
        public virtual Tinkhundla? Tinkhundla { get; set; }
        public virtual IEnumerable<Patient>? Patients { get; set; }
    }
}
