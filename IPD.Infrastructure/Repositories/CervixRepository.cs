using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using IPD.Infrastructure.Sql;
using Microsoft.EntityFrameworkCore;

namespace IPD.Infrastructure.Repositories
{
    public class CervixRepository : Repository<Cervix>, ICervixRepository
    {
        private readonly DataContext context;

        public CervixRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public Cervix UpdateCervix(Cervix cervix)
        {
            try
            {
                var existingInDb = context.Cervixes
                    .FirstOrDefault(i =>
                        i.PartographID.Equals(cervix.PartographID) &&
                        i.CervixTime.Equals(cervix.CervixTime)
                    );

                if (existingInDb == null)
                {
                    existingInDb = new Cervix()
                    {
                        PartographID = cervix.PartographID,
                        CervixTime = cervix.CervixTime,
                        CervixDetails = cervix.CervixDetails
                    };
                    context.Cervixes.Add(existingInDb);
                }
                else
                {
                    if (existingInDb.CervixDetails != cervix.CervixDetails)
                    {
                        existingInDb.CervixDetails = cervix.CervixDetails;
                        context.Entry(existingInDb).State = EntityState.Modified;
                    }
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