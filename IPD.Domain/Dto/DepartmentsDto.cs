using System.ComponentModel.DataAnnotations;

namespace IPD.Domain.Dto
{
    /// <summary>
    /// Contains details of department.
    /// </summary>
    public class DepartmentsDto
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
    }
}
