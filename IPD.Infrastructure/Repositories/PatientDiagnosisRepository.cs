using IPD.Domain.Dto;
using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using IPD.Infrastructure.Sql;
using Microsoft.EntityFrameworkCore;

namespace IPD.Infrastructure.Repositories
{
    public class PatientDiagnosisRepository : Repository<PatientDiagnosis>, IPatientDiagnosisRepository
    {
        private readonly DataContext context;

        public PatientDiagnosisRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        //public async Task<IEnumerable<PatientDiagnosisDto>> GetAllPatientDiagnosis(Guid aid)
        //{

        //    try
        //    {
        //        var minDate = DateTime.Now.AddMonths(-3);
        //        var q = await (from c in _context.PatientDiagnosis
        //                       .Where(e => e.DateCreated > minDate).Include(x => x.DiagonosisDetails)
        //                       .Where(e => e.AdmissionID == aid)
        //                       .Include(e => e.Admissions)
        //                       .ThenInclude(e => e.BirthDetails)
        //                       join f in _context.Facilities
        //                       on c.FacilityCode equals f.FacilityCode into fc
        //                       from f in fc.DefaultIfEmpty()


        //                       select new PatientsDiagnosisDto
        //                       {
        //                           AdmissionID = aid,
        //                           PatientDiagnosisID = c.PatientDiagnosisID,
        //                           DiagnosisNote = c.DiagnosisNote,
        //                           DateCreated = c.DateCreated,
        //                           FacilityName = f.FacilityName
        //                       })

        //                 .ToListAsync();
        //        return q;
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        public async Task<List<PatientDiagnosisDto>> GetPatientDiagnosisByAdmissionIdAsync(Guid admissionId, string facilityCode)
        {
            try
            {
                var patientDetails = await context.Admissions
                                        .FirstOrDefaultAsync(i => i.AdmissionID.Equals(admissionId));
                var patientId = patientDetails?.PatientID ?? Guid.Empty;

                
                var facilities = await context.Facilities
                                      .FirstOrDefaultAsync(i => i.IsRowDeleted == false && i.FacilityCode == facilityCode);
                var facilityName = facilities?.FacilityName ?? string.Empty;


                var minDate = DateTime.Now.AddMonths(-3);

                var patientExaminationList = await context.PatientDiagnosis
                                .Where(i => i.Admissions.PatientID.Equals(patientId) && i.DateCreated > minDate)
                                .Select(i => new PatientDiagnosisDto()
                                {
                                    DiagnosisNote = i.DiagnosisNote,
                                    FacilityName = facilityName,
                                    DateCreated = i.DateCreated,
                                    DiagonosisDetails = i.DiagonosisDetails.Select(k => new Domain.Dto.DiagonosisDetailsDto()
                                    {
                                        DiagonosisDetailsID = k.DiagonosisDetailsID,
                                        DiseaseID = k.DiseaseID,
                                        PatientDiagnosisID = k.PatientDiagnosisID
                                    }).ToList()
                                })
                                .ToListAsync();

                return patientExaminationList;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}