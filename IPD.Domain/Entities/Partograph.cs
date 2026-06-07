using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPD.Domain.Entities
{
    /// <summary>
    /// The Partograph table holds  the Partograph detail of the patients
    /// </summary>
    public class Partograph : BaseModel
    {
        /// <summary>
        /// Primary key of the table Partograph.
        /// </summary>
        [Key]
        public Guid PartographID { get; set; }

        /// <summary>
        /// Foreign Key, Primary Key of the Admissions. 
        /// </summary>
        public Guid AdmissionID { get; set; }

        /// <summary>
        /// Gravida mesurement of the patients.
        /// </summary>
        [Required(ErrorMessage = "Gravida is required!")]
        [Display(Name = "Gravida")]
        public byte Gravida { get; set; }

        /// <summary>
        /// Parity mesurement of the patients.
        /// </summary>
        [Required(ErrorMessage = "Parity is required!")]
        [Display(Name = "Parity")]
        public byte? Parity { get; set; }

        /// <summary>
        /// SB or NND System of a patients.
        /// </summary>       
        [StringLength(100)]
        [Display(Name = "SBOrNND")]
        public string? SBOrNND { get; set; }

        /// <summary>
        /// Loss of pregnancy due to the premature exit of the products of conception
        /// </summary>
        [Display(Name = "Abortion")]
        public int? Abortion { get; set; }

        /// <summary>
        /// Estimated date of delivery of the patients.
        /// </summary>
        [Column(TypeName = "SmallDateTime")]
        [Display(Name = "EDD")]
        public DateTime? EDD { get; set; }

        /// <summary>
        /// Mesurement of height.
        /// </summary>       
        [StringLength(100)]
        [Display(Name = "Borderline risk factors")]
        public string? BorderlineRiskFactors { get; set; }

        /// <summary>
        /// Regular contractions started time.
        /// </summary>      
        [Display(Name = "Height")]
        public decimal? Height { get; set; }

        /// <summary>
        ///Regular contractions started time.
        /// </summary>
        [Required(ErrorMessage = "The Regular Contractions is required!")]
        [Column(TypeName = "SmallDateTime")]
        [Display(Name = "Regular Contractions")]
        public DateTime? RegularContractions { get; set; }

        [Required(ErrorMessage = "Partograph initiated date is required!")]
        [Column(TypeName = "SmallDateTime")]
        [Display(Name = "Initiate Date")]
        public DateTime InitiateDate { get; set; } = DateTime.MinValue;

        [StringLength(10)]
        [Required(ErrorMessage = "Partograph initiated time is required!")]
        [Display(Name = "Initiate Time")]
        public string InitiateTime { get; set; } = String.Empty;

        /// <summary>
        ///During pregnancy to describe a rupture of the amniotic sac.
        /// </summary>       
        [Column(TypeName = "SmallDateTime")]
        [Display(Name = "Membranes Ruptured")]
        public DateTime? MembranesRuptured { get; set; }

        [ForeignKey("AdmissionID")]
        public virtual Admission Admissions { get; set; }
        public virtual IEnumerable<FetalHeartRate> FetalHeartRates { get; set; }
        public virtual IEnumerable<Liquor> Liquors { get; set; }
        public virtual IEnumerable<Moulding> Mouldings { get; set; }
        public virtual IEnumerable<Cervix> Cervixes { get; set; }
        public virtual IEnumerable<DescentOfHead> DescentOfHeads { get; set; }
        public virtual IEnumerable<Contraction> Contractions { get; set; }
        public virtual IEnumerable<Oxytocin> Oxytocins { get; set; }
        public virtual IEnumerable<Drop> Drops { get; set; }
        public virtual IEnumerable<Medicine> Medicines { get; set; }
        public virtual IEnumerable<BloodPressure> BloodPressures { get; set; }
        public virtual IEnumerable<Pulse> Pulses { get; set; }
        public virtual IEnumerable<Temperature> Temperatures { get; set; }
        public virtual IEnumerable<Protein> Proteins { get; set; }
        public virtual IEnumerable<Acetone> Acetones { get; set; }
        public virtual IEnumerable<Volume> Volumes { get; set; }
        public virtual IEnumerable<PartographDetail> PartographDetails { get; set; }
    }
}
