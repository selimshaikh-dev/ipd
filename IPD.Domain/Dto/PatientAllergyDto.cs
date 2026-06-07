namespace IPD.Domain.Dto
{
    public class PatientAllergyDto
    {
        public Guid PatientAllergiesID { get; set; }
        public int AllergiesID { get; set; }
        public string AllergiesName { get; set; }
        public Guid ComplaintID { get; set; }
    }
}
