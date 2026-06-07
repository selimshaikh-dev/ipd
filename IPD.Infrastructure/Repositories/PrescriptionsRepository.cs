using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using IPD.Infrastructure.Sql;

namespace IPD.Infrastructure.Repositories
{
    public class PrescriptionsRepository : Repository<Prescription>, IPrescriptionsRepository
    {
        private readonly DataContext context;

        public PrescriptionsRepository(DataContext context) : base(context)
        {
            this.context = context;
        }
    }
}