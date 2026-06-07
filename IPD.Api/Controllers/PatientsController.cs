using IPD.Domain.Dto;
using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace IPD.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<PatientsController> logger;

        public PatientsController(IUnitOfWork unitOfWork, ILogger<PatientsController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        #region SaveOrUpdate

        [HttpPost]
        [Route("SaveOrUpdate")]
        public async Task<IActionResult> SaveOrUpdate([FromHeader(Name = "x-facility-code")] string facilityCode, [FromBody] PatientDto patient)
        {
            try
            {
                if (patient.PatientID == Guid.Empty)
                {
                    patient.UHID = await unitOfWork.PatientsRepository.GeneratePatientUHID(patient.DOB);
                    var patientInDb = new Patient
                    {
                        UHID = patient.UHID,
                        FirstName = patient.FirstName,
                        MiddleName = patient.MiddleName,
                        LastName = patient.LastName,
                        DOB = patient.DOB,
                        Cellphone = patient.Cellphone,
                        Email = patient.Email,
                        CellphoneCountryCode = patient.CellphoneCountryCode,
                        ChiefdomID = patient.ChiefdomID,
                        ContactAddress = patient.ContactAddress,
                        CountryID = patient.CountryID,
                        DateDeceased = patient.DateDeceased,
                        IsDeceased = patient.IsDeceased,
                        LandPhone = patient.LandPhone,
                        LandPhoneCountryCode = patient.LandPhoneCountryCode,
                        MaritalStatus = patient.MaritalStatus,
                        NationalID = patient.NationalID,
                        PostalAddress = patient.PostalAddress,
                        Sex = patient.Sex,
                        FacilityCode = facilityCode
                    };

                    var patientAdd = unitOfWork.PatientsRepository.Add(patientInDb);
                    await unitOfWork.SaveChangesAsync();
                    return Ok(patientAdd);
                }

                var patientinDb = unitOfWork.PatientsRepository.GetById(patient.PatientID);
                if (patientinDb == null)
                {
                    return NotFound();
                }
                patientinDb.NationalID = patient.NationalID;
                patientinDb.FirstName = patient.FirstName;
                patientinDb.MiddleName = patient.MiddleName;
                patientinDb.LastName = patient.LastName;
                patientinDb.DOB = patient.DOB;
                patientinDb.Sex = patient.Sex;
                patientinDb.MaritalStatus = patient.MaritalStatus;
                patientinDb.ContactAddress = patient.ContactAddress;
                patientinDb.PostalAddress = patient.PostalAddress;
                patientinDb.CellphoneCountryCode = patient.CellphoneCountryCode;
                patientinDb.Cellphone = patient.Cellphone;
                patientinDb.LandPhoneCountryCode = patient.LandPhoneCountryCode;
                patientinDb.LandPhone = patient.LandPhone;
                patientinDb.Email = patient.Email;
                patientinDb.CountryID = patient.CountryID;
                patientinDb.ChiefdomID = patient.ChiefdomID;
                patientinDb.DateDeceased = patient.DateDeceased;
                patientinDb.IsDeceased = patient.IsDeceased;

                var pateintUp = unitOfWork.PatientsRepository.Update(patientinDb);
                await unitOfWork.SaveChangesAsync();

                return Ok(pateintUp);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        #endregion SaveOrUpdate

        [HttpGet]
        [Route("GetClientById")]
        public IActionResult GetClientById(Guid PatientId)
        {
            try
            {
                var GetClientByID = unitOfWork.PatientsRepository.GetById(PatientId);
                return Ok(GetClientByID);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetClientDetailsById")]
        public async Task<IActionResult> GetClientDetailsById(Guid PatientId)
        {
            try
            {
                var GetClientDetailsByID = await unitOfWork.PatientsRepository.GetPatientDetailsById(PatientId);
                return Ok(GetClientDetailsByID);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetPatientsByCellPhone")]
        public async Task<IActionResult> GetPatientsByCellPhone(string cellPhone)
        {
            try
            {
                var GetClientByID = await unitOfWork.PatientsRepository.GetPatientsByCellPhone(cellPhone);
                return Ok(GetClientByID);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetPatientsByNID")]
        public async Task<IActionResult> GetPatientsByNID(string nid)
        {
            try
            {
                var GetClientByID = await unitOfWork.PatientsRepository.GetPatientsByNID(nid);
                return Ok(GetClientByID);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetPatientsByUHID")]
        public async Task<IActionResult> GetPatientsByUHID(string uhid)
        {
            try
            {
                var GetClientByID = await unitOfWork.PatientsRepository.GetPatientsByUHID(uhid);
                return Ok(GetClientByID);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("AdvancedSearch")]
        public async Task<IActionResult> AdvancedSearch(AdvancedSearchDto client)
        {
            try
            {
                IEnumerable<Patient> patients = new List<Patient>();

                if (client == null)
                {
                    return NotFound("No match found!");
                }

                if (!string.IsNullOrEmpty(client.FirstName) && !string.IsNullOrEmpty(client.LastName) && client.Sex > 0 && string.IsNullOrEmpty(client.MiddleName) && client.Dob == null)
                    patients = await unitOfWork.PatientsRepository.GetPatientsByAdvanced(client.FirstName.Trim(), client.LastName.Trim(), client.Sex);

                if (!string.IsNullOrEmpty(client.FirstName) && !string.IsNullOrEmpty(client.LastName) && client.Sex > 0 && !string.IsNullOrEmpty(client.MiddleName) && client.Dob == null)
                    patients = await unitOfWork.PatientsRepository.GetPatientsByAdvanced(client.FirstName.Trim(), client.MiddleName.Trim(), client.LastName.Trim(), client.Sex);

                if (!string.IsNullOrEmpty(client.FirstName) && !string.IsNullOrEmpty(client.LastName) && client.Sex > 0 && client.Dob != null && string.IsNullOrEmpty(client.MiddleName))
                    patients = await unitOfWork.PatientsRepository.GetPatientsByAdvanced(client.FirstName.Trim(), client.LastName.Trim(), client.Dob ?? default, client.Sex);

                if (!string.IsNullOrEmpty(client.FirstName) && !string.IsNullOrEmpty(client.LastName) && client.Sex > 0 && client.Dob != null && !string.IsNullOrEmpty(client.MiddleName))
                    patients = await unitOfWork.PatientsRepository.GetPatientsByAdvanced(client.FirstName.Trim(), client.MiddleName.Trim(), client.LastName.Trim(), client.Dob ?? default, client.Sex);

                if (patients.ToList().Count == 0)
                {
                    return NotFound("No match found!");
                }
                else
                {
                    return Ok(patients);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("PinSearchDapper")]
        public async Task<IActionResult> PinSearchDapper(long Pin)
        {
            try
            {
                IEnumerable<Patient> patients = new List<Patient>();
                patients = await unitOfWork.PinSearchRepository.GetByPIN(Pin);

                if (patients.ToList().Count == 0)
                {
                    return NotFound("No match found!");
                }
                else
                {
                    return Ok(patients);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("PatientSearchByPin")]
        public IActionResult PatientSearchByPin(string Pin)
        {
            try
            {
                var PinSearch = unitOfWork.PatientsRepository.PatientSearchByPin(Pin);
                return Ok(PinSearch);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("PatientSearchByPatientId")]
        public IActionResult PatientSearchByPatientId(string PatientId)
        {
            try
            {
                var IdSearch = unitOfWork.PatientsRepository.PatientSearchByPatientId(Guid.Parse(PatientId));
                return Ok(IdSearch);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("PatientSearchByCellPhone")]
        public IActionResult PatientSearchByCellPhone(string CellPhone)
        {
            try
            {
                var CellSearch = unitOfWork.PatientsRepository.PatientSearchByCellPhone(CellPhone);
                return Ok(CellSearch);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}