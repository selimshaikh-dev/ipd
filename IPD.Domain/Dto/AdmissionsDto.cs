using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPD.Domain.Dto
{
    public class AdmissionsDto
    {
        /// <summary>
        /// Primary key of admission table.
        /// </summary>
        public Guid AdmissionID { get; set; }

        /// <summary>
        /// Date of admission.
        /// </summary>
        [Required(ErrorMessage = "The Admission Date is required!")]
        [Column(TypeName = "SmallDateTime")]
        [Display(Name = "Admission Date")]
        public DateTime AdmissionDate { get; set; }

        /// <summary>
        /// Patient's admission time.
        /// </summary>
        [Required(ErrorMessage = "The Admission Time is required!")]
        [Column(TypeName = "SmallDateTime")]
        [Display(Name = "Admission Time")]
        public DateTime AdmissionTime { get; set; }

        /// <summary>
        /// The patient will be treated under that doctor.
        /// </summary>
        [Required(ErrorMessage = "The Assaign Doctor name is required!")]
        [StringLength(92)]
        [Display(Name = "Assaign Doctor")]

        public string AssaignDoctor { get; set; }

        /// <summary>
        /// Name of the next of kin. Emergency contact person.
        /// </summary>
        [Required(ErrorMessage = "The Next Of Kin is required!")]
        [StringLength(95)]
        [Display(Name = "NextOfKin")]
        public string NextOfKin { get; set; } = null!;

        /// <summary>
        /// Relationship between patients and next of kin.
        /// </summary>
        [Required(ErrorMessage = "The Relationship is required!")]
        [StringLength(95)]
        [Display(Name = "Relationship")]
        public string Relationship { get; set; } = null!;

        /// <summary>
        /// Contact address of the next of kin.
        /// </summary>
        [Required(ErrorMessage = "The Contact Address is required!")]
        [StringLength(500)]
        [Display(Name = "ContactAddress")]
        public string ContactAddress { get; set; } = null!;

        /// <summary>
        /// Country code of the cellphone. Next of kin's cellphone.
        /// </summary>
        [Required(ErrorMessage = "The Cellphone Country Code is required!")]
        [StringLength(3)]
        [Display(Name = "Cellphone Country Code")]
        public string CellphoneCountryCode { get; set; } = null!;

        /// <summary>
        /// Emergency contact number. Next of kin’s cellphone.
        /// </summary>
        [Required(ErrorMessage = "The Cellphone is required!")]
        [StringLength(15)]
        [Display(Name = "Cellphone")]
        public string Cellphone { get; set; } = null!;

        /// <summary>
        /// "Yes" indicates patient has been discharged. "No" indicates the patient is still admitted in the hospital.
        /// </summary>
        [Required(ErrorMessage = "IsDischarged is required!")]
        [Display(Name = "Is Discharged")]
        public bool IsDischarged { get; set; }

        /// <summary>
        /// Foreign key, Primary key of the Patients table.
        /// </summary>
        public Guid PatientID { get; set; }
        public DateTime? DateCreated { get; set; }

        public List<DischargesDto>? Discharges { get; set; }
        public List<DeathCertificateDto>? DeathCertificates { get; set; }

    }
}
