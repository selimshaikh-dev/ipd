using IPD.Domain.Dto;
using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IPD.Api.Controllers
{
    /// <summary>
    /// NcdsController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class NcdsController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<NcdsController> logger;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="logger"></param>
        public NcdsController(IUnitOfWork unitOfWork, ILogger<NcdsController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        /// <summary>
        /// Add new Ncds.
        /// </summary>
        /// <param name="Ncds"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddNcds([FromBody] NcdDto ncds)
        {
            try
            {
                var ncdsInDb = new Ncd
                {
                    NcdName = ncds.NcdName
                };
                var ncdsAdded = unitOfWork.NcdsRepository.Add(ncdsInDb);
                await unitOfWork.SaveChangesAsync();

                var ncdsToReturn =
                    await unitOfWork.NcdsRepository.GetByIdAsync(ncdsAdded.NcdsID);

                return Ok(ncdsToReturn);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// GetAll Ncds
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> LoadNcds()
        {
            try
            {
                var ncdsInDb = await unitOfWork.NcdsRepository.GetAll()
                    .Where(x => x.IsRowDeleted.Equals(false))
                    .ToListAsync();

                return Ok(ncdsInDb);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Find Ncds By Key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{key}")]
        public async Task<IActionResult> FindNcdsByKey(int key)
        {
            try
            {
                if (key == 0)
                    return BadRequest("Invalid key!");

                var ncdsInDb = await unitOfWork.NcdsRepository.GetByIdAsync(key);

                if (ncdsInDb == null)
                    return NotFound();

                return Ok(ncdsInDb);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Updates existing ncds.
        /// </summary>
        /// <param name="ncds"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> EditNcds([FromBody] NcdDto ncds)
        {
            try
            {
                if (ncds.NcdsID == 0)
                    return BadRequest();

                var ncdsInDb = await unitOfWork.NcdsRepository.GetByIdAsync(ncds.NcdsID);
                if (ncdsInDb == null)
                    return NotFound();

                ncdsInDb.NcdName = ncds.NcdName;

                var ncdsUpdated = unitOfWork.NcdsRepository.Update(ncdsInDb);
                await unitOfWork.SaveChangesAsync();

                var ncdsToReturn =
                    await unitOfWork.NcdsRepository.GetByIdAsync(ncdsUpdated.NcdsID);

                return Ok(ncdsToReturn);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }
    }
}