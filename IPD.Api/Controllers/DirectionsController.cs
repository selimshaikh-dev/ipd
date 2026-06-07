using IPD.Domain.Dto;
using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IPD.Api.Controllers
{
    /// <summary>
    /// Directions Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DirectionsController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<DirectionsController> logger;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="unitOfWork">IUnitOfWork</param>
        /// <param name="logger">ILogger</param>
        public DirectionsController(IUnitOfWork unitOfWork, ILogger<DirectionsController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        /// <summary>
        /// Add new Directions.
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddDirections([FromBody] DirectionDto direction)
        {
            try
            {
                var directionInDb = new Direction
                {
                    DirectionDetails = direction.DirectionDetails,
                };

                var directionAdded = unitOfWork.DirectionRepository.Add(directionInDb);
                await unitOfWork.SaveChangesAsync();

                var directionToReturn =
                    await unitOfWork.DirectionRepository.GetByIdAsync(directionAdded.DirectionID);

                return Ok(directionToReturn);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// GetAll direction
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> LoadDirections()
        {
            try
            {
                var directionInDb = await unitOfWork.DirectionRepository.GetAll()
                    .Where(x => x.IsRowDeleted.Equals(false))
                    .ToListAsync();

                return Ok(directionInDb);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Find directionByKey
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{key}")]
        public async Task<IActionResult> FindDirectionByKey(int key)
        {
            try
            {
                if (key == 0)
                    return BadRequest("Invalid key!");

                var directionInDb = await unitOfWork.DirectionRepository.GetByIdAsync(key);

                if (directionInDb == null)
                    return NotFound();

                return Ok(directionInDb);
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
        /// <param name="direction"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> EditDirections([FromBody] DirectionDto direction)
        {
            try
            {
                if (direction.DirectionID == Guid.Empty)
                    return BadRequest();

                var directionInDb = await unitOfWork.DirectionRepository.GetByIdAsync(direction.DirectionID);
                if (directionInDb == null)
                    return NotFound();

                directionInDb.DirectionDetails = direction.DirectionDetails;

                var directionUpdated = unitOfWork.DirectionRepository.Update(directionInDb);
                await unitOfWork.SaveChangesAsync();

                var directionToReturn =
                    await unitOfWork.DirectionRepository.GetByIdAsync(directionUpdated.DirectionID);

                return Ok(directionToReturn);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }
    }
}