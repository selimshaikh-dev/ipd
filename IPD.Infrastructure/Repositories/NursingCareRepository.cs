using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using IPD.Infrastructure.Sql;

namespace IPD.Infrastructure.Repositories
{
    public class NursingCareRepository : Repository<NursingCare>, INursingCareRepository
    {
        private readonly DataContext context;

        public NursingCareRepository(DataContext context) : base(context)
        {
            this.context = context;
        }
    }
}