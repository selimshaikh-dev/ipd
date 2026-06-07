using IPD.Domain.Dto;
using IPD.Domain.Entities;

namespace IPD.Infrastructure.Contracts
{
    public interface ITreatmentPlansRepository : IRepository<TreatmentPlan>
    {
        Task<IEnumerable<TreatmentPlanDto>> GetAllTreatmentPlan(Guid aid);
    }
}