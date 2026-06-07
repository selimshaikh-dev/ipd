using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using IPD.Infrastructure.Sql;

namespace IPD.Infrastructure.Repositories
{
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        private readonly DataContext context;

        public DepartmentRepository(DataContext context) : base(context)
        {
            this.context = context;
        }
    }
}