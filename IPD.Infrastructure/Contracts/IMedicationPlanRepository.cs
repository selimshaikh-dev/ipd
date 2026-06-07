using IPD.Domain.Dto;
using IPD.Domain.Entities;

namespace IPD.Infrastructure.Contracts
{
    public interface IMedicationPlanRepository : IRepository<MedicationPlan>
    {
        Task<List<MedicationPlanDto>> GetLatestMedicationPlanAsync(Guid admissionId, string facilityCode);
    }
}