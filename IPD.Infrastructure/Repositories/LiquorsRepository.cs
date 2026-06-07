using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using IPD.Infrastructure.Sql;
using Microsoft.EntityFrameworkCore;

namespace IPD.Infrastructure.Repositories
{
    public class LiquorsRepository : Repository<Liquor>, ILiquorsRepository
    {
        private readonly DataContext context;

        public LiquorsRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public Liquor UpdateLiquor(Liquor liquor)
        {
            try
            {
                var existingInDb = context.Liquors
                    .FirstOrDefault(i =>
                        i.PartographID.Equals(liquor.PartographID) &&
                        i.LiquorTime.Equals(liquor.LiquorTime)
                    );

                if (existingInDb == null)
                {
                    existingInDb = new Liquor()
                    {
                        PartographID = liquor.PartographID,
                        LiquorTime = liquor.LiquorTime,
                        LiquorDetails = liquor.LiquorDetails
                    };
                    context.Liquors.Add(existingInDb);
                }
                else
                {
                    existingInDb.LiquorDetails = liquor.LiquorDetails;
                    context.Entry(existingInDb).State = EntityState.Modified;
                }

                return existingInDb;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}