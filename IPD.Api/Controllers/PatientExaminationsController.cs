using IPD.Domain.Dto;
using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IPD.Api.Controllers
{
    /// <summary>
    /// PatientExaminationsController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PatientExaminationsController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<PatientExaminationsController> logger;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="logger"></param>
        public PatientExaminationsController(IUnitOfWork unitOfWork, ILogger<PatientExaminationsController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        /// <summary>
        /// Add PatientExamination.
        /// </summary>
        /// <param name="patientExamination"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddPatientExaminations([FromBody] PatientExaminationsDto patientExamination)
        {
            try
            {
                var patientExaminationInDb = new PatientExamination
                {
                    PatientExaminationID = patientExamination.PatientExaminationID,
                    AdmissionID = patientExamination.AdmissionID,
                    Findings = patientExamination.Findings
                };

                if (patientExamination.ExaminationDetails?.Any() ?? false)
                {
                    patientExaminationInDb.ExaminationDetails = patientExamination.ExaminationDetails.Select(detail =>
                        new ExaminationDetail
                        {
                            DigonosisExaminationID = detail.DigonosisExaminationID,
                            IsRowDeleted = false
                        }).ToList();
                }

                var patientExaminationAdded = unitOfWork.PatientExaminationsRepository.Add(patientExaminationInDb);
                await unitOfWork.SaveChangesAsync();

                var patientExaminationToReturn = await unitOfWork.PatientExaminationsRepository.GetByIdAsync(patientExaminationAdded.PatientExaminationID);

                return Ok(patientExaminationToReturn);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Load PatientExaminations of a patient in specific admission.
        /// </summary>
        /// <param name="admissionId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{admissionId}")]
        public async Task<IActionResult> LoadPatientExaminations(Guid admissionId)
        {
            try
            {
                var examinationInDb = await unitOfWork.PatientExaminationsRepository
                    .GetAllPatientExamintation(admissionId);

                return Ok(examinationInDb);
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
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{key}")]
        public async Task<IActionResult> FindPatientExaminationByKey(Guid key)
        {
            try
            {
                if (key == Guid.Empty)
                    return BadRequest("Invalid key!");

                var patientExaminationInDb = await unitOfWork.PatientExaminationsRepository
                            .GetAll()
                            .Include(i => i.ExaminationDetails)
                            .FirstOrDefaultAsync(x => x.PatientExaminationID == key);

                if (patientExaminationInDb == null)
                    return NotFound();

                return Ok(patientExaminationInDb);
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
        public async Task<IActionResult> FindPatientExaminationByAdmissionID([FromHeader(Name = "x-facility-code")] string facilityCode, Guid admissionId)
        {
            try
            {
                if (admissionId == Guid.Empty)
                    return BadRequest("Invalid key!");

                var patientExaminationInDb = await unitOfWork.PatientExaminationsRepository
                    .GetPatientExaminationsByAdmissionIdAsync(admissionId, facilityCode);

                if (patientExaminationInDb == null)
                    return NotFound();

                return Ok(patientExaminationInDb);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Updates existing PatientExamination.
        /// </summary>
        /// <param name="patientExamination"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> EditPatientExaminations([FromBody] PatientExaminationsDto patientExamination)
        {
            try
            {
                if (patientExamination.PatientExaminationID == Guid.Empty)
                {
                    return BadRequest();
                }

                var patientExaminationInDb = await unitOfWork.PatientExaminationsRepository
                 .GetAll()
                 .Include(x => x.ExaminationDetails)
                 .FirstOrDefaultAsync(x => x.PatientExaminationID == patientExamination.PatientExaminationID);

                if (patientExaminationInDb == null)
                    return NotFound();

                patientExaminationInDb.AdmissionID = patientExamination.AdmissionID;
                patientExaminationInDb.Findings = patientExamination.Findings;

                unitOfWork.ExaminationDetailsRepository.RemoveRange(patientExaminationInDb.ExaminationDetails);

                if (patientExamination.ExaminationDetails?.Any() ?? false)
                {
                    patientExaminationInDb.ExaminationDetails = patientExamination.ExaminationDetails.Select(detail =>
                        new ExaminationDetail
                        {
                            DigonosisExaminationID = detail.DigonosisExaminationID,
                            IsRowDeleted = false
                        }).ToList();
                }
                var patientExaminationUpdated = unitOfWork.PatientExaminationsRepository.Update(patientExaminationInDb);
                await unitOfWork.SaveChangesAsync();

                var patientExaminationToReturn = await unitOfWork.PatientExaminationsRepository.GetByIdAsync(patientExaminationUpdated.PatientExaminationID);

                return Ok(patientExaminationToReturn);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }
    }
}