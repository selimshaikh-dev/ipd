using IPD.Domain.Entities;

namespace IPD.Domain.Dto
{
    public class ReportDto:BaseModel
    {
        public string UHID { get; set; } 
        public string PatientName { get; set; }
        public string Cellphone { get; set;}
        public string ContactAddress { get; set; }
        public string PatientAddress { get; set; }
        public string AdmissionDate { get; set; }
        public string AdmissionTime { get; set; }
        public string AssaignDoctor { get; set; }
        public string NextOfKin { get; set; }
        public string NextOfKinCellphone { get; set; }
        public string DischargeDate { get; set; }
        public string DischargeTime { get; set; }
    }
}
