namespace IPD.Web.Models.DTO
{
    public class DischargeNoteDTO
    {
        public Guid DischargeID { get; set; }
        public DateTime DischargeDate { get; set; }
        public DateTime DischargeTime { get; set; }
        public string Advice { get; set; }
        public string DietNutritionAdvice { get; set; }
        public string MedicationAdvice { get; set; }
        public string Remarks { get; set; }
        public string FinalDiagnosis { get; set; }
        public Guid DischargeStatusID { get; set; }
        public Guid AdmissionID { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
