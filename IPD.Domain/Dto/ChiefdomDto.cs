using System.ComponentModel.DataAnnotations;

namespace IPD.Domain.Dto
{
    /// <summary>
    /// Contains details of chiefdoms.
    /// </summary>
    public class ChiefdomDto
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
        /// Forengn key, primary key of the table tinkhundla.
        /// </summary>
        [Required(ErrorMessage = "The tinkhundla ID is required!")]
        [Display(Name = "Tinkhundla ID")]
        public int TinkhundlaID { get; set; }
    }
}
