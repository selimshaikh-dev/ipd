using IPD.Domain.Dto;
using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IPD.Api.Controllers
{
    /// <summary>
    /// FetalHeartRates controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class FetalHeartRatesController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<FetalHeartRatesController> logger;

        /// <summary>
        /// FetalHeartRates constructor
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="logger"></param>
        public FetalHeartRatesController(IUnitOfWork unitOfWork, ILogger<FetalHeartRatesController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        /// <summary>
        /// Add new fetalHeartRates.
        /// </summary>
        /// <param name="fetalHeartRates"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddFetalHeartRates([FromBody] FetalHeartRateCreateDto fetalHeartRates)
        {
            try
            {
                var fetalHeartRateList = fetalHeartRates.Data?.Select(x => new FetalHeartRate()
                {
                    FetalRate = Convert.ToInt32(x[1]),
                    FetalRateTime = Convert.ToInt64(x[0]),
                    PartographID = fetalHeartRates.PartographID,
                }).ToList() ?? new List<FetalHeartRate>();

                foreach (var item in fetalHeartRateList)
                {
                    unitOfWork.FetalHeartRatesRepository.UpdateFatalRate(item);
                }

                await unitOfWork.SaveChangesAsync();

                return Ok(fetalHeartRateList);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        ///  Load fetalHeartRates  of a patient in specific admission.
        /// </summary>
        /// <param name="partographId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{partographId}")]
        public async Task<IActionResult> LoadFetalHeartRates(Guid partographId)
        {
            try
            {
                var fetalHeartRatesInDb = await unitOfWork.FetalHeartRatesRepository
                    .GetAll()
                    .Where(p => p.PartographID == partographId && p.IsRowDeleted.Equals(false))
                    .OrderByDescending(o => o.DateCreated)
                    .ToListAsync();

                var data = fetalHeartRatesInDb.Select(x => new long[]
                {
                    x.FetalRateTime,
                    x.FetalRate
                })
                .OrderBy(i => i[0])
                .ToList();
                var fetalHeartRates = new FetalHeartRateCreateDto()
                {
                    PartographID = partographId,
                    Data = data
                };


                return Ok(fetalHeartRates);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        ///// <summary>
        /////  Find fetalHeartRates post by key
        ///// </summary>
        ///// <param name="key"></param>
        ///// <returns></returns>
        //[HttpGet]
        //[Route("[action]/{key}")]
        //public async Task<IActionResult> FindFetalHeartRateByKey(Guid key)
        //{
        //    try
        //    {
        //        if (key == Guid.Empty)
        //            return BadRequest("Invalid key!");

        //        var fetalHeartRatesInDb = await unitOfWork.FetalHeartRatesRepository.GetByIdAsync(key);

        //        if (fetalHeartRatesInDb == null)
        //            return NotFound();

        //        return Ok(fetalHeartRatesInDb);
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogError(ex.Message);
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
        //    }
        //}

        ///// <summary>
        ///// Updates existing fetalHeartRates.
        ///// </summary>
        ///// <param name="fetalHeartRates"></param>
        ///// <returns></returns>
        //[HttpPut]
        //[Route("[action]")]
        //public async Task<IActionResult> EditFetalHeartRates([FromBody] FetalHeartRateDto fetalHeartRates)
        //{
        //    try
        //    {
        //        if (fetalHeartRates.FetalHeartRateID == Guid.Empty)
        //            return BadRequest();

        //        var fetalHeartRatesInDb = await unitOfWork.FetalHeartRatesRepository.GetByIdAsync(fetalHeartRates.FetalHeartRateID);
        //        if (fetalHeartRatesInDb == null)
        //            return NotFound();

        //        fetalHeartRatesInDb.FetalRate = fetalHeartRates.FetalRate;
        //        fetalHeartRatesInDb.FetalRateTime = fetalHeartRates.FetalRateTime;
        //        fetalHeartRatesInDb.PartographID = fetalHeartRates.PartographID;

        //        var updatedfetalHeartRates = unitOfWork.FetalHeartRatesRepository.Update(fetalHeartRatesInDb);
        //        await unitOfWork.SaveChangesAsync();

        //        var fetalHeartRatesToReturn = await unitOfWork.FetalHeartRatesRepository.GetByIdAsync(updatedfetalHeartRates.FetalHeartRateID);

        //        return Ok(fetalHeartRatesToReturn);
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogError(ex.Message);
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
        //    }
        //}
    }
}