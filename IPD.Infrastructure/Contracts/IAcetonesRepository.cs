using IPD.Domain.Entities;

namespace IPD.Infrastructure.Contracts
{
    public interface IAcetonesRepository : IRepository<Acetone>
    {
        Acetone UpdateAcetone(Acetone acetone);
    }
}