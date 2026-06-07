using IPD.Domain.Dto;
using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using IPD.Infrastructure.Sql;
using Microsoft.EntityFrameworkCore;

namespace IPD.Infrastructure.Repositories
{
    public class MedicationPlanRepository : Repository<MedicationPlan>, IMedicationPlanRepository
    {
        private readonly DataContext context;

        public MedicationPlanRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<List<MedicationPlanDto>> GetLatestMedicationPlanAsync(Guid admissionId, string facilityCode)
        {
            var facilities = await context.Facilities
                                  .FirstOrDefaultAsync(i => i.IsRowDeleted == false && i.FacilityCode == facilityCode);
            var facilityName = facilities?.FacilityName ?? string.Empty;

            var minDate = DateTime.Now.AddMonths(-3);

            var medicationList = await context.MedicationPlans
                            .Include(x => x.Medications)
                            .Include(x => x.Intervals)
                            .Include(x => x.Directions)
                            .Where(i => i.Prescriptions.AdmissionID.Equals(admissionId) && i.DateCreated > minDate)
                            .Select(i => new MedicationPlanDto()
                            {
                                Dose = i.Dose,
                                Durations = i.Durations,
                                FacilityName = facilityName,
                                DateCreated = i.DateCreated,
                                Medications = new MedicationDto
                                {
                                    MedicationName = i.Medications.MedicationName,
                                },
                                Intervals = new IntervalDto
                                {
                                    IntervalName = i.Intervals.IntervalName,
                                },
                                Directions = new DirectionDto
                                {
                                    DirectionDetails = i.Directions.DirectionDetails
                                }
                            })
                            .ToListAsync();

            return medicationList;
        }

      
       

        #region Public Methods
        //public async Task<List<MedicationPlanDTO>> GetAllMedicationPlan(Guid prescriptionsID, string facilityCode)
        //{
        //    try
        //    {
        //        var prescriptions = await context.Prescriptions
        //                                .FirstOrDefaultAsync(i => i.PrescriptionsID.Equals(prescriptionsID));
        //        var admissionId = prescriptions?.AdmissionID ?? Guid.Empty;


        //        var facilities = await context.Facilities
        //                              .FirstOrDefaultAsync(i => i.IsRowDeleted == false && i.FacilityCode == facilityCode);
        //        var facilityName = facilities?.FacilityName ?? string.Empty;


        //        var minDate = DateTime.Now.AddMonths(-3);

        //        var medicationList = await context.MedicationPlans
        //                        .Where(i => i.Prescriptions.AdmissionID.Equals(admissionId) && i.DateCreated > minDate)
        //                        .Select(i => new MedicationPlanDTO()
        //                        {
        //                            Dose = i.Dose,
        //                            Durations = i.Durations,
        //                            FacilityName = facilityName,
        //                            DateCreated = i.DateCreated,
        //                            Prescriptions = i.Prescriptions.Select(k => new PrescriptionsDTO()
        //                            {
        //                                MedicationPlanID = k.MedicationPlanID,
        //                                DateCreated = k.DateCreated,
        //                            }).ToList()
        //                        })
        //                        .ToListAsync();

        //        return medicationList;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        #endregion Public Methods
    }
}