using System.ComponentModel.DataAnnotations;

namespace IPD.Domain.Entities
{
    /// <summary>
    /// The Regions table holds the name of all regions of Eswatini.
    /// </summary>
    public class Region : BaseModel
    {
        /// <summary>
        /// Primary key of the table Region.
        /// </summary>
        [Key]
        public int RegionID { get; set; }

        /// <summary>
        /// Region name.
        /// </summary>
        [Required(ErrorMessage = "Please enter your Region Name!")]
        [StringLength(90)]
        [Display(Name = "Region Name")]
        public string RegionName { get; set; } = null!;
        public List<Facility>? Facilities { get; set; }
        public virtual List<InternationalReferral>? InternationalReferrals { get; set; }
    }
}
