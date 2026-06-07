using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace IPD.Infrastructure.Sql
{
    public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder.UseSqlServer("Server=Dimik;Database=CMIS.IPD;User Id=sa;Password=1234;MultipleActiveResultSets=true");

            return new DataContext(optionsBuilder.Options);
        }
    }
}
