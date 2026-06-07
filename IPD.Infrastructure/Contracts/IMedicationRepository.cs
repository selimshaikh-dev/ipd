using IPD.Domain.Dto;
using IPD.Domain.Entities;

namespace IPD.Infrastructure.Contracts
{
    public interface IMedicationRepository : IRepository<Medication>
    {
        IList<Medication> GetAllMedication();
    }
}