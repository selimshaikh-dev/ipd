using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using IPD.Infrastructure.Sql;

namespace IPD.Infrastructure.Repositories
{
    public class PostSurgeriesReposity : Repository<PostSurgery>, IPostSurgeriesReposity
    {
        private readonly DataContext context;

        public PostSurgeriesReposity(DataContext context) : base(context)
        {
            this.context = context;
        }
    }
}