using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using IPD.Infrastructure.Sql;

namespace IPD.Infrastructure.Repositories
{
    public class InternationalReferralRepository : Repository<InternationalReferral>, IInternationalReferralRepository
    {
        private readonly DataContext context;

        public InternationalReferralRepository(DataContext context) : base(context)
        {
            this.context = context;
        }
    }
}