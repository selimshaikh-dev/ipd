using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using IPD.Infrastructure.Sql;

namespace IPD.Infrastructure.Repositories
{
    public class PatientsNcdRepository : Repository<PatientsNcd>, IPatientsNcdRepository
    {
        public PatientsNcdRepository(DataContext context) : base(context)
        {
        }
    }
}