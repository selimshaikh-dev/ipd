using IPD.Domain.Dto;
using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IPD.Api.Controllers
{
    /// <summary>
    /// DiabeticProfilesController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DiabeticProfilesController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<DiabeticProfile> logger;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="unitOfWork">IUnitOfWork</param>
        /// <param name="logger">ILogger</param>
        public DiabeticProfilesController(IUnitOfWork unitOfWork, ILogger<DiabeticProfile> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        /// <summary>
        /// Saves patient's diabetic profile.
        /// </summary>
        /// <param name="diabeticProfile">DiabeticProfile</param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddDiabeticProfile([FromHeader(Name = "x-facility-code")] string facilityCode, [FromBody] DiabeticsProfileDto diabeticProfile)
        {
            try
            {
                var diabeticProfileInDb = new DiabeticProfile
                {
                    DiabeticProfileID = diabeticProfile.DiabeticProfileID,
                    DateCollected = diabeticProfile.DateCollected,
                    TimeCollected = diabeticProfile.TimeCollected,
                    BloodSuger = diabeticProfile.BloodSuger,
                    UrinSuger = diabeticProfile.UrinSuger,
                    UrinKetones = diabeticProfile.UrinKetones,
                    InsulinDose = diabeticProfile.InsulinDose,
                    FacilityCode = facilityCode,
                    AdmissionID = diabeticProfile.AdmissionID,
                    DateCreated = DateTime.Now
                };

                var diabeticProfileAdded = unitOfWork.DiabeticProfileRepository.Add(diabeticProfileInDb);
                await unitOfWork.SaveChangesAsync();

                var diabeticProfileToReturn = await unitOfWork.DiabeticProfileRepository.GetByIdAsync(diabeticProfileAdded.DiabeticProfileID);

                return Ok(diabeticProfileToReturn);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Load  DiabeticProfiles  of a patient in specific admission.
        /// </summary>
        /// <param name="admissionId">Guid</param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{admissionId}")]
        public async Task<IActionResult> LoadDiabeticProfiles(Guid admissionId)
        {
            try
            {
                var diabeticProfileInDb = await unitOfWork.DiabeticProfileRepository
                    .GetAllDiabeticProfile(admissionId);
                return Ok(diabeticProfileInDb);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Finds patient's  Diabetic Profile  from  DiabeticProfiles  table using primary key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{key}")]
        public async Task<IActionResult> FindDiabeticProfileByKey(Guid key)
        {
            try
            {
                if (key == Guid.Empty)
                    return BadRequest("Invalid key!");

                var diabeticProfileInDb = await unitOfWork.DiabeticProfileRepository.GetByIdAsync(key);

                if (diabeticProfileInDb == null)
                    return NotFound();

                return Ok(diabeticProfileInDb);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Updates existing DiabeticProfile.
        /// </summary>
        /// <param name="diabeticProfile">DiabeticProfile</param>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> EditDiabeticProfile([FromBody] DiabeticsProfileDto diabeticProfile)
        {
            try
            {
                if (diabeticProfile.DiabeticProfileID == Guid.Empty)
                    return BadRequest();

                var diabeticProfileInDb = await unitOfWork.DiabeticProfileRepository.GetByIdAsync(diabeticProfile.DiabeticProfileID);

                if (diabeticProfileInDb == null)
                    return NotFound();

                diabeticProfileInDb.DiabeticProfileID = diabeticProfile.DiabeticProfileID;
                diabeticProfileInDb.DateCollected = diabeticProfile.DateCollected;
                diabeticProfileInDb.TimeCollected = diabeticProfile.TimeCollected;
                diabeticProfileInDb.BloodSuger = diabeticProfile.BloodSuger;
                diabeticProfileInDb.UrinSuger = diabeticProfile.UrinSuger;
                diabeticProfileInDb.UrinKetones = diabeticProfile.UrinKetones;
                diabeticProfileInDb.InsulinDose = diabeticProfile.InsulinDose;
                diabeticProfileInDb.FacilityCode = diabeticProfile.FacilityCode;
                diabeticProfileInDb.AdmissionID = diabeticProfile.AdmissionID;

                var UpdatedDiabeticProfile = unitOfWork.DiabeticProfileRepository.Update(diabeticProfileInDb);
                await unitOfWork.SaveChangesAsync();

                var diabeticProfileToReturn = await unitOfWork.DiabeticProfileRepository.GetByIdAsync(UpdatedDiabeticProfile.DiabeticProfileID);

                return Ok(diabeticProfileToReturn);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }
    }
}