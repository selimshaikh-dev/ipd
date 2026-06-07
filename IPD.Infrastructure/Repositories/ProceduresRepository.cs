using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using IPD.Infrastructure.Sql;

namespace IPD.Infrastructure.Repositories
{
    public class ProceduresRepository : Repository<Procedure>, IProceduresRepository
    {
        private readonly DataContext context;

        public ProceduresRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public IList<Procedure> GetAllProcedure()
        {
            var list = (from p in context.Procedure

                        where p.IsRowDeleted.Equals(false)
                        select new Procedure
                        {
                            ProcedureName = p.ProcedureName,
                        }).ToList();
            return list;
        }
    }
}