using IPD.Domain.Dto;
using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using IPD.Infrastructure.Sql;
using Microsoft.EntityFrameworkCore;

namespace IPD.Infrastructure.Repositories
{
    public class BirthDetailsRepository : Repository<BirthDetail>, IBirthDetailsRepository
    {
        private readonly DataContext context;

        public BirthDetailsRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<List<PartographIndexDto>> GetByPatientId(Guid patientId)
        {
            var birthDetails = await context.BirthDetails
                    .AsNoTracking()
                    .Where(i => i.Admissions.PatientID.Equals(patientId))
                    .Select (i => new PartographIndexDto()
                    {
                        FirstName = i.Admissions.Patients.FirstName,
                        MiddleName = i.Admissions.Patients.MiddleName,
                        LastName = i.Admissions.Patients.LastName,
                        PartographID = i.Admissions.Partograph.FirstOrDefault().PartographID,
                        AdmissionID = i.AdmissionID,
                        BirthDate = i.BirthDate,
                        BirthTime = i.BirthTime,
                        TypeOfDelivery = i.TypeOfDelivery,
                        IsSuccessfulDelivery = i.IsSuccessfulDelivery,
                        BirthDetailsID = i.BirthDetailsID,
                        InitiateDate = i.Admissions.Partograph.FirstOrDefault().InitiateDate,
                        InitiateTime = i.Admissions.Partograph.FirstOrDefault().InitiateTime,
                        FacilityCode = i.Admissions.FacilityCode,
                        Remarks = i.Remarks,
                    })
                    .ToListAsync();
            return birthDetails;
        }

        
      public async  Task<BirthDetail> GetByAdmissionID(Guid admissionID)
        {
            var birthDetails = await context.BirthDetails
                                     .AsNoTracking()
                                     .FirstOrDefaultAsync(i => i.AdmissionID.Equals(admissionID));
                                    
            return birthDetails;
        }
    }
}