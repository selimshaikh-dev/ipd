using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using IPD.Infrastructure.Sql;

namespace IPD.Infrastructure.Repositories
{
    public class SurgicalProceduresRepository : Repository<SurgicalProcedure>, ISurgicalProceduresRepository
    {
        private readonly DataContext context;

        public SurgicalProceduresRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public IList<SurgicalProcedure> GetAllSurgicalProcedure()
        {
            var list = (from s in context.SurgicalProcedures

                        where s.IsRowDeleted.Equals(false)
                        select new SurgicalProcedure
                        {
                            ProcedureName = s.ProcedureName,
                        }).ToList();
            return list;
        }
    }
}