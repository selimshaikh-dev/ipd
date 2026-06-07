using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using IPD.Infrastructure.Sql;

namespace IPD.Infrastructure.Repositories
{
    public class ExaminationDetailsRepository : Repository<ExaminationDetail>, IExaminationDetailsRepository
    {
        public ExaminationDetailsRepository(DataContext context) : base(context)
        {
        }
    }
}