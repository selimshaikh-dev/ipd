using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using IPD.Infrastructure.Sql;

namespace IPD.Infrastructure.Repositories
{
    public class CountriesRepository : Repository<Country>, ICountriesRepository
    {
        private readonly DataContext context;

        public CountriesRepository(DataContext context) : base(context)
        {
            this.context = context;
        }
    }
}