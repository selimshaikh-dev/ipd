using IPD.Domain.Dto;
using IPD.Domain.Entities;
using IPD.Domain.Helpers;
using IPD.Infrastructure.Contracts;
using IPD.Infrastructure.Sql;
using Microsoft.EntityFrameworkCore;
using static IPD.Domain.Constants.Enumerators;

namespace IPD.Infrastructure.Repositories
{
    public class PatientsRepository : Repository<Patient>, IPatientsRepository
    {
        private readonly DataContext context;

        public PatientsRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Patient>> GetPatientsByCellPhone(string cellPhone)
        {
            List<Patient> patients = new List<Patient>();
            patients = await context.Patients.Include(e => e.Countries).Include(e => e.Chiefdoms).ThenInclude(x => x.Tinkhundla).AsNoTracking().Where(i => i.Cellphone.Equals(cellPhone)).ToListAsync();
            if (patients.Count > 0)
            {
                patients.FirstOrDefault().DateCreated?.ToString("dd-MM-yyyy");
            }
            return patients;
        }

        public async Task<IEnumerable<Patient>> GetPatientsByNID(string nid)
        {
            var patients = await context.Patients.Include(e => e.Countries).Include(e => e.Chiefdoms).ThenInclude(x => x.Tinkhundla).Where(i => i.NationalID.Equals(nid)).AsNoTracking().ToListAsync();
            return patients;
        }

        public async Task<IEnumerable<Patient>> GetPatientsByPatientId(Guid pid)
        {
            var patients = await context.Patients.Include(e => e.Countries).Include(e => e.Chiefdoms).ThenInclude(x => x.Tinkhundla).Where(i => i.PatientID == pid).AsNoTracking().ToListAsync();
            return patients;
        }

        public async Task<IEnumerable<Patient>> GetPatientsByUHID(string uhid)
        {
            var patients = await context.Patients.Include(e => e.Countries).Include(e => e.Chiefdoms).ThenInclude(x => x.Tinkhundla).Where(i => i.UHID.Equals(uhid)).AsNoTracking().ToListAsync();
            return patients;
        }

        public async Task<IEnumerable<Patient>> GetPatientsByAdvanced(string FirstName, string MiddleName, string LastName, DateTime Dob, byte Sex)
        {
            try
            {
                var dob = Dob.Date;
                var patients = await context.Patients.Include(e => e.Countries).Include(e => e.Chiefdoms).ThenInclude(x => x.Tinkhundla)
                    .AsNoTracking()
                    .Where(i =>
                    i.FirstName.Equals(FirstName.Trim()) &&
                    i.MiddleName.Equals(MiddleName.Trim()) &&
                    i.LastName.Equals(LastName.Trim()) &&
                    i.DOB.Equals(dob) &&
                    i.Sex.Equals(Sex))
                    .ToListAsync();
                return patients;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Patient>> GetPatientsByAdvanced(string FirstName, string MiddleName, string LastName, byte Sex)
        {
            try
            {
                var patients = await context.Patients.Include(e => e.Countries).Include(e => e.Chiefdoms).ThenInclude(x => x.Tinkhundla)
                    .AsNoTracking()
                    .Where(i =>
                    i.FirstName.Equals(FirstName.Trim()) &&
                    i.MiddleName.Equals(MiddleName.Trim()) &&
                    i.LastName.Equals(LastName.Trim()) &&
                    i.Sex.Equals(Sex))
                    .ToListAsync();
                return patients;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Patient>> GetPatientsByAdvanced(string FirstName, string LastName, DateTime Dob, byte Sex)
        {
            try
            {
                var dob = Dob.Date;
                var patients = await context.Patients.Include(e => e.Countries).Include(e => e.Chiefdoms).ThenInclude(x => x.Tinkhundla)
                    .AsNoTracking()
                    .Where(i =>
                    i.FirstName.Equals(FirstName.Trim()) &&
                    i.LastName.Equals(LastName.Trim()) &&
                    i.DOB.Equals(dob) &&
                    i.Sex.Equals(Sex))
                    .ToListAsync();
                return patients;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Patient>> GetPatientsByAdvanced(string FirstName, string LastName, byte Sex)
        {
            try
            {
                var patients = await context.Patients.Include(e => e.Countries).Include(e => e.Chiefdoms).ThenInclude(x => x.Tinkhundla)
                    .AsNoTracking()
                    .Where(i =>
                    i.FirstName.Equals(FirstName.Trim()) &&
                    i.LastName.Equals(LastName.Trim()) &&
                    i.Sex.Equals(Sex))
                    .ToListAsync();
                return patients;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<string> GeneratePatientUHID(DateTime dateOfBirth)
        {
            int maxRetry = 3;
            int retryCount = 1;
            do
            {
                string uhid = GetNextUHID(dateOfBirth);
                if (!await IsExistUHIDAsync(uhid))
                {
                    return uhid;
                }
                retryCount++;
            }
            while (retryCount <= maxRetry);

            return $"H{StringHelpers.GetRandomString(10)}";
        }

        private string GetNextUHID(DateTime dateOfBirth)
        {
            string year = dateOfBirth.Year.ToString().PadLeft(4, '0');
            string month = StringHelpers.GetRandomString(2);
            string day = dateOfBirth.Day.ToString().PadLeft(2, '0');
            string random = StringHelpers.GetRandomString(2);

            return $"H{year}{month}{day}{random}";
        }

        private Task<bool> IsExistUHIDAsync(string uhid)
        {
            return context.Patients.AnyAsync(i => i.UHID.Equals(uhid));
        }

        public IQueryable<Patient> PatientSearchByPin(string Pin)
        {
            var PSPin = _context.Patients.Where(x => x.NationalID == Pin);

            return PSPin;
        }

        public IQueryable<Patient> PatientSearchByPatientId(Guid PatientId)
        {
            var PSId = _context.Patients.Where(x => x.PatientID == PatientId);

            return PSId;
        }

        public IQueryable<Patient> PatientSearchByCellPhone(string CellPhone)
        {
            var PSCell = _context.Patients.Where(x => x.Cellphone == CellPhone);

            return PSCell;
        }

        public async Task<PatientGetDto> GetPatientDetailsById(Guid patientID)
        {
            var patientList = await context.Patients
                            .AsNoTracking()
                            .Where(i=> i.PatientID.Equals(patientID))
                            .Select(i => new PatientGetDto()
                            {
                                PatientID = i.PatientID,
                                UHID = i.UHID,
                                NationalID=i.NationalID,
                                FirstName=i.FirstName,
                                MiddleName=i.MiddleName,
                                LastName=i.LastName,
                                DOB=i.DOB,
                                Sex=i.Sex,
                                MaritalStatus=i.MaritalStatus,
                                ContactAddress=i.ContactAddress,
                                PostalAddress=i.PostalAddress,
                                CellphoneCountryCode=i.CellphoneCountryCode,
                                Cellphone=i.Cellphone,
                                LandPhoneCountryCode=i.LandPhoneCountryCode,
                                LandPhone=i.LandPhone,
                                Email=i.Email,
                                IsDeceased=i.IsDeceased,
                                DateDeceased=i.DateDeceased,
                                dateOfBirth=i.DOB,
                                CountryID=i.CountryID,
                                CountryName=i.Countries.Name,
                                ChiefdomID=i.ChiefdomID,
                                ChiefdomName=i.Chiefdoms.Name,
                                TinkhundlaID=i.Chiefdoms.TinkhundlaID,
                                TinKhundlaName = i.Chiefdoms.Tinkhundla.Name,
                                DateCreated=i.DateCreated ?? default(DateTime)
                            })
                            .ToListAsync();
            var patient = patientList.FirstOrDefault();
            if (patient == null)
            {
                patient = new PatientGetDto();
            }
            patient.MaritalStatusName = Enum.GetName(typeof(MaritalStatuses), patient.MaritalStatus);
            patient.SexName = Enum.GetName(typeof(Sex), patient.Sex);
            return patient;
        }
    }
}