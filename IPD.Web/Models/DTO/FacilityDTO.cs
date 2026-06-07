using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPD.Web.Models.DTO
{
    public class FacilityDTO
    {
        /// <summary>
        /// Primary key of the table Facility.
        /// </summary>
        [Key]
        public int FacilityID { get; set; }
        /// <summary>
        /// Health facility name.
        /// </summary>
        [Required(ErrorMessage = "Please enter your Region Name!")]
        [StringLength(90)]
        [Display(Name = "Facility Name")]
        public string FacilityName { get; set; } = null!;
        /// <summary>
        /// Health facility code.
        /// </summary>
        [Required(ErrorMessage = " Facility Code is Required!")]
        [StringLength(20)]
        [Display(Name = "Facility Code")]
        public string FacilityCode { get; set; } = null!;
        /// <summary>
        /// Longitude of health facility.
        /// </summary>
        [StringLength(20)]
        [Display(Name = "Longitude")]
        public string? Longitude { get; set; }
        /// <summary>
        /// Latitude of health facility.
        /// </summary>
        [StringLength(20)]
        [Display(Name = "Latitude")]
        public string? Latitude { get; set; }
        /// <summary>
        /// Foreign key, Primary key of the table Region.
        /// </summary>
        [ForeignKey("RegionID")]
        public int RegionID { get; set; }
        /// <summary>
        /// Telephone number of the health facility.
        /// </summary>
        [StringLength(15)]
        [Display(Name = "Telephone")]
        public string? Telephone { get; set; }
        /// <summary>
        /// Instance of Region Table.
        /// </summary>
        
    }
}
