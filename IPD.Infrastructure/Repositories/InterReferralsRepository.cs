using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using IPD.Infrastructure.Sql;

namespace IPD.Infrastructure.Repositories
{
    public class InterReferralsRepository : Repository<InterDepartmentReferral>, IInterReferralsRepository
    {
        private readonly DataContext context;

        public InterReferralsRepository(DataContext context) : base(context)
        {
            this.context = context;
        }
    }
}