using IPD.Domain.Entities;

namespace IPD.Infrastructure.Contracts
{
    public interface IProceduresRepository : IRepository<Procedure>
    {
        IList<Procedure> GetAllProcedure();
    }
}