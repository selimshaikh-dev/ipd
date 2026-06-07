using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using IPD.Infrastructure.Sql;

namespace IPD.Infrastructure.Repositories
{
    public class RegionRepository : Repository<Region>, IRegionRepository
    {
        protected DataContext context;

        public RegionRepository(DataContext dbcontext) : base(dbcontext)
        {
            this.context = dbcontext;
        }

        public IList<Region> GetAllRegion()
        {
            var list = (from r in context.Regions

                        where r.IsRowDeleted.Equals(false)
                        select new Region
                        {
                            RegionName = r.RegionName,
                        }).ToList();
            return list;
        }
    }
}