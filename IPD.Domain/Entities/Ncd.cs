using System.ComponentModel.DataAnnotations;

namespace IPD.Domain.Entities
{
    public class Ncd:BaseModel
    {
        /// <summary>
        /// Primary key of the table Ncds.
        /// </summary>
        [Key]
        public int NcdsID { get; set; }

        /// <summary>
        /// Name of the Ncds.
        /// </summary>
        [Required(ErrorMessage = "The NcdName is required!")]
        [StringLength(90)]
        [Display(Name = "NcdName")]
        public string NcdName { get; set; }
        public virtual IEnumerable<PatientsNcd>? PatientsNcds { get; set; } 
    }
}