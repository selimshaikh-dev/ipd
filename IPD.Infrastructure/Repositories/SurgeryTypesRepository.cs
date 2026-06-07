using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using IPD.Infrastructure.Sql;

namespace IPD.Infrastructure.Repositories
{
    public class SurgeryTypesRepository : Repository<SurgeryType>, ISurgeryTypesRepository
    {
        private readonly DataContext context;

        public SurgeryTypesRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public IList<SurgeryType> GetAllSurgeryType()
        {
            var list = (from s in context.SurgeryTypes

                        where s.IsRowDeleted.Equals(false)
                        select new SurgeryType
                        {
                            TypeName = s.TypeName,
                        }).ToList();
            return list;
        }
    }
}