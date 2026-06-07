using System.ComponentModel.DataAnnotations;

namespace IPD.Domain.Dto
{
    public class ReportPickerDto
    {
        [Required(ErrorMessage = "Required!")]
        [Display(Name = "Date from")]
        public DateTime DateFrom { get; set; }

        [Required(ErrorMessage = "Required!")]
        [Display(Name = "Date to")]
        public DateTime DateTo { get; set; }
    }
}