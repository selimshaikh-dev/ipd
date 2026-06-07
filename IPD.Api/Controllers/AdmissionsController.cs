using IPD.Domain.Dto;
using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IPD.Api.Controllers
{
    /// <summary>
    /// AdmissionsController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AdmissionsController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<AdmissionsController> logger;

        /// <summary>
        /// constructor for AdmissionsController
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="logger"></param>
        public AdmissionsController(IUnitOfWork unitOfWork, ILogger<AdmissionsController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        /// <summary>
        /// Load Admission  of a patient in specific admission.
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{patientId}")]
        public async Task<IActionResult> LoadAdmission(Guid patientId)
        {
            try
            {
                var admissions = await unitOfWork.AdmissionRepository
                    .GetAll()
                    .Include(x => x.Discharges)
                    .Where(x => x.PatientID == patientId && x.IsRowDeleted.Equals(false))
                    .ToListAsync();

                return Ok(admissions);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Find admission by key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{key}")]
        public async Task<IActionResult> FindAdmissionByKey(Guid key)
        {
            try
            {
                var admission = await unitOfWork.AdmissionRepository.GetByIdAsync(key);
                if (admission == null)
                    return NotFound();

                return Ok(admission);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Add an Admission.
        /// </summary>
        /// <param name="admission"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddAdmission([FromHeader(Name = "x-facility-code")] string facilityCode, [FromBody] AdmissionsDto admission)
        {
            try
            {
                var admissionInDb = new Admission
                {
                    AdmissionID = admission.AdmissionID,
                    AdmissionDate = admission.AdmissionDate,
                    AdmissionTime = admission.AdmissionTime,
                    NextOfKin = admission.NextOfKin,
                    Relationship = admission.Relationship,
                    ContactAddress = admission.ContactAddress,
                    CellphoneCountryCode = admission.CellphoneCountryCode,
                    Cellphone = admission.Cellphone,
                    IsDischarged = admission.IsDischarged,
                    PatientID = admission.PatientID,
                    AssaignDoctor = admission.AssaignDoctor,
                    FacilityCode = facilityCode,
                };
                var admissionAdded = unitOfWork.AdmissionRepository.Add(admissionInDb);
                await unitOfWork.SaveChangesAsync();

                var admissionToReturn = await unitOfWork.AdmissionRepository.GetByIdAsync(admissionAdded.AdmissionID);

                return Ok(admissionToReturn);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Edit an admission
        /// </summary>
        /// <param name="admission"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> EditAdmission(AdmissionsDto admission)
        {
            try
            {
                var admissionEntity = await unitOfWork.AdmissionRepository.GetByIdAsync(admission.AdmissionID);
                if (admissionEntity == null)
                    return NotFound();

                admissionEntity.AdmissionID = admission.AdmissionID;
                admissionEntity.AdmissionDate = admission.AdmissionDate;
                admissionEntity.AdmissionTime = admission.AdmissionTime;
                admissionEntity.AssaignDoctor = admission.AssaignDoctor;
                admissionEntity.NextOfKin = admission.NextOfKin;
                admissionEntity.Relationship = admission.Relationship;
                admissionEntity.ContactAddress = admission.ContactAddress;
                admissionEntity.CellphoneCountryCode = admission.CellphoneCountryCode;
                admissionEntity.Cellphone = admission.Cellphone;
                admissionEntity.IsDischarged = admission.IsDischarged;
                admissionEntity.PatientID = admission.PatientID;

                var admissionUpdated = unitOfWork.AdmissionRepository.Update(admissionEntity);
                await unitOfWork.SaveChangesAsync();

                var admissionToReturn = await unitOfWork.AdmissionRepository.GetByIdAsync(admissionUpdated.AdmissionID);

                return Ok(admissionToReturn);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }
    }
}