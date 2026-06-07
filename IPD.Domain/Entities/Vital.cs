using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPD.Domain.Entities
{
    /// <summary>
    /// Represents vitels entity in the database.
    /// </summary>
    public class Vital : BaseModel
    {
        /// <summary>
        /// Primary key of the vitals entity.
        /// </summary>
        [Key]
        public Guid VitalID { get; set; }

        /// <summary>
        /// Date of vital collection.
        /// </summary>
        [Required(ErrorMessage = "Date collected is required.")]
        [Column(TypeName = "smalldatetime")]
        [Display(Name = "Date collected")]
        public DateTime DateCollected { get; set; }

        /// <summary>
        /// Time of vital collection.
        /// </summary>
        [Required(ErrorMessage = "Time collected is required.")]
        [Column(TypeName = "smalldatetime")]
        [Display(Name = "Time collected")]
        public DateTime TimeCollected { get; set; }

        /// <summary>
        /// Weight of the patient.
        /// </summary>
        [Required(ErrorMessage = "Weight is required.")]
        [Range(2, 200, ErrorMessage = "Weight should be between 2kg and 200kg")]
        [Display(Name = "Weight")]
        public decimal Weight { get; set; }

        /// <summary>
        /// Height of the patient.
        /// </summary>        
        [Range(10, 250, ErrorMessage = "Height should be between 10cm and 250cm")]
        [Display(Name = "Height")]
        public decimal? Height { get; set; }

        /// <summary>
        /// Temperature of the patient.
        /// </summary>
        [Required(ErrorMessage = "Temperature is required.")]
        [Range(20, 60, ErrorMessage = "Temperature should be between 20 and 60 degrees celsius")]
        [Display(Name = "Temperature")]
        public decimal Temperature { get; set; }

        /// <summary>
        /// MUAC of the patient.
        /// </summary>
        [Range(5, 60, ErrorMessage = "MUAC should be between 5cm and 60cm")]
        [Display(Name = "MUAC")]
        public decimal? MUAC { get; set; }

        /// <summary>
        /// Systolic blood pressure of the patient.
        /// </summary>
        [Required(ErrorMessage = "Systolic is required.")]
        [Range(50, 350, ErrorMessage = "Systolic should be between 50mmHg and 350mmHg")]
        [Display(Name = "Systolic")]    
        public short Systolic { get; set; }

        /// <summary>
        /// Diastolic blood pressure of the patient.
        /// </summary>
        [Required(ErrorMessage = "Diastolic is required.")]
        [Range(30, 150, ErrorMessage = "Diastolic should be between 40mmHg and 150mmHg")]
        [Display(Name = "Diastolic")]       
        public short Diastolic { get; set; }

        /// <summary>
        /// Patient's respiratory rate.
        /// </summary>
        [Range(5, 300, ErrorMessage = "Respiratory rate should be between 5 and 300 breaths per minute")]
        [Display(Name = "Respiratory rate")]        
        public short? RespiratoryRate { get; set; }

        /// <summary>
        /// Pulse of the patient.
        /// </summary>
        [Required(ErrorMessage = "Pulse is required.")]
        [Range(20, 400, ErrorMessage = "Pulse should be between 20bpm and 400bpm")]
        [Display(Name = "Pulse")]
        public short Pulse { get; set; }

        /// <summary>
        /// Patient's oxigen saturation.
        /// </summary>        
        [Range(50, 100, ErrorMessage = "Oxygen saturation should be between 50% and 100%")]
        [Display(Name = "Oxygen saturation")]
        public short? OxygenSaturation { get; set; }

        /// <summary>
        /// Patient's calculated BMI.
        /// </summary>
        [Display(Name = "BMI")]
        public decimal? BMI { get; set; }

        /// <summary>
        /// Patient's nutritional status.
        /// </summary>
        [StringLength(90)]
        [Display(Name = "Nutritional status")]
        public string? NutritionalStatus { get; set; }

        /// <summary>
        /// Patient's other vitals collected in any special situation.
        /// </summary>
        [StringLength(1000)]
        [Display(Name = "Other vitals")]
        public string? OtherVitals { get; set; }

        /// <summary>
        /// Code of the health facility.
        /// </summary>
        [StringLength(20)]
        [Display(Name = "Facility code")]
        public string? FacilityCode { get; set; }

        /// <summary>
        /// Foreignkey, Primary key of the table admissions.
        /// </summary>
        public Guid AdmissionID { get; set; }
        [ForeignKey("AdmissionID")]
        public virtual Admission Admission { get; set; }
    }
}