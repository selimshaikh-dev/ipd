using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using IPD.Infrastructure.Sql;

namespace IPD.Infrastructure.Repositories
{
    public class AdmissionRepository : Repository<Admission>, IAdmissionRepository
    {
        private readonly DataContext context;

        public AdmissionRepository(DataContext context) : base(context)
        {
            this.context = context;
        }
    }
}