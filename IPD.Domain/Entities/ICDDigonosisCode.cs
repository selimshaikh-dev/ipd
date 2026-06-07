using System.ComponentModel.DataAnnotations;

namespace IPD.Domain.Entities
{
    /// <summary>
    /// The ICDDigonosisCodes table holds the name of all ICD digonosis codes
    /// </summary>
    public class ICDDigonosisCode:BaseModel
    {
        /// <summary>
        /// Primary key of the table ICDDigonosisCodes.
        /// </summary>
        [Key]
        public int DiseaseID { get; set; }

        /// <summary>
        /// Code of the digonosis provided by WHO.
        /// </summary>
        [Required(ErrorMessage = "The ICDCode is required!")]
        [StringLength(30)]
        [Display(Name = "ICDCode")]
        public string ICDCode { get; set; }

        /// <summary>
        /// Description of the ICD code.
        /// </summary>
        [Required(ErrorMessage = "The Description is required!")]
        [StringLength(500)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        /// <summary>
        /// Root name of the disease id.
        /// </summary>
        public int ParentsID { get; set; }
        public virtual IEnumerable<DiagonosisDetail>? DiagonosisDetails { get; set; }
    }
}
