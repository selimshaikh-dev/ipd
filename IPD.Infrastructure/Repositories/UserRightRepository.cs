using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using IPD.Infrastructure.Sql;

namespace IPD.Infrastructure.Repositories
{
    public class UserRightRepository : Repository<UserRight>, IUserRightRepository
    {
        protected DataContext context;

        public UserRightRepository(DataContext dbcontext) : base(dbcontext)
        {
            this.context = dbcontext;
        }
    }
}