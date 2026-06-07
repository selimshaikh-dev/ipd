using IPD.Domain.Dto;
using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IPD.Api.Controllers
{
    /// <summary>
    /// PatientDiagnosisController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PatientDiagnosisController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<PatientDiagnosisController> logger;

        /// <summary>
        /// PatientDiagnosis constructor
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="logger"></param>
        public PatientDiagnosisController(IUnitOfWork unitOfWork, ILogger<PatientDiagnosisController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        /// <summary>
        /// Saves a PatientDiagnosis.
        /// </summary>
        /// <param name="facilityCode"></param>
        /// <param name="patientDiagnosis"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddPatientDiagnosis([FromHeader(Name = "x-facility-code")] string facilityCode, [FromBody] PatientDiagnosisDto patientDiagnosis)
        {
            try
            {
                var patientDiagnosisInDb = new PatientDiagnosis
                {
                    DiagnosisNote = patientDiagnosis.DiagnosisNote,
                    AdmissionID = patientDiagnosis.AdmissionID,
                    FacilityCode = facilityCode
                };

                if (patientDiagnosis.DiagonosisDetails.Any())
                {
                    patientDiagnosisInDb.DiagonosisDetails = patientDiagnosis.DiagonosisDetails.Select(detail =>
                        new DiagonosisDetail
                        {
                            DiseaseID = detail.DiseaseID,
                            IsRowDeleted = false
                        }).ToList();
                }

                var patientDiagnosisAdded = unitOfWork.PatientDiagnosisRepository.Add(patientDiagnosisInDb);
                await unitOfWork.SaveChangesAsync();

                var patientDiagnosisToReturn = await unitOfWork.PatientDiagnosisRepository.GetByIdAsync(patientDiagnosisAdded.PatientDiagnosisID);

                return Ok(patientDiagnosisToReturn);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Load Admission  of a patient in specific admission.
        /// </summary>
        /// <param name="admissionId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{admissionId}")]
        public async Task<IActionResult> LoadPatientDiagnosis(Guid admissionId)
        {
            try
            {
                var patientDiagnosisInDb = await unitOfWork.PatientDiagnosisRepository
                .GetAllPatientDiagnosis(admissionId);

                return Ok(patientDiagnosisInDb);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        ///  Find PatientDiagnosis by key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{key}")]
        public async Task<IActionResult> FindPatientDiagnosisByKey(Guid key)
        {
            try
            {
                if (key == Guid.Empty)
                    return BadRequest("Invalid key!");

                var patientDiagnosisInDb = await unitOfWork.PatientDiagnosisRepository
                    .GetAll()
                    .Include(x => x.DiagonosisDetails)
                    .FirstOrDefaultAsync(x => x.PatientDiagnosisID == key);

                if (patientDiagnosisInDb == null)
                    return NotFound();

                return Ok(patientDiagnosisInDb);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        ///  Find PatientExaminationBykey
        /// </summary>
        /// <param name="facilityCode"></param>
        /// <param name="admissionId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{admissionId}")]
        public async Task<IActionResult> FindPatientDiagnosisByAdmissionID([FromHeader(Name = "x-facility-code")] string facilityCode, Guid admissionId)
        {
            try
            {
                if (admissionId == Guid.Empty)
                    return BadRequest("Invalid key!");

                var patientDiagnosisInDb = await unitOfWork.PatientDiagnosisRepository
                    .GetPatientDiagnosisByAdmissionIdAsync(admissionId, facilityCode);

                if (patientDiagnosisInDb == null)
                    return NotFound();

                return Ok(patientDiagnosisInDb);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Updates existing PatientDiagnosis.
        /// </summary>
        /// <param name="patientDiagnosis"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> EditPatientDiagnosis([FromBody] PatientDiagnosisDto patientDiagnosis)
        {
            try
            {
                if (patientDiagnosis.PatientDiagnosisID == Guid.Empty)
                    return BadRequest();

                var patientDiagnosisInDb = await unitOfWork.PatientDiagnosisRepository
                    .GetAll()
                    .Include(x => x.DiagonosisDetails)
                    .FirstOrDefaultAsync(x => x.PatientDiagnosisID == patientDiagnosis.PatientDiagnosisID);

                if (patientDiagnosisInDb == null)
                    return NotFound();

                patientDiagnosisInDb.DiagnosisNote = patientDiagnosis.DiagnosisNote;
                patientDiagnosisInDb.AdmissionID = patientDiagnosis.AdmissionID;

                unitOfWork.DiagonosisDetailRepository.RemoveRange(patientDiagnosisInDb.DiagonosisDetails);
                if (patientDiagnosis.DiagonosisDetails.Any())
                {
                    patientDiagnosisInDb.DiagonosisDetails = patientDiagnosis.DiagonosisDetails.Select(detail =>
                        new DiagonosisDetail
                        {
                            DiseaseID = detail.DiseaseID,
                            PatientDiagnosisID = patientDiagnosisInDb.PatientDiagnosisID,
                            IsRowDeleted = false
                        }).ToList();
                }

                var patientDiagnosisUpdated = unitOfWork.PatientDiagnosisRepository.Update(patientDiagnosisInDb);
                await unitOfWork.SaveChangesAsync();

                var patientDiagnosisToReturn = await unitOfWork.PatientDiagnosisRepository.GetByIdAsync(patientDiagnosisUpdated.PatientDiagnosisID);

                return Ok(patientDiagnosisToReturn);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }
    }
}