using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPD.Domain.Entities
{
    /// <summary>
    /// The PartographDetails table holds the details of partograph   
    /// </summary>
    public class PartographDetail : BaseModel
    {
        /// <summary>
        ///  Primary Key of the table PartographDetails.
        /// </summary>
        [Key]
        public Guid PartographDetailsID { get; set; }
        /// <summary>
        /// Foreign Key, Primary Key of the table partograph. 
        /// </summary>
        public Guid PartographID { get; set; }

        /// <summary>
        /// Description about the liquor.
        /// </summary>
        [Required(ErrorMessage = "The liquor is required!")]
        [StringLength(150)]
        [Display(Name = "liquor")]
        public string Liquor { get; set; }

        /// <summary>
        /// Time of liquor of the patients.
        /// </summary>
        [Required(ErrorMessage = "The Liquor time is required!")]
        [Column(TypeName = "SmallDateTime")]
        [Display(Name = "Liquor time")]
        public DateTime LiquorTime { get; set; }

        /// <summary>
        /// Description about the moulding.
        /// </summary>
        [Required(ErrorMessage = "The moulding is required!")]
        [StringLength(150)]
        [Display(Name = "moulding")]
        public string Moulding { get; set; }

        /// <summary>
        /// Taken time of moulding.
        /// </summary>
        [Required(ErrorMessage = "The moulding time is required!")]
        [Column(TypeName = "SmallDateTime")]
        [Display(Name = "Moulding time")]
        public DateTime MouldingTime { get; set; }

        /// <summary>
        /// Description about the cervix of the patients.
        /// </summary>
        [Required(ErrorMessage = "The cervix is required!")]
        [StringLength(150)]
        [Display(Name = "Cervix")]
        public string Cervix { get; set; }

        /// <summary>
        /// Cervix taken time of patients.
        /// </summary>
        [Required(ErrorMessage = "The cervix time is required!")]
        [Column(TypeName = "SmallDateTime")]
        [Display(Name = "Cervix time")]
        public DateTime CervixTime { get; set; }

        /// <summary>
        /// Description about the Descent of head.
        /// </summary>
        [Required(ErrorMessage = "The descent of head is required!")]
        [Display(Name = "Descent of head")]
        public int DescentOfHead { get; set; }

        /// <summary>
        /// Descent of head taken time of patients.
        /// </summary>
        [Required(ErrorMessage = "The descent of head time is required!")]
        [Column(TypeName = "SmallDateTime")]
        [Display(Name = "Descent of head time")]
        public DateTime DescentOfHeadTime { get; set; }

        /// <summary>
        /// Contractions of the patients.
        /// </summary>
        [Required(ErrorMessage = "The contractions is required!")]
        [Display(Name = "Contractions")]
        public int Contractions { get; set; }

        /// <summary>
        /// Description about the contractions duration.
        /// </summary>
        [Required(ErrorMessage = "The contractions duration is required!")]
        [StringLength(150)]
        [Display(Name = "Contractions duration")]
        public string ContractionsDuration { get; set; }

        /// <summary>
        /// Contractions taken time.
        /// </summary>
        [Required(ErrorMessage = "The contractions time is required!")]
        [Column(TypeName = "SmallDateTime")]
        [Display(Name = "Contractions time")]
        public DateTime ContractionsTime { get; set; }

        /// <summary>
        /// Oxytocin U/L of the patient.
        /// </summary>
        [Required(ErrorMessage = "The oxytocinUL is required!")]
        [Display(Name = "OxytocinUL")]
        public int Oxytocin { get; set; }

        /// <summary>
        /// Oxytocin U/L taken time.
        /// </summary>
        [Required(ErrorMessage = "The oxytocin time is required!")]
        [Column(TypeName = "SmallDateTime")]
        [Display(Name = "oxytocin time")]
        public DateTime OxytocinTime { get; set; }

        /// <summary>
        /// Description about the drops/min.
        /// </summary>
        [Required(ErrorMessage = "The drops is required!")]
        [Display(Name = "Drops")]
        public int Drops { get; set; }

        /// <summary>
        /// Drops taken time.
        /// </summary>
        [Required(ErrorMessage = "The drops time is required!")]
        [Column(TypeName = "SmallDateTime")]
        [Display(Name = "drops time")]
        public DateTime DropsTime { get; set; }

        /// <summary>
        /// Description of medicine.
        /// </summary>
        [Required(ErrorMessage = "The medicine is required!")]
        [StringLength(150)]
        [Display(Name = "medicine")]
        public string Medicine { get; set; }

        /// <summary>
        /// Medicine taken time.
        /// </summary>
        [Required(ErrorMessage = "The medicine time is required!")]
        [Column(TypeName = "SmallDateTime")]
        [Display(Name = "medicine time")]
        public DateTime MedicineTime { get; set; }

        /// <summary>
        /// Systolic mesurement of BP.
        /// </summary>
        [Required(ErrorMessage = "The systolic is required!")]
        [Display(Name = "Systolic")]
        public int Systolic { get; set; }

        /// <summary>
        /// Diastolic mesurement of BP.
        /// </summary>
        [Required(ErrorMessage = "The diastolic is required!")]
        [Display(Name = "Diastolic")]
        public int Diastolic { get; set; }

        /// <summary>
        /// BP taken time.
        /// </summary>
        [Required(ErrorMessage = "The Bp time is required!")]
        [Column(TypeName = "SmallDateTime")]
        [Display(Name = "Bp time ")]
        public DateTime BpTime { get; set; }

        /// <summary>
        /// Pulse rate of the patients.
        /// </summary>
        [Required(ErrorMessage = "The pulse is required!")]
        [Display(Name = "Pulse")]
        public int Pulse { get; set; }

        /// <summary>
        /// Pulse rate taken time of the patients.
        /// </summary>
        [Required(ErrorMessage = "The pulse time is required!")]
        [Column(TypeName = "SmallDateTime")]
        [Display(Name = "pulse time ")]
        public DateTime PulseTime { get; set; }

        /// <summary>
        /// Description of temp °C. 
        /// </summary>
        [Required(ErrorMessage = "The temp is required!")]
        [Display(Name = "Temp")]
        public int Temp { get; set; }

        /// <summary>
        /// Temparature mesure time.
        /// </summary>
        [Required(ErrorMessage = "The temp time is required!")]
        [Column(TypeName = "SmallDateTime")]
        [Display(Name = "temp time ")]
        public DateTime TempTime { get; set; }

        /// <summary>
        /// Description of protein.
        /// </summary>
        [Required(ErrorMessage = "The protein is required!")]
        [StringLength(150)]
        [Display(Name = "protein")]
        public string Protein { get; set; }

        /// <summary>
        /// Protein taken time of the patients.
        /// </summary>
        [Required(ErrorMessage = "The protein time is required!")]
        [Column(TypeName = "SmallDateTime")]
        [Display(Name = "protein time ")]
        public DateTime ProteinTime { get; set; }

        /// <summary>
        /// Description of acetone.
        /// </summary>
        [Required(ErrorMessage = "The acetone is required!")]
        [StringLength(150)]
        [Display(Name = "acetone")]
        public string Acetone { get; set; }

        /// <summary>
        /// Acetone taken time of the patients.
        /// </summary>
        [Required(ErrorMessage = "The acetone time is required!")]
        [Column(TypeName = "SmallDateTime")]
        [Display(Name = " acetone time ")]
        public DateTime AcetoneTime { get; set; }

        /// <summary>
        /// Description of volume.
        /// </summary>
        [Required(ErrorMessage = "The Volume is required!")]
        [Display(Name = "Volume")]
        public int Volume { get; set; }

        /// <summary>
        /// Time of volume.
        /// </summary>
        [Required(ErrorMessage = "The volume time is required!")]
        [Column(TypeName = "SmallDateTime")]
        [Display(Name = " volume time ")]
        public DateTime VolumeTime { get; set; }

        /// <summary>
        /// Fetal Rate  is the heart rate and rhythm of the baby
        /// </summary>
        [Required(ErrorMessage = "The fetal rate is required!")]
        [Display(Name = "fetal rate")]
        public int FetalRate { get; set; }

        /// <summary>
        /// Fatal rate measurement time of the baby.
        /// </summary>
        [Required(ErrorMessage = "The fetal rate time is required!")]
        [Column(TypeName = "SmallDateTime")]
        [Display(Name = " Fetal rate time ")]
        public DateTime FetalRateTime { get; set; }

        [ForeignKey("PartographID")]
        public virtual Partograph Partograph { get; set; }
    }
}
