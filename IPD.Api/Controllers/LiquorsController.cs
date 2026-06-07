using IPD.Domain.Dto;
using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace IPD.Api.Controllers
{
    /// <summary>
    /// Liquors controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LiquorsController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<LiquorsController> logger;

        /// <summary>
        /// Liquors constructor
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="logger"></param>
        public LiquorsController(IUnitOfWork unitOfWork, ILogger<LiquorsController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        /// <summary>
        /// Add new liquors.
        /// </summary>
        /// <param name="liquor"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddLiquors([FromBody] LiquorCreateDto liquor)
        {
            try
            {
                var liquorList = liquor.Data?.Select(x => new Liquor()
                {
                    LiquorDetails = Convert.ToString(x[1]),
                    LiquorTime = Convert.ToInt64(x[0]),
                    PartographID = liquor.PartographID,
                }).ToList() ?? new List<Liquor>();

                foreach (var item in liquorList)
                {
                    unitOfWork.LiquorsRepository.UpdateLiquor(item);
                }

                await unitOfWork.SaveChangesAsync();

                return Ok(liquorList);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        ///// <summary>
        /////  Load fetalHeartRates  of a patient in specific admission.
        ///// </summary>
        ///// <param name="partographId"></param>
        ///// <returns></returns>
        //[HttpGet]
        //[Route("[action]/{partographId}")]
        //public async Task<IActionResult> LoadLiquors(Guid partographId)
        //{
        //    try
        //    {
        //        var liquorInDb = await unitOfWork.LiquorsRepository
        //            .GetAll()
        //            .Where(l => l.PartographID == partographId && l.IsRowDeleted.Equals(false))
        //            .OrderByDescending(o => o.DateCreated)
        //            .ToListAsync();

        //        var data = liquorInDb.Select(x => new long[]
        //        {
        //            x.LiquorTime,
        //            x.LiquorDetails
        //        })
        //        .OrderBy(i => i[0])
        //        .ToList();
        //        var liquors = new LiquorCreateDto();
        //        if (data.Count > 0)
        //        {
        //            liquors = new LiquorCreateDto()
        //            {
        //                PartographID = partographId,
        //                Data = data
        //            };
        //        }

        //        return Ok(liquors);
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogError(ex.Message);
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
        //    }
        //}

        ///// <summary>
        /////  Find liquor post by key
        ///// </summary>
        ///// <param name="key"></param>
        ///// <returns></returns>
        //[HttpGet]
        //[Route("[action]/{key}")]
        //public async Task<IActionResult> FindLiquorByKey(Guid key)
        //{
        //    try
        //    {
        //        if (key == Guid.Empty)
        //            return BadRequest("Invalid key!");

        //        var liquorInDb = await unitOfWork.LiquorsRepository.GetByIdAsync(key);

        //        if (liquorInDb == null)
        //            return NotFound();

        //        return Ok(liquorInDb);
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogError(ex.Message);
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
        //    }
        //}

        ///// <summary>
        ///// Updates existing liquor.
        ///// </summary>
        ///// <param name="liquor"></param>
        ///// <returns></returns>
        //[HttpPut]
        //[Route("[action]")]
        //public async Task<IActionResult> EditLiquors([FromBody] LiquorDTO liquor)
        //{
        //    try
        //    {
        //        if (liquor.LiquorID == Guid.Empty)
        //            return BadRequest();

        //        var liquorInDb = await unitOfWork.LiquorsRepository.GetByIdAsync(liquor.LiquorID);
        //        if (liquorInDb == null)
        //            return NotFound();

        //        liquorInDb.LiquorDetails = liquor.LiquorDetails;
        //        liquorInDb.LiquorTime = liquor.LiquorTime;
        //        liquorInDb.PartographID = liquor.PartographID;

        //        var updatedliquors = unitOfWork.LiquorsRepository.Update(liquorInDb);
        //        await unitOfWork.SaveChangesAsync();

        //        var liquorsToReturn = await unitOfWork.LiquorsRepository.GetByIdAsync(updatedliquors.LiquorID);

        //        return Ok(liquorsToReturn);
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogError(ex.Message);
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
        //    }

        //}
    }
}