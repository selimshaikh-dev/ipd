using IPD.Domain.Dto;
using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using IPD.Infrastructure.Sql;
using Microsoft.EntityFrameworkCore;

namespace IPD.Infrastructure.Repositories
{
    public class VitalRepository : Repository<Vital>, IVitalRepository
    {
        private readonly DataContext context;

        public VitalRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<VitalDto>> GetAllLoadVital(Guid aid)
        {
            try
            {
                var minDate = DateTime.Now.AddMonths(-3);
                var q = await (from c in _context.Vitals
                    .Where(e => e.DateCreated > minDate)
                    .Where(e => e.AdmissionID == aid)
                    .Include(e => e.Admission)
                    .ThenInclude(e => e.BirthDetails)
                    join f in _context.Facilities
                    on c.FacilityCode equals f.FacilityCode into fc
                    from f in fc.DefaultIfEmpty()
                    select new VitalDto
                    {
                        AdmissionID = aid,
                        VitalID = c.VitalID,
                        DateCreated = c.DateCreated,
                        FacilityName = f.FacilityName,
                        DateCollected = c.DateCollected,
                        TimeCollected = c.TimeCollected,
                        Weight = c.Weight,
                        Height = c.Height,
                        Temperature = c.Temperature,
                        Systolic = c.Systolic,
                        Diastolic = c.Diastolic,
                        RespiratoryRate = c.RespiratoryRate,
                        Pulse = c.Pulse,
                        BMI = c.BMI,
                        NutritionalStatus = c.NutritionalStatus,
                        OtherVitals = c.OtherVitals,
                        MUAC= c.MUAC,
                        OxygenSaturation = c.OxygenSaturation
                    }).OrderByDescending(e=>e.DateCreated)

                .ToListAsync();
                return q;
            }
            catch
            {
                throw;
            }
        }
    }
}