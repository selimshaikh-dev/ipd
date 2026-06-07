using IPD.Domain.Dto;
using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using IPD.Infrastructure.Sql;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IPD.Api.Controllers
{
    /// <summary>
    /// DeathCertificates Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DeathCertificatesController : ControllerBase
    {
        private readonly DataContext context;
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<DeathCertificatesController> logger;

        /// <summary>
        /// DeathCertificates constructor
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="logger"></param>
        public DeathCertificatesController(IUnitOfWork unitOfWork, ILogger<DeathCertificatesController> logger,DataContext context)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
            this.context = context;
        }

        /// <summary>
        /// Saves a DeathCertificates.
        /// </summary>
        /// <param name="deathCertificates"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddDeathCertificates([FromHeader(Name = "x-facility-code")] string facilityCode, [FromBody] DeathCertificateDto deathCertificates)
        {
            try
            {
                var deathCertificateInDb = new DeathCertificate
                {
                    AdmissionID = deathCertificates.AdmissionID,
                    CauseOfDeath = deathCertificates.CauseOfDeath,
                    DateOfDeath = deathCertificates.DateOfDeath,
                    HandOn = deathCertificates.HandOn,
                    HandOver = deathCertificates.HandOver,
                    Indvuna = deathCertificates.Indvuna,
                    Interval = deathCertificates.Interval,
                    OtherReason = deathCertificates.OtherReason,
                    PhysicalAddress = deathCertificates.PhysicalAddress,
                    Resident = deathCertificates.Resident,
                    SpecialInvistigation = deathCertificates.SpecialInvistigation,
                    TimeOfDeath = deathCertificates.TimeOfDeath,
                    FacilityCode = facilityCode,
                };
                // Extra work by Rakib Hasan
                var admission = context.Admissions.Where(w=> w.AdmissionID == deathCertificates.AdmissionID).FirstOrDefault();
                var deathCertificateAdded = unitOfWork.DeathCertificateRepository.Add(deathCertificateInDb);
                if (admission != null) admission.IsDischarged = true;        
                await unitOfWork.SaveChangesAsync();
               
                // End extra work by Rakib Hasan

                var deathCertificateToReturn =
                    await unitOfWork.DeathCertificateRepository.GetByIdAsync(deathCertificateAdded.DeathCertificateID);

                return Ok(deathCertificateToReturn);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        ///  Updates existing  DeathCertificates.
        /// </summary>
        /// <param name="deathCertificates"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> EditDeathCertificates([FromBody] DeathCertificateDto deathCertificates)
        {
            try
            {
                if (deathCertificates.DeathCertificateID == Guid.Empty)
                {
                    return BadRequest();
                }

                var deathCertificateEntity =
                    await unitOfWork.DeathCertificateRepository.GetByIdAsync(deathCertificates.DeathCertificateID);
                if (deathCertificateEntity == null)
                {
                    return NotFound();
                }
               
                deathCertificateEntity.AdmissionID = deathCertificates.AdmissionID;

                deathCertificateEntity.CauseOfDeath = deathCertificates.CauseOfDeath;
                deathCertificateEntity.DateOfDeath = deathCertificates.DateOfDeath;
                deathCertificateEntity.HandOn = deathCertificates.HandOn;
                deathCertificateEntity.HandOver = deathCertificates.HandOver;
                deathCertificateEntity.Indvuna = deathCertificates.Indvuna;
                deathCertificateEntity.Interval = deathCertificates.Interval;
                deathCertificateEntity.OtherReason = deathCertificates.OtherReason;
                deathCertificateEntity.PhysicalAddress = deathCertificates.PhysicalAddress;
                deathCertificateEntity.Resident = deathCertificates.Resident;
                deathCertificateEntity.SpecialInvistigation = deathCertificates.SpecialInvistigation;
                deathCertificateEntity.TimeOfDeath = deathCertificates.TimeOfDeath;

                var deathCertificateUpdated = unitOfWork.DeathCertificateRepository.Update(deathCertificateEntity);
                await unitOfWork.SaveChangesAsync();

                var deathCertificateToReturn =
                    await unitOfWork.DeathCertificateRepository.GetByIdAsync(deathCertificateUpdated
                        .DeathCertificateID);

                return Ok(deathCertificateToReturn);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Finds a DeathCertificate info  from DeathCertificate table using primary key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{key}")]
        public async Task<IActionResult> FindDeathCertificateByKey(Guid key)
        {
            try
            {
                if (key == Guid.Empty)
                    return BadRequest("Invalid key!");

                var deathCertificates = await unitOfWork.DeathCertificateRepository.GetByIdAsync(key);
                if (deathCertificates == null)
                {
                    return NotFound();
                }

                return Ok(deathCertificates);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Load DeathCertificate of a Admission in specific admission.
        /// </summary>
        /// <param name="admissionId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{admissionId}")]
        public async Task<IActionResult> LoadDeathCertificates(Guid admissionId)
        {
            try
            {
                var deathCertificates = await unitOfWork.DeathCertificateRepository
                    .GetAll()
                    .Where(x => x.AdmissionID == admissionId && x.IsRowDeleted.Equals(false))
                    .ToListAsync();

                return Ok(deathCertificates);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// check if patient has death certificate or not.
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{patientId}")]
        public async Task<IActionResult> HasDeathCertificates(Guid patientId)
        {
            try
            {
                var hasDeathCertificates = await unitOfWork.AdmissionRepository
                    .GetAll()
                    .Where(x => x.PatientID == patientId && x.IsRowDeleted.Equals(false))
                    .SelectMany(x => x.DeathCertificates)
                    .Where(x => x.IsRowDeleted.Equals(false))
                    .AnyAsync();

                return Ok(hasDeathCertificates);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }
    }
}