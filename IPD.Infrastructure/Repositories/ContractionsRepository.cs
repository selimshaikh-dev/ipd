using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using IPD.Infrastructure.Sql;
using Microsoft.EntityFrameworkCore;

namespace IPD.Infrastructure.Repositories
{
    public class ContractionsRepository : Repository<Contraction>, IContractionsRepository
    {
        private readonly DataContext context;

        public ContractionsRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public Contraction UpdateContraction(Contraction contraction)
        {
            try
            {
                var existingInDb = context.Contractions
                    .FirstOrDefault(i =>
                        i.PartographID.Equals(contraction.PartographID) &&
                        i.ContractionsTime.Equals(contraction.ContractionsTime)
                    );
                if (existingInDb == null)
                {
                    existingInDb = new Contraction()
                    {
                        PartographID = contraction.PartographID,
                        ContractionsTime = contraction.ContractionsTime,
                        ContractionsDetails = contraction.ContractionsDetails,
                        Duration = contraction.Duration
                    };
                    context.Contractions.Add(existingInDb);
                }
                else
                {
                    if (existingInDb.ContractionsDetails != contraction.ContractionsDetails || existingInDb.Duration != contraction.Duration)
                    {

                        existingInDb.ContractionsDetails = contraction.ContractionsDetails;
                        existingInDb.Duration = contraction.Duration;
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