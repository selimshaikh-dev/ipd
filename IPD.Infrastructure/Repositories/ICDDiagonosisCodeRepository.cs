using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using IPD.Infrastructure.Sql;

namespace IPD.Infrastructure.Repositories
{
    public class ICDDiagonosisCodeRepository : Repository<ICDDigonosisCode>, IICDDiagonosisCodeRepository
    {
        private readonly DataContext context;

        public ICDDiagonosisCodeRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public IList<ICDDigonosisCode> GetAllICDDigonosisCode()
        {
            var list = (from d in context.ICDDigonosisCodes

                        where d.IsRowDeleted.Equals(false)
                        select new ICDDigonosisCode
                        {
                            Description = d.Description,
                        }).ToList();
            return list;
        }
    }
}