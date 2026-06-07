using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using IPD.Infrastructure.Sql;

namespace IPD.Infrastructure.Repositories
{
    public class DiagonosisDetailRepository : Repository<DiagonosisDetail>, IDiagonosisDetailRepository
    {
        public DiagonosisDetailRepository(DataContext context) : base(context)
        {
        }
    }
}