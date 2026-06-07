using IPD.Domain.Entities;

namespace IPD.Infrastructure.Contracts
{
    public interface ISurgicalProceduresRepository : IRepository<SurgicalProcedure>
    {
        IList<SurgicalProcedure> GetAllSurgicalProcedure();
    }
}