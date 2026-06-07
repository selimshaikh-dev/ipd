using IPD.Domain.Dto;
using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using IPD.Infrastructure.Sql;
using Microsoft.EntityFrameworkCore;

namespace IPD.Infrastructure.Repositories
{
    public class DiabeticProfileRepository : Repository<DiabeticProfile>, IDiabeticProfileRepository
    {
        private readonly DataContext context;

        public DiabeticProfileRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<DiabeticsProfileDto>> GetAllDiabeticProfile(Guid aid)
        {
            try
            {
                var minDate = DateTime.Now.AddMonths(-3);
                var q = await (from c in _context.DiabeticProfiles
                               .Where(e => e.DateCreated > minDate)
                               .Where(e => e.AdmissionID == aid)
                               .Include(e => e.Admission)
                               .ThenInclude(e => e.BirthDetails)
                               join f in _context.Facilities
                               on c.FacilityCode equals f.FacilityCode into fc
                               from f in fc.DefaultIfEmpty()
                               select new DiabeticsProfileDto
                               {
                                   AdmissionID = aid,
                                   DiabeticProfileID = c.DiabeticProfileID,
                                   DateCreated = c.DateCreated,
                                   FacilityName = f.FacilityName,
                                   DateCollected = c.DateCollected,
                                   TimeCollected = c.TimeCollected,
                                   BloodSuger = c.BloodSuger,
                                   UrinSuger = c.UrinSuger,
                                   UrinKetones = c.UrinKetones,
                                   InsulinDose = c.InsulinDose
                               }).OrderByDescending(e => e.DateCreated)

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