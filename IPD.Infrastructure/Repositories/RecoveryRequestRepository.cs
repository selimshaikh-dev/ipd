using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using IPD.Infrastructure.Sql;

namespace IPD.Infrastructure.Repositories
{
    public class RecoveryRequestRepository : Repository<RecoveryRequest>, IRecoveryRequestRepository
    {
        protected DataContext context;

        public RecoveryRequestRepository(DataContext dbcontext) : base(dbcontext)
        {
            this.context = dbcontext;
        }
    }
}