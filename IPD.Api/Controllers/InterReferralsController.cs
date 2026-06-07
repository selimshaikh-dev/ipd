using IPD.Domain.Dto;
using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IPD.Api.Controllers
{
    /// <summary>
    /// InterDepartmentReferral Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class InterReferralsController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<InterReferralsController> logger;

        /// <summary>
        /// InterDepartmentReferral constructor
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="logger"></param>
        public InterReferralsController(IUnitOfWork unitOfWork, ILogger<InterReferralsController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        /// <summary>
        /// Saves a Referral.
        /// </summary>
        /// <param name="interDepartmentReferral"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddReferrals([FromHeader(Name = "x-facility-code")] string facilityCode, [FromBody] InterDepartmentReferralDto interDepartmentReferral)
        {
            try
            {
                var referralEntity = new InterDepartmentReferral
                {
                    InterDepartmentReferralsID = interDepartmentReferral.InterDepartmentReferralsID,
                    DepartmentID = interDepartmentReferral.DepartmentID,
                    ReferralTo = interDepartmentReferral.ReferralTo,
                    Date = interDepartmentReferral.Date,
                    Time = interDepartmentReferral.Time,
                    Ward = interDepartmentReferral.Ward,
                    ReasonOfReferral = interDepartmentReferral.ReasonOfReferral,
                    ReferralOfficer = interDepartmentReferral.ReferralOfficer,
                    Feedback = interDepartmentReferral.Feedback,
                    ConsultingOfficer = interDepartmentReferral.ConsultingOfficer,
                    AdmissionID = interDepartmentReferral.AdmissionID,
                    FacilityCode = facilityCode
                };
                var referralDtoAdded = unitOfWork.InterReferralsRepository.Add(referralEntity);
                await unitOfWork.SaveChangesAsync();

                var referralToReturn = await unitOfWork.InterReferralsRepository.GetByIdAsync(referralDtoAdded.InterDepartmentReferralsID);

                return Ok(referralToReturn);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        ///  Updates existing  InterDepartmentReferral.
        /// </summary>
        /// <param name="interDepartmentReferral"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> EditReferrals([FromBody] InterDepartmentReferralDto interDepartmentReferral)
        {
            try
            {
                if (interDepartmentReferral.InterDepartmentReferralsID == Guid.Empty)
                {
                    return BadRequest();
                }

                var referralEntity = await unitOfWork.InterReferralsRepository.GetByIdAsync(interDepartmentReferral.InterDepartmentReferralsID);
                if (referralEntity == null)
                {
                    return NotFound();
                }

                referralEntity.DepartmentID = interDepartmentReferral.DepartmentID;
                referralEntity.ReferralTo = interDepartmentReferral.ReferralTo;
                referralEntity.Date = interDepartmentReferral.Date;
                referralEntity.Time = interDepartmentReferral.Time;
                referralEntity.Ward = interDepartmentReferral.Ward;
                referralEntity.ReasonOfReferral = interDepartmentReferral.ReasonOfReferral;
                referralEntity.ReferralOfficer = interDepartmentReferral.ReferralOfficer;
                referralEntity.Feedback = interDepartmentReferral.Feedback;
                referralEntity.ConsultingOfficer = interDepartmentReferral.ConsultingOfficer;
                referralEntity.AdmissionID = interDepartmentReferral.AdmissionID;

                var interDepartmentReferralUpdated = unitOfWork.InterReferralsRepository.Update(referralEntity);
                await unitOfWork.SaveChangesAsync();

                var referralToReturn = await unitOfWork.InterReferralsRepository.GetByIdAsync(interDepartmentReferralUpdated.InterDepartmentReferralsID);

                return Ok(referralToReturn);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Finds a InterDepartmentReferral info  from InterDepartmentReferrals table using primary key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{key}")]
        public async Task<IActionResult> FindReferralByKey(Guid key)
        {
            try
            {
                var interDepartmentReferral = await unitOfWork.InterReferralsRepository.GetByIdAsync(key);
                if (interDepartmentReferral == null)
                {
                    return NotFound();
                }

                return Ok(interDepartmentReferral);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Load InterDepartmentReferral of a Admission in specific admission.
        /// </summary>
        /// <param name="admissionId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{admissionId}")]
        public async Task<IActionResult> LoadReferrals(Guid admissionId)
        {
            try
            {
                var interDepartmentReferral = await unitOfWork.InterReferralsRepository
                    .GetAll()
                    .Where(x => x.AdmissionID == admissionId && x.IsRowDeleted.Equals(false))
                    .OrderByDescending(o => o.DateCreated)
                    .ToListAsync();

                return Ok(interDepartmentReferral);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }
    }
}