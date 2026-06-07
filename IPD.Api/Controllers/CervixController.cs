using IPD.Domain.Dto;
using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IPD.Api.Controllers
{
    /// <summary>
    /// Cervix controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CervixController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<CervixController> logger;

        /// <summary>
        /// Mouldings constructor
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="logger"></param>
        public CervixController(IUnitOfWork unitOfWork, ILogger<CervixController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        /// <summary>
        /// Add new cervix.
        /// </summary>
        /// <param name="cervix"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddCervix([FromBody] CervixCreateDto cervix)
        {
            try
            {
                var cervixList = cervix.Data?.Select(x => new Cervix()
                {
                    CervixDetails = Convert.ToInt32(x[1]),
                    CervixTime = Convert.ToInt64(x[0]),
                    PartographID = cervix.PartographID,
                }).ToList() ?? new List<Cervix>();

                foreach (var item in cervixList)
                {
                    unitOfWork.CervixRepository.UpdateCervix(item);
                }

                await unitOfWork.SaveChangesAsync();

                return Ok(cervixList);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        ///  Load cervix  of a patient in specific admission.
        /// </summary>
        /// <param name="partographId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{partographId}")]
        public async Task<IActionResult> LoadCervix(Guid partographId)
        {
            try
            {
                var cervixInDb = await unitOfWork.CervixRepository
                    .GetAll()
                    .Where(c => c.PartographID == partographId && c.IsRowDeleted.Equals(false))
                    .OrderByDescending(o => o.DateCreated)
                    .ToListAsync();

                var data = cervixInDb.Select(x => new long[]
                {
                    DateTime.Now.Ticks,
                    //x.CervixTime,
                    x.CervixDetails
                })
                .OrderBy(i => i[0])
                .ToList();

                var cervix = new CervixCreateDto();
                if (data.Count > 0)
                {
                    cervix = new CervixCreateDto()
                    {
                        PartographID = partographId,
                        Data = data
                    };
                }

                return Ok(cervix);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        ///// <summary>
        /////  Find cervix post by key
        ///// </summary>
        ///// <param name="key"></param>
        ///// <returns></returns>
        //[HttpGet]
        //[Route("[action]/{key}")]
        //public async Task<IActionResult> FindCervixByKey(Guid key)
        //{
        //    try
        //    {
        //        if (key == Guid.Empty)
        //            return BadRequest("Invalid key!");

        //        var cervixInDb = await unitOfWork.CervixRepository.GetByIdAsync(key);

        //        if (cervixInDb == null)
        //            return NotFound();

        //        return Ok(cervixInDb);
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogError(ex.Message);
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
        //    }
        //}

        ///// <summary>
        ///// Updates existing cervix.
        ///// </summary>
        ///// <param name="cervix"></param>
        ///// <returns></returns>
        //[HttpPut]
        //[Route("[action]")]
        //public async Task<IActionResult> EditCervix([FromBody] CervixDto cervix)
        //{
        //    try
        //    {
        //        if (cervix.CervixID == Guid.Empty)
        //            return BadRequest();

        //        var cervixInDb = await unitOfWork.CervixRepository.GetByIdAsync(cervix.CervixID);
        //        if (cervixInDb == null)
        //            return NotFound();

        //        cervixInDb.CervixDetails = cervix.CervixDetails;
        //        cervixInDb.CervixTime = cervix.CervixTime;
        //        cervixInDb.PartographID = cervix.PartographID;

        //        var updatedcervix = unitOfWork.CervixRepository.Update(cervixInDb);
        //        await unitOfWork.SaveChangesAsync();

        //        var cervixToReturn = await unitOfWork.CervixRepository.GetByIdAsync(updatedcervix.CervixID);

        //        return Ok(cervixToReturn);
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogError(ex.Message);
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
        //    }

        //}
    }
}