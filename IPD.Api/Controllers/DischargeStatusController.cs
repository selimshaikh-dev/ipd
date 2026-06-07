using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IPD.Api.Controllers
{
    /// <summary>
    /// DischargeStatusController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DischargeStatusController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<DischargeStatusController> logger;

        /// <summary>
        /// constructor for DischargeStatusController
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="logger"></param>
        public DischargeStatusController(IUnitOfWork unitOfWork, ILogger<DischargeStatusController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        /// <summary>
        /// Get all discharge status
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> LoadDischargeStatuses()
        {
            try
            {
                var dischargeStatuses = await unitOfWork.DischargeStatusRepository
                    .GetAll()
                    .Where(x => x.IsRowDeleted.Equals(false))
                    .ToListAsync();

                return Ok(dischargeStatuses);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Find DischargeStatus By Key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{key}")]
        public async Task<IActionResult> FindDischargeStatusByKey(Guid key)
        {
            try
            {
                if (key == Guid.Empty)
                    return BadRequest("Invalid key!");

                var dischargeStatus = await unitOfWork.DischargeStatusRepository.GetByIdAsync(key);
                if (dischargeStatus == null)
                    return NotFound();

                return Ok(dischargeStatus);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Add new DischargeStatus.
        /// </summary>
        /// <param name="dischargeStatus"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddSurgeryTypes([FromBody] DischargeStatus dischargeStatus)
        {
            try
            {
                var dischargeStatusAdded = unitOfWork.DischargeStatusRepository.Add(dischargeStatus);
                await unitOfWork.SaveChangesAsync();

                var dischargeStatusToReturn =
                    await unitOfWork.DischargeStatusRepository.GetByIdAsync(dischargeStatusAdded.DischargeStatusID);

                return Ok(dischargeStatusToReturn);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Updates existing DischargeStatus.
        /// </summary>
        /// <param name="dischargeStatus"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> EditSurgeryTypes([FromBody] DischargeStatus dischargeStatus)
        {
            try
            {
                if (dischargeStatus.DischargeStatusID == Guid.Empty)
                    return BadRequest();

                var dischargeStatusInDb = await unitOfWork.DischargeStatusRepository.GetByIdAsync(dischargeStatus.DischargeStatusID);
                if (dischargeStatusInDb == null)
                    return NotFound();

                dischargeStatusInDb.DischargesStatus = dischargeStatus.DischargesStatus;

                var dischargeStatusUpdated = unitOfWork.DischargeStatusRepository.Update(dischargeStatusInDb);
                await unitOfWork.SaveChangesAsync();

                var dischargeStatusToReturn =
                    await unitOfWork.DischargeStatusRepository.GetByIdAsync(dischargeStatusUpdated.DischargeStatusID);

                return Ok(dischargeStatusToReturn);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }
    }
}