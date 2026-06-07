using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using IPD.Infrastructure.Sql;

namespace IPD.Infrastructure.Repositories
{
    public class FacilityRepository : Repository<Facility>, IFacilityRepository
    {
        protected DataContext context;

        public FacilityRepository(DataContext dbcontext) : base(dbcontext)
        {
            this.context = dbcontext;
        }
    }
}