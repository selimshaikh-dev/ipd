using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using IPD.Infrastructure.Sql;

namespace IPD.Infrastructure.Repositories
{
    public class DischargeRepository : Repository<Discharge>, IDischargeRepository
    {
        private readonly DataContext context;

        public DischargeRepository(DataContext context) : base(context)
        {
            this.context = context;
        }
    }
}