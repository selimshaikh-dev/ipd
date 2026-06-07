using IPD.Domain.Dto;
using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using IPD.Infrastructure.Sql;
using Microsoft.EntityFrameworkCore;

namespace IPD.Infrastructure.Repositories
{
    public class PartographDetailsRepository : Repository<PartographDetail>, IPartographDetailsRepository
    {
        private readonly DataContext context;

        public PartographDetailsRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<PartographDetailReadDto> GetPartographDetailsAsync(Guid partographId)
        {
            var fetalHeartRatesData = await context.FetalHeartRates
                .Where(i => i.PartographID == partographId && i.IsRowDeleted == false)
                .OrderBy(i => i.FetalRateTime)
                .Select(x => new long[]
                {
                    x.FetalRateTime,
                    x.FetalRate
                })
                .ToListAsync();

            var liquorData = await context.Liquors
                .Where(i => i.PartographID == partographId && i.IsRowDeleted == false)
                .OrderBy(i => i.LiquorTime)
                .Select(x => new string[]
                {
                    x.LiquorTime.ToString(),
                    x.LiquorDetails
                })
                .ToListAsync();

            var mouldingData = await context.Mouldings
                .Where(i => i.PartographID == partographId && i.IsRowDeleted == false)
                .OrderBy(i => i.MouldingTime)
                .Select(x => new string[]
                {
                    x.MouldingTime.ToString(),
                    x.MouldingDetails
                })
                .ToListAsync();

            var cervixData = await context.Cervixes
                .Where(i => i.PartographID == partographId && i.IsRowDeleted == false)
                .OrderBy(i => i.CervixTime)
                .Select(x => new long[]
                {
                    x.CervixTime,
                    x.CervixDetails
                })
                .ToListAsync();

            var descentData = await context.DescentOfHeads
                .Where(i => i.PartographID == partographId && i.IsRowDeleted == false)
                .OrderBy(i => i.DescentOfHeadTime)
                .Select(x => new long[]
                {
                    x.DescentOfHeadTime,
                    x.DescentOfHeadDetails
                })
                .ToListAsync();

            var contractionsData = await context.Contractions
                .Where(i => i.PartographID == partographId && i.IsRowDeleted == false)
                .OrderBy(i => i.ContractionsTime)
                .Select(x => new string[]
                {
                    x.ContractionsTime.ToString(),
                    x.ContractionsDetails.ToString(),
                    x.Duration
                })
                .ToListAsync();

            var oxytocinData = await context.Oxytocins
                .Where(i => i.PartographID == partographId && i.IsRowDeleted == false)
                .OrderBy(i => i.OxytocinTime)
                .Select(x => new long[]
                {
                    x.OxytocinTime,
                    x.OxytocinDetails
                })
                .ToListAsync();

            var dropsData = await context.Drops
                .Where(i => i.PartographID == partographId && i.IsRowDeleted == false)
                .OrderBy(i => i.DropsTime)
                .Select(x => new long[]
                {
                    x.DropsTime,
                    x.DropsDetails
                })
                .ToListAsync();

            var medicineData = await context.Medicines
                .Where(i => i.PartographID == partographId && i.IsRowDeleted == false)
                .OrderBy(i => i.MedicinesTime)
                .Select(x => new string[]
                {
                    x.MedicinesTime.ToString(),
                    x.MedicinesName
                })
                .ToListAsync();

            var bloodPressureData = await context.BloodPressures
                .Where(i => i.PartographID == partographId && i.IsRowDeleted == false)
                .OrderBy(i => i.BloodPressureTime)
                .Select(x => new long[]
                {
                    x.BloodPressureTime,
                    x.SystolicPressure,
                    x.DiastolicPressure
                })
                .ToListAsync();

            var pulseData = await context.Pulses
                .Where(i => i.PartographID == partographId && i.IsRowDeleted == false)
                .OrderBy(i => i.PulseTime)
                .Select(x => new long[]
                {
                    x.PulseTime,
                    x.PulseDetails
                })
                .ToListAsync();

            var temparatureData = await context.Temperatures
                .Where(i => i.PartographID == partographId && i.IsRowDeleted == false)
                .OrderBy(i => i.TemperatureTime)
                .Select(x => new long[]
                {
                    x.TemperatureTime,
                    x.TemperaturesDetails
                })
                .ToListAsync();

            var proteinData = await context.Proteins
                .Where(i => i.PartographID == partographId && i.IsRowDeleted == false)
                .OrderBy(i => i.ProteinsTime)
                .Select(x => new string[]
                {
                    x.ProteinsTime.ToString(),
                    x.ProteinsDetails
                })
                .ToListAsync();

            var acetoneData = await context.Acetones
                .Where(i => i.PartographID == partographId && i.IsRowDeleted == false)
                .OrderBy(i => i.AcetoneTime)
                .Select(x => new string[]
                {
                    x.AcetoneTime.ToString(),
                    x.AcetonesDetails
                })
                .ToListAsync();

            var volumeData = await context.volumes
                .Where(i => i.PartographID == partographId && i.IsRowDeleted == false)
                .OrderBy(i => i.VolumesTime)
                .Select(x => new string[]
                {
                    x.VolumesTime.ToString(),
                    x.VolumesDetails
                })
                .ToListAsync();

            var partographDetails = new PartographDetailReadDto()
            {
                PartographID = partographId,
                FetalHeartRateData = fetalHeartRatesData,
                LiquorData = liquorData,
                MouldingData = mouldingData,
                CervixData = cervixData,
                DescentData = descentData,
                ContractionsData = contractionsData,
                OxytocinData = oxytocinData,
                AcetoneData = acetoneData,
                BloodPressureData = bloodPressureData,
                DropsData = dropsData,
                MedicineData = medicineData,
                ProteinData = proteinData,
                PulseData = pulseData,
                TemparatureData = temparatureData,
                VolumeData = volumeData
            };
            return partographDetails;
        }
    }
}