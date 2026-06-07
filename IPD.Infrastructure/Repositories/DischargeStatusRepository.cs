using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using IPD.Infrastructure.Sql;

namespace IPD.Infrastructure.Repositories;

public class DischargeStatusRepository : Repository<DischargeStatus>, IDischargeStatusRepository
{
    public DischargeStatusRepository(DataContext context) : base(context)
    {
    }
}