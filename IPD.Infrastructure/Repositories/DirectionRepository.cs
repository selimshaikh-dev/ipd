using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using IPD.Infrastructure.Sql;

namespace IPD.Infrastructure.Repositories
{
    public class DirectionRepository : Repository<Direction>, IDirectionRepository
    {
        private readonly DataContext context;

        public DirectionRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public IList<Direction> GetAllDirection()
        {
            var list = (from s in context.Directions

                        where s.IsRowDeleted.Equals(false)
                        select new Direction
                        {
                            DirectionDetails = s.DirectionDetails,
                        }).ToList();
            return list;
        }
    }
}