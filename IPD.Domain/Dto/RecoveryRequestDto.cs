using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPD.Domain.Dto
{
    public class RecoveryRequestDto
    {
        public string CellPhone { get; set; }
        public string? UserName { get; set; }
        public string? NationaliD { get; set; }
    }
}
