using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using IPD.Infrastructure.Sql;

namespace IPD.Infrastructure.Repositories
{
    public class DeathCertificateRepository : Repository<DeathCertificate>, IDeathCertificateRepository
    {
        private readonly DataContext context;

        public DeathCertificateRepository(DataContext context) : base(context)
        {
            this.context = context;
        }
    }
}