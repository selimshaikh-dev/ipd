using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using IPD.Infrastructure.Sql;
using Microsoft.EntityFrameworkCore;

namespace IPD.Infrastructure.Repositories
{
    public class OxytocinRepository : Repository<Oxytocin>, IOxytocinRepository
    {
        private readonly DataContext context;

        public OxytocinRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public Oxytocin UpdateOxytocin(Oxytocin oxytocin)
        {
            try
            {
                var existingInDb = context.Oxytocins
                    .FirstOrDefault(i =>
                        i.PartographID.Equals(oxytocin.PartographID) &&
                        i.OxytocinTime.Equals(oxytocin.OxytocinTime)
                    );
                if (existingInDb == null)
                {
                    existingInDb = new Oxytocin()
                    {
                        PartographID = oxytocin.PartographID,
                        OxytocinTime = oxytocin.OxytocinTime,
                        OxytocinDetails = oxytocin.OxytocinDetails
                    };
                    context.Oxytocins.Add(existingInDb);
                }
                else
                {
                    if (existingInDb.OxytocinDetails != oxytocin.OxytocinDetails)
                    {
                        existingInDb.OxytocinDetails = oxytocin.OxytocinDetails;
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