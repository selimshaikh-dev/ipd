using IPD.Domain.Dto;
using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using IPD.Infrastructure.Sql;
using Microsoft.EntityFrameworkCore;

namespace IPD.Infrastructure.Repositories
{
    public class TreatmentPlansRepository : Repository<TreatmentPlan>, ITreatmentPlansRepository
    {
        private readonly DataContext context;

        public TreatmentPlansRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<TreatmentPlanDto>> GetAllTreatmentPlan(Guid aid)
        {
            try
            {
                var minDate = DateTime.Now.AddMonths(-3);
                var q = await (from c in _context.TreatmentPlans
                    .Where(e => e.DateCreated > minDate)
                    .Where(e => e.AdmissionID == aid)
                    .Include(e => e.Admissions)
                    .ThenInclude(e => e.BirthDetails)
                        join f in _context.Facilities
                        on c.FacilityCode equals f.FacilityCode into fc
                        from f in fc.DefaultIfEmpty()
                        select new TreatmentPlanDto
                        {
                            AdmissionID = aid,
                            TreatmentPlanID = c.TreatmentPlanID,
                            TreatementPlanDetails = c.TreatementPlanDetails,
                            DateCreated = c.DateCreated,
                            FacilityName = f.FacilityName
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