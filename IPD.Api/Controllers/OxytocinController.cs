using IPD.Domain.Dto;
using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IPD.Api.Controllers
{
    /// <summary>
    /// Oxytocin controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OxytocinController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<OxytocinController> logger;

        /// <summary>
        /// Oxytocin constructor
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="logger"></param>
        public OxytocinController(IUnitOfWork unitOfWork, ILogger<OxytocinController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        /// <summary>
        /// Add new oxytocin.
        /// </summary>
        /// <param name="oxytocin"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddOxytocin([FromBody] OxytocinCreateDto oxytocin)
        {
            try
            {
                var oxytocinList = oxytocin.Data?.Select(x => new Oxytocin()
                {
                    OxytocinDetails = Convert.ToInt32(x[1]),
                    OxytocinTime = Convert.ToInt64(x[0]),
                    PartographID = oxytocin.PartographID,
                }).ToList() ?? new List<Oxytocin>();

                foreach (var item in oxytocinList)
                {
                    unitOfWork.OxytocinRepository.UpdateOxytocin(item);
                }

                await unitOfWork.SaveChangesAsync();

                return Ok(oxytocinList);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        ///  Load oxytocin  of a patient in specific admission.
        /// </summary>
        /// <param name="partographId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{partographId}")]
        public async Task<IActionResult> LoadOxytocin(Guid partographId)
        {
            try
            {
                var oxytocinInDb = await unitOfWork.OxytocinRepository
                    .GetAll()
                    .Where(p => p.PartographID == partographId && p.IsRowDeleted.Equals(false))
                    .OrderByDescending(o => o.DateCreated)
                    .ToListAsync();

                var data = oxytocinInDb.Select(x => new long[]
                {
                    DateTime.Now.Ticks,
                    x.OxytocinDetails
                })
                .OrderBy(i => i[0])
                .ToList();
                var oxytocins = new OxytocinCreateDto();
                if (data.Count > 0)
                {
                    oxytocins = new OxytocinCreateDto()
                    {
                        PartographID = partographId,
                        Data = data
                    };
                }

                return Ok(oxytocins);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        ///  Find oxytocin post by key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        //[HttpGet]
        //[Route("[action]/{key}")]
        //public async Task<IActionResult> FindoxytocinByKey(Guid key)
        //{
        //    try
        //    {
        //        if (key == Guid.Empty)
        //            return BadRequest("Invalid key!");

        //        var oxytocinInDb = await unitOfWork.OxytocinRepository.GetByIdAsync(key);

        //        if (oxytocinInDb == null)
        //            return NotFound();

        //        return Ok(oxytocinInDb);
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogError(ex.Message);
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
        //    }
        //}

        /// <summary>
        /// Updates existing oxytocin.
        /// </summary>
        /// <param name="oxytocin"></param>
        /// <returns></returns>
        //[HttpPut]
        //[Route("[action]")]
        //public async Task<IActionResult> EditOxytocin([FromBody] OxytocinCreateDto oxytocin)
        //{
        //    try
        //    {
        //        if (oxytocin.OxytocinID == Guid.Empty)
        //            return BadRequest();

        //        var oxytocinInDb = await unitOfWork.OxytocinRepository.GetByIdAsync(oxytocin.OxytocinID);
        //        if (oxytocinInDb == null)
        //            return NotFound();

        //        oxytocinInDb.OxytocinDetails = oxytocin.OxytocinDetails;
        //        //oxytocinInDb.OxytocinTime = oxytocin.OxytocinTime;
        //        oxytocinInDb.PartographID = oxytocin.PartographID;

        //        var updatedoxytocin = unitOfWork.OxytocinRepository.Update(oxytocinInDb);
        //        await unitOfWork.SaveChangesAsync();

        //        var oxytocinToReturn = await unitOfWork.OxytocinRepository.GetByIdAsync(updatedoxytocin.OxytocinID);

        //        return Ok(oxytocinToReturn);
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogError(ex.Message);
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
        //    }
        //}
    }
}