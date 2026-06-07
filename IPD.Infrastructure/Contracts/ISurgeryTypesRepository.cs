using IPD.Domain.Entities;

namespace IPD.Infrastructure.Contracts
{
    public interface ISurgeryTypesRepository : IRepository<SurgeryType>
    {
        IList<SurgeryType> GetAllSurgeryType();
    }
}