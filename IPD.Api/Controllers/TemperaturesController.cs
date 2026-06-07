using IPD.Domain.Dto;
using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IPD.Api.Controllers
{
    /// <summary>
    /// Temperatures controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TemperaturesController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<TemperaturesController> logger;

        /// <summary>
        /// Temperatures constructor
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="logger"></param>
        public TemperaturesController(IUnitOfWork unitOfWork, ILogger<TemperaturesController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        /// <summary>
        /// Add new temperatures.
        /// </summary>
        /// <param name="temperatures"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddTemperatures([FromBody] TemperaturesCreateDto temperatures)
        {
            try
            {
                var temperaturesList = temperatures.Data?.Select(x => new Temperature()
                {
                    TemperaturesDetails = Convert.ToInt32(x[1]),
                    TemperatureTime = Convert.ToInt64(x[0]),
                    PartographID = temperatures.PartographID,
                }).ToList() ?? new List<Temperature>();

                foreach (var item in temperaturesList)
                {
                    unitOfWork.TemperaturesRepository.UpdateTemperature(item);
                }

                await unitOfWork.SaveChangesAsync();

                return Ok(temperaturesList);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        ///  Load temperatures  of a patient in specific admission.
        /// </summary>
        /// <param name="partographId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{partographId}")]
        public async Task<IActionResult> LoadTemperatures(Guid partographId)
        {
            try
            {
                var temperaturesInDb = await unitOfWork.TemperaturesRepository
                    .GetAll()
                    .Where(c => c.PartographID == partographId && c.IsRowDeleted.Equals(false))
                    .OrderByDescending(o => o.DateCreated)
                    .ToListAsync();

                var data = temperaturesInDb.Select(x => new long[]
                {
                    DateTime.Now.Ticks,
                    x.TemperaturesDetails
                })
                .OrderBy(i => i[0])
                .ToList();

                var temperatures = new TemperaturesCreateDto();
                if (data.Count > 0)
                {
                    temperatures = new TemperaturesCreateDto()
                    {
                        PartographID = partographId,
                        Data = data
                    };
                }

                return Ok(temperatures);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        ///  Find temperatures post by key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        //[HttpGet]
        //[Route("[action]/{key}")]
        //public async Task<IActionResult> FindTemperaturesByKey(Guid key)
        //{
        //    try
        //    {
        //        if (key == Guid.Empty)
        //            return BadRequest("Invalid key!");

        //        var temperaturesInDb = await unitOfWork.TemperaturesRepository.GetByIdAsync(key);

        //        if (temperaturesInDb == null)
        //            return NotFound();

        //        return Ok(temperaturesInDb);
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogError(ex.Message);
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
        //    }
        //}

        /// <summary>
        /// Updates existing temperatures.
        /// </summary>
        /// <param name="temperatures"></param>
        /// <returns></returns>
        //[HttpPut]
        //[Route("[action]")]
        //public async Task<IActionResult> EditTemperatures([FromBody] TemperaturesCreateDto temperatures)
        //{
        //    try
        //    {
        //        if (temperatures.TemperaturesID == Guid.Empty)
        //            return BadRequest();

        //        var temperaturesInDb = await unitOfWork.TemperaturesRepository.GetByIdAsync(temperatures.TemperaturesID);
        //        if (temperaturesInDb == null)
        //            return NotFound();

        //        temperaturesInDb.TemperaturesDetails = temperatures.TemperaturesDetails;
        //        //temperaturesInDb.Time = temperatures.Time;
        //        temperaturesInDb.PartographID = temperatures.PartographID;

        //        var updatedtemperatures = unitOfWork.TemperaturesRepository.Update(temperaturesInDb);
        //        await unitOfWork.SaveChangesAsync();

        //        var temperaturesToReturn = await unitOfWork.TemperaturesRepository.GetByIdAsync(updatedtemperatures.TemperaturesID);

        //        return Ok(temperaturesToReturn);
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogError(ex.Message);
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
        //    }
        //}
    }
}