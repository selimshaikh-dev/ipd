using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using IPD.Infrastructure.Sql;

namespace IPD.Infrastructure.Repositories
{
    public class SurgeriesRepository : Repository<Surgery>, ISurgeriesRepository
    {
        private readonly DataContext context;

        public SurgeriesRepository(DataContext context) : base(context)
        {
            this.context = context;
        }
    }
}