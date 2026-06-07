using IPD.Domain.Dto;
using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using IPD.Infrastructure.Sql;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IPD.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseModel
    {
        protected readonly DataContext _context;

        public Repository(DataContext context)
        {
            this._context = context;
        }

        public T Add(T entity)
        {
            try
            {
                entity.IsRowDeleted = false;
                return _context.Set<T>().Add(entity).Entity;
            }
            catch
            {
                throw;
            }
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
        }

        public T Update(T entity)
        {
            try
            {
                entity.IsRowDeleted = false;
                return _context.Set<T>().Update(entity).Entity;
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<T> GetAll()
        {
            try
            {
                return _context.Set<T>()
                    .AsQueryable()
                    .AsNoTracking().Where(x => x.IsRowDeleted.Equals(false))
                    .ToList();
            }
            catch
            {
                throw;
            }
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public T FirstOrDefault(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            try
            {
                return _context.Set<T>().AsQueryable().AsNoTracking().FirstOrDefault(predicate);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<T?> FirstOrDefaultAsync(Expression<Func<T?, bool>> predicate)
        {
            return await _context.Set<T>().AsQueryable().AsNoTracking().FirstOrDefaultAsync(predicate);
        }

        public IEnumerable<T> Query(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            var entity = _context.Set<T>().AsNoTracking().Where(predicate);
            return entity;
        }

        IQueryable<T> IRepository<T>.GetAll()
        {
            var entity = _context.Set<T>().AsNoTracking();
            return entity;
        }

        IQueryable<T> IRepository<T>.Query(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public virtual T GetById(Guid id)
        {
            try
            {
                var entity = _context.Set<T>().Find(id);
                _context.Entry(entity).State = EntityState.Detached;
                return entity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public T GetById(int Key)
        {
            try
            {
                var entity = _context.Set<T>().Find(Key);
                _context.Entry(entity).State = EntityState.Detached;
                return entity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity == null)
            {
                return null;
            }

            _context.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public async Task<T?> GetByIdAsync(int key)
        {
            var entity = await _context.Set<T>().FindAsync(key);
            if (entity == null)
            {
                return null;
            }

            _context.Entry(entity).State = EntityState.Detached;
            return entity;
        }


        public async Task<IEnumerable<PatientExaminationsDto>> GetAllPatientExamintation(Guid aid)
        {
            try
            {
                var minDate = DateTime.Now.AddMonths(-3);
                var q = await (from c in _context.PatientExaminations
                    .Where(e => e.DateCreated > minDate)
                    .Include(x => x.ExaminationDetails)
                    .Where(e => e.AdmissionID == aid)
                    .Include(e => e.Admissions)
                    .ThenInclude(e => e.BirthDetails)
                               join f in _context.Facilities
                               on c.FacilityCode equals f.FacilityCode into fc
                               from f in fc.DefaultIfEmpty()

                               select new PatientExaminationsDto
                               {
                                   ExaminationDetails = c.ExaminationDetails.Select(e => new ExaminationDetailsDto()
                                   {
                                       DigonosisExaminationID = e.DigonosisExaminationID,
                                       DigonosisExaminationName = e.DiagnosisExamination.DigonosisExaminationsName,
                                       PatientExaminationID = e.PatientExaminationID,
                                   }).ToList(),
                                   AdmissionID = aid,
                                   PatientExaminationID = c.PatientExaminationID,
                                   Findings = c.Findings,
                                   DateCreated = c.DateCreated,
                                   FacilityName = f.FacilityName
                               }).OrderByDescending(e => e.DateCreated).OrderByDescending(e => e.DateCreated).ToListAsync();


                return q;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<PatientDiagnosisDto>> GetAllPatientDiagnosis(Guid aid)
        {
            try
            {
                var minDate = DateTime.Now.AddMonths(-3);
                var q = await (from c in _context.PatientDiagnosis
                               .Where(e => e.DateCreated > minDate).Include(x => x.DiagonosisDetails)
                               //.Where(e => e.DateCreated > minDate).Include(f => f.ICD)
                               .Where(e => e.AdmissionID == aid).Include(e => e.Admissions)
                               .ThenInclude(e => e.BirthDetails)
                               join f in _context.Facilities
                               on c.FacilityCode equals f.FacilityCode into fc
                               from f in fc.DefaultIfEmpty()

                               select new PatientDiagnosisDto
                               {
                                   DiagonosisDetails = c.DiagonosisDetails.Select(e => new DiagonosisDetailsDto()
                                   {
                                       DiagonosisDetailsID = e.DiagonosisDetailsID,
                                       DiseaseID = e.DiseaseID,
                                       PatientDiagnosisID = c.PatientDiagnosisID,
                                   }).ToList(),

                                   //ICDDigonosisCodes = c.ICDDigonosisCodes.Select(x => new ICDDigonosisCodeDto()
                                   //{
                                   //    DiseaseID = x.DiseaseID,
                                   //    ICDCode = x.ICDCode,
                                   //    Description = x.Description,
                                   //    ParentsID = x.ParentsID,
                                   //}).ToList(),

                                   AdmissionID = aid,
                                   PatientDiagnosisID = c.PatientDiagnosisID,
                                   DiagnosisNote = c.DiagnosisNote,
                                   DateCreated = c.DateCreated,
                                   FacilityName = f.FacilityName,

                               }).OrderByDescending(e => e.DateCreated).OrderByDescending(e => e.DateCreated).ToListAsync();


                return q;
            }
            catch
            {
                throw;
            }
        }


        public async Task<IEnumerable<PrescriptionGetDto>> GetLatestPescritionList(Guid admissionId, string facilityCode)
        {
            var facilities = await _context.Facilities
                                .FirstOrDefaultAsync(i => i.IsRowDeleted == false && i.FacilityCode == facilityCode);
            var facilityName = facilities?.FacilityName ?? string.Empty;

            var minDate = DateTime.Now.AddMonths(-3);
            var prescriptionList = await _context.Prescriptions
              .AsNoTracking()
              .Where(x => x.AdmissionID == admissionId && x.DateCreated > minDate).Select(e => new PrescriptionGetDto()
              {
                  DoctorName = e.DoctorName,
                  AdmissionID = admissionId,
                  FacilityName = facilityName,
                  DateCreated = e.DateCreated,
                  PrescriptionsDate = e.PrescriptionsDate,
                  PrescriptionsID = e.PrescriptionsID,
                  MedicationPlanDtos = e.MedicationPlans.Select(z => new MedicationPlanDto()
                  {
                      MedicationName = z.Medications.MedicationName
                  })
                  .ToList()
              }).OrderByDescending(e => e.DateCreated).ToListAsync();

            return prescriptionList;
        }


    }
}