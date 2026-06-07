using System.ComponentModel.DataAnnotations;

namespace IPD.Domain.Entities
{
    public class Procedure :BaseModel
    {
        /// <summary>
        /// Primary key of the table procedure.
        /// </summary>
        [Key]
        public int ProcedureID { get; set; }

        /// <summary>
        /// Name of the procedure.
        /// </summary>
        [Required(ErrorMessage = "The Procedure name is required!")]
        [StringLength(90)]
        [Display(Name = "Procedure name")]
        public string ProcedureName { get; set; }
        public virtual List<InternationalReferral>? InternationalReferrals { get; set; }
    }
}
