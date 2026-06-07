using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using IPD.Infrastructure.Sql;

namespace IPD.Infrastructure.Repositories
{
    public class NcdsRepository : Repository<Ncd>, INcdsRepository
    {
        private readonly DataContext context;

        public NcdsRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public IList<Ncd> GetAllNcds()
        {
            var list = (from n in context.Ncds

                        where n.IsRowDeleted.Equals(false)
                        select new Ncd
                        {
                            NcdName = n.NcdName,
                        }).ToList();
            return list;
        }
    }
}