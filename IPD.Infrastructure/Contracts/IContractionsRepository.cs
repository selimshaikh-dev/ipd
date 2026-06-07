using IPD.Domain.Entities;

namespace IPD.Infrastructure.Contracts
{
    public interface IContractionsRepository : IRepository<Contraction>
    {
        Contraction UpdateContraction(Contraction contraction);
    }
}