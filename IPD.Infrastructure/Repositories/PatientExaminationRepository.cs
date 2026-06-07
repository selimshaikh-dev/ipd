using IPD.Domain.Dto;
using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using IPD.Infrastructure.Sql;
using Microsoft.EntityFrameworkCore;

namespace IPD.Infrastructure.Repositories
{
    public class PatientExaminationRepository : Repository<PatientExamination>, IPatientExaminationsRepository
    {
        #region Private Fields

        private readonly DataContext context;

        #endregion Private Fields

        #region Public Constructors

        public PatientExaminationRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<List<PatientExaminationsDto>> GetPatientExaminationsByAdmissionIdAsync(Guid admissionId, string facilityCode)
        {
            try
            {
                var patientDetails = await context.Admissions
                                        .FirstOrDefaultAsync(i => i.AdmissionID.Equals(admissionId));
                var patientId = patientDetails?.PatientID ?? Guid.Empty;


                var facilities = await context.Facilities
                                      .FirstOrDefaultAsync(i => i.IsRowDeleted == false && i.FacilityCode == facilityCode);
                var facilityName = facilities?.FacilityName ?? string.Empty;


                var minDate = DateTime.Now.AddMonths(-3);

                var patientExaminationList = await context.PatientExaminations
                                .Where(i => i.Admissions.PatientID.Equals(patientId)
                                    && i.DateCreated > minDate)
                                 .Select(i => new PatientExaminationsDto()
                                 {
                                     Findings = i.Findings,
                                     FacilityName = facilityName,
                                     DateCreated = i.DateCreated,
                                     ExaminationDetails = i.ExaminationDetails.Select(k => new ExaminationDetailsDto()
                                     {
                                         ExaminationDetailID = k.ExaminationDetailID,
                                         DigonosisExaminationID = k.DigonosisExaminationID,
                                         DigonosisExaminationName = k.DiagnosisExamination.DigonosisExaminationsName,
                                         DateCreated = k.DateCreated,
                                         PatientExaminationID = k.PatientExaminationID
                                     }).ToList()

                                 }).OrderByDescending(e => e.DateCreated)
                                .ToListAsync();

                return patientExaminationList;

            }
            catch (Exception )
            {
                throw;
            }
        }

        #endregion Public Constructors

        #region Public Methods

        //public async Task<List<PatientExaminationsDto>> GetPatientExaminationsByAdmissionIdAsync(Guid admissionId, string facilityCode)
        //{
        //    try
        //    {
        //        var patientDetails = await context.Admissions
        //                                .FirstOrDefaultAsync(i => i.AdmissionID.Equals(admissionId));
        //        var patientId = patientDetails?.PatientID ?? Guid.Empty;


        //        var facilities = await context.Facilities
        //                              .FirstOrDefaultAsync(i => i.IsRowDeleted == false && i.FacilityCode == facilityCode);
        //        var facilityName = facilities?.FacilityName ?? string.Empty;


        //        var minDate = DateTime.Now.AddMonths(-3);

        //        var patientExaminationList = await context.PatientExaminations
        //                        .Where(i => i.Admissions.PatientID.Equals(patientId)
        //                            && i.DateCreated > minDate)
        //                         .Select(i => new PatientExaminationsDto()
        //                         {
        //                            Findings = i.Findings,
        //                            FacilityName = facilityName,
        //                            DateCreated = i.DateCreated,
        //                            ExaminationDetails = i.ExaminationDetails.Select(k => new ExaminationDetailsDto()
        //                            {
        //                                ExaminationDetailID = k.ExaminationDetailID,
        //                                DigonosisExaminationID = k.DigonosisExaminationID,
        //                                DigonosisExaminationName = k.DiagnosisExamination.DigonosisExaminationsName,
        //                                DateCreated = k.DateCreated,
        //                                PatientExaminationID = k.PatientExaminationID
        //                            }).ToList()

        //                        }).OrderByDescending(e => e.DateCreated)
        //                        .ToListAsync();

        //        return patientExaminationList;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        #endregion Public Methods
    }
}
