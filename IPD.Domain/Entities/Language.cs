using System.ComponentModel.DataAnnotations;

namespace IPD.Domain.Entities
{
    public class Language : BaseModel
    {
        /// <summary>
        /// Primary key of the table language
        /// </summary>
        [Key]
        public int LanguageID { get; set; }

        /// <summary>
        /// Name of the language.
        /// </summary>
        [Required(ErrorMessage = "The Language name is required!")]
        [StringLength(90)]
        [Display(Name = "Language name")]
        public string LanguageName { get; set; }
        

    }
}
