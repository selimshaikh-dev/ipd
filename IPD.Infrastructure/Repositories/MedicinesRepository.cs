using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using IPD.Infrastructure.Sql;
using Microsoft.EntityFrameworkCore;

namespace IPD.Infrastructure.Repositories
{
    public class MedicinesRepository : Repository<Medicine>, IMedicinesRepository
    {
        private readonly DataContext context;

        public MedicinesRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public Medicine UpdateMedicine(Medicine medicine)
        {
            try
            {
                var existingInDb = context.Medicines
                    .FirstOrDefault(i =>
                        i.PartographID.Equals(medicine.PartographID) &&
                        i.MedicinesTime.Equals(medicine.MedicinesTime)
                    );

                if (existingInDb == null)
                {
                    existingInDb = new Medicine()
                    {
                        PartographID = medicine.PartographID,
                        MedicinesTime = medicine.MedicinesTime,
                        MedicinesName = medicine.MedicinesName
                    };
                    context.Medicines.Add(existingInDb);
                }
                else
                {
                    if (existingInDb.MedicinesName != medicine.MedicinesName)
                    {
                        existingInDb.MedicinesName = medicine.MedicinesName;
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