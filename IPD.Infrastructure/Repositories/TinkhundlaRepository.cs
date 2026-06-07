using IPD.Domain.Dto;
using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using IPD.Infrastructure.Sql;

namespace IPD.Infrastructure.Repositories
{
    public class TinkhundlaRepository : Repository<Tinkhundla>, ITinkhundlaRepository
    {
        private readonly DataContext _context;

        public TinkhundlaRepository(DataContext context) : base(context)
        {
            this._context = context;
        }

        public IList<TinkhundlaDto> GetTinkhundlaList()
        {
            var list = (from a in _context.Tinkhundla
                        where a.IsRowDeleted.Equals(false)
                        select new TinkhundlaDto
                        {
                            Name = a.Name
                        }).ToList();

            return list;
        }
    }
}