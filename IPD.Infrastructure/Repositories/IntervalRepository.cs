using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using IPD.Infrastructure.Sql;

namespace IPD.Infrastructure.Repositories
{
    public class IntervalRepository : Repository<Interval>, IIntervalRepository
    {
        private readonly DataContext context;

        public IntervalRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public IList<Interval> GetAllInterval()
        {
            var list = (from s in context.Intervals

                        where s.IsRowDeleted.Equals(false)
                        select new Interval
                        {
                            IntervalName = s.IntervalName,
                        }).ToList();
            return list;
        }
    }
}