using System.ComponentModel.DataAnnotations;

namespace IPD.Domain.Entities
{
    /// <summary>
    /// Contains details of department.
    /// </summary>
    public class Department: BaseModel
    {
        /// <summary>
        /// Primary key of the table departments.
        /// </summary>
        [Key]
        public Guid DepartmentID { get; set; }

        /// <summary>
        /// Name of the departments table.
        /// </summary>
        [Required(ErrorMessage = "The department name is required!")]
        [StringLength(90)]
        [Display(Name = "Department name")]
        public string DepartmentName { get; set; } = null!;
        public virtual List<InterDepartmentReferral>? InterDepartmentReferrals { get; set; }
    }
}
