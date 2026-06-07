using IPD.Domain.Entities;

namespace IPD.Infrastructure.Contracts
{
    public interface IAllergiesRepository : IRepository<Allergy>
    {
        IList<Allergy> GetAllAllergy();
    }
}