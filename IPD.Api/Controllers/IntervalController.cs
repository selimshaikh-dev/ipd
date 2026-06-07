using IPD.Domain.Dto;
using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IPD.Api.Controllers
{
    /// <summary>
    /// Interval controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class IntervalController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<IntervalController> logger;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="unitOfWork">IUnitOfWork</param>
        /// <param name="logger">ILogger</param>
        public IntervalController(IUnitOfWork unitOfWork, ILogger<IntervalController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        /// <summary>
        /// Add new Interval.
        /// </summary>
        /// <param name="interval"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddIntervals([FromBody] IntervalDto interval)
        {
            try
            {
                var intervalInDb = new Interval
                {
                    IntervalName = interval.IntervalName,
                };
                var intervalAdded = unitOfWork.IntervalRepository.Add(intervalInDb);
                await unitOfWork.SaveChangesAsync();

                var intervalToReturn =
                    await unitOfWork.IntervalRepository.GetByIdAsync(intervalAdded.IntervalID);

                return Ok(intervalToReturn);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// GetAll interval
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> LoadIntervals()
        {
            try
            {
                var intervalInDb = await unitOfWork.IntervalRepository.GetAll()
                    .Where(x => x.IsRowDeleted.Equals(false))
                    .ToListAsync();

                return Ok(intervalInDb);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Find intervalByKey
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{key}")]
        public async Task<IActionResult> FindIntervalByKey(int key)
        {
            try
            {
                if (key == 0)
                    return BadRequest("Invalid key!");

                var intervalInDb = await unitOfWork.IntervalRepository.GetByIdAsync(key);

                if (intervalInDb == null)
                    return NotFound();

                return Ok(intervalInDb);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Updates existing interval.
        /// </summary>
        /// <param name="surgicalProcedure"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> EditIntervals([FromBody] IntervalDto interval)
        {
            try
            {
                if (interval.IntervalID == Guid.Empty)
                    return BadRequest();

                var intervalInDb = await unitOfWork.IntervalRepository.GetByIdAsync(interval.IntervalID);
                if (intervalInDb == null)
                    return NotFound();

                intervalInDb.IntervalName = interval.IntervalName;

                var intervalUpdated = unitOfWork.IntervalRepository.Update(intervalInDb);
                await unitOfWork.SaveChangesAsync();

                var intervalToReturn =
                    await unitOfWork.IntervalRepository.GetByIdAsync(intervalUpdated.IntervalID);

                return Ok(intervalToReturn);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }
    }
}