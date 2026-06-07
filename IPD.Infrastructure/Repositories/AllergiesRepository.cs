using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using IPD.Infrastructure.Sql;

namespace IPD.Infrastructure.Repositories
{
    public class AllergiesRepository : Repository<Allergy>, IAllergiesRepository
    {
        private readonly DataContext context;

        public AllergiesRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public IList<Allergy> GetAllAllergy()
        {
            var list = (from a in context.Allergies

                        where a.IsRowDeleted.Equals(false)
                        select new Allergy
                        {
                            AllergiesName = a.AllergiesName,
                        }).ToList();
            return list;
        }
    }
}