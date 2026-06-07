using IPD.Domain.Dto;
using IPD.Infrastructure.Contracts;
using IPD.Infrastructure.Sql;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPD.Infrastructure.Repositories
{
    public class ReportRepository:Repository<ReportDto>, IReportRepository
    {
        private readonly DataContext context;
        public ReportRepository(DataContext context):base(context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<ReportDto>> GetAllLoadReports(DateTimeDto DateTimeDto)
        {
            try
            {
                var q = await (from c in context.Patients
                         join a in context.Admissions.Where(w => w.AdmissionDate >= DateTimeDto.FromDate && w.AdmissionDate <= DateTimeDto.ToDate) on c.PatientID equals a.PatientID
                         join d in context.Discharges on a.AdmissionID equals d.AdmissionID into dc
                         from d in dc.DefaultIfEmpty()
                         join f in context.Facilities on c.FacilityCode equals f.FacilityCode into fc
                         from f in fc.DefaultIfEmpty()
                         select new
                         { 
                             AdmissionDate = a.AdmissionDate,
                             AdmissionTime=  a.AdmissionTime,
                             AssaignDoctor = a.AssaignDoctor,
                             NextOfKin = a.NextOfKin,
                             NextOfKinCellphone = a.Cellphone,
                             PatientNane = c.FirstName + " " + c.MiddleName + " " + c.LastName,
                             ContactAddress = a.ContactAddress,
                             UHID=c.UHID,
                             PatientAddress=c.ContactAddress,
                             Cellphone = c.Cellphone,
                             FacilityCode = f == null ? "" : f.FacilityCode,
                             DischargeDate = d == null ? DateTime.Now.AddDays(10) : d.DischargeDate,
                             DischargeTime = d == null ? DateTime.Now.AddDays(10) : d.DischargeTime
                         }).ToListAsync();
                List<ReportDto> obj =new List<ReportDto>();
                if (q.Count>0)
                {
                    foreach (var i in q)
                    {
                        string ddate = "";
                        string dtime = "";
                        if (i.DischargeDate.ToString("dd-MM-yyyy")!= DateTime.Now.AddDays(10).ToString("dd-MM-yyyy"))
                        {
                            ddate = i.DischargeDate.ToString("dd-MM-yyyy");
                            dtime = i.DischargeTime.Hour > 12 ? (i.DischargeTime.Hour - 12).ToString() + ":" + i.DischargeTime.ToString("mm") + " PM" : i.DischargeTime.Hour.ToString() + ":" + i.DischargeTime.ToString("mm") + " PM";
                        }
                        obj.Add(new ReportDto
                        {
                            AdmissionDate = i.AdmissionDate.ToString("dd-MM-yyyy"),
                            AssaignDoctor = i.AssaignDoctor,
                            AdmissionTime = i.AdmissionTime.Hour>12? (i.AdmissionTime.Hour-12).ToString()+ ":"+ i.AdmissionTime.ToString("mm")+" PM" : i.AdmissionTime.Hour.ToString() + ":" + i.AdmissionTime.ToString("mm") + " PM",
                            Cellphone= "Cellphone:" + i.Cellphone,
                            NextOfKin=i.NextOfKin,
                            NextOfKinCellphone= "Cellphone:" + i.NextOfKinCellphone,
                            PatientName=i.PatientNane,
                            ContactAddress=i.ContactAddress,
                            UHID="UHID:"+i.UHID,
                            PatientAddress=i.PatientAddress,
                            DischargeDate =ddate,
                            DischargeTime = dtime,
                        }); 
                    }
                }
               
                return obj;
            }
            catch
            {
                throw;
            }
        }
    }
}
