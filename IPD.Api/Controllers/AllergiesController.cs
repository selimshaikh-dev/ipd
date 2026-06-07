using IPD.Domain.Dto;
using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IPD.Api.Controllers
{
    /// <summary>
    /// AllergiesController.
    /// </summary>

    [Route("api/[controller]")]
    [ApiController]
    public class AllergiesController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<AllergiesController> logger;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="logger"></param>
        public AllergiesController(IUnitOfWork unitOfWork, ILogger<AllergiesController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        /// <summary>
        /// Add new Allergy.
        /// </summary>
        /// <param name="allergy"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddAllergies([FromBody] AllergiesDto allergy)
        {
            try
            {
                var allergyInDb = new Allergy
                {
                    AllergiesName = allergy.AllergiesName
                };
                var allergyAdded = unitOfWork.AllergiesRepository.Add(allergyInDb);
                await unitOfWork.SaveChangesAsync();

                var allergyToReturn =
                    await unitOfWork.AllergiesRepository.GetByIdAsync(allergyAdded.AllergiesID);

                return Ok(allergyToReturn);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// GetAll Allergy
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> LoadAllergies()
        {
            try
            {
                var allergyInDb = await unitOfWork.AllergiesRepository.GetAll()
                    .Where(x => x.IsRowDeleted.Equals(false))
                    .ToListAsync();

                return Ok(allergyInDb);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Find Allergy By Key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{key}")]
        public async Task<IActionResult> FindAllergiesByKey(int key)
        {
            try
            {
                if (key == 0)
                    return BadRequest("Invalid key!");

                var allergyInDb = await unitOfWork.AllergiesRepository.GetByIdAsync(key);

                if (allergyInDb == null)
                    return NotFound();

                return Ok(allergyInDb);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Updates existing allergy.
        /// </summary>
        /// <param name="allergy"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> EditAllergies([FromBody] AllergiesDto allergy)
        {
            try
            {
                if (allergy.AllergiesID == 0)
                    return BadRequest();

                var allergyInDb = await unitOfWork.AllergiesRepository.GetByIdAsync(allergy.AllergiesID);
                if (allergyInDb == null)
                    return NotFound();

                allergyInDb.AllergiesName = allergy.AllergiesName;

                var allergyUpdated = unitOfWork.AllergiesRepository.Update(allergyInDb);
                await unitOfWork.SaveChangesAsync();

                var allergyToReturn =
                    await unitOfWork.AllergiesRepository.GetByIdAsync(allergyUpdated.AllergiesID);

                return Ok(allergyToReturn);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }
    }
}