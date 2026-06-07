using IPD.Domain.Dto;
using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace IPD.Api.Controllers
{
    /// <summary>
    /// Mouldings controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MouldingsController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<MouldingsController> logger;

        /// <summary>
        /// Mouldings constructor
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="logger"></param>
        public MouldingsController(IUnitOfWork unitOfWork, ILogger<MouldingsController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        /// <summary>
        /// Add new mouldings.
        /// </summary>
        /// <param name="moulding"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddMouldings([FromBody] MouldingCreateDto moulding)
        {
            try
            {
                var mouldingList = moulding.Data?.Select(x => new Moulding()
                {
                    MouldingDetails = Convert.ToString(x[1]),
                    MouldingTime = Convert.ToInt64(x[0]),
                    PartographID = moulding.PartographID,
                }).ToList() ?? new List<Moulding>();

                foreach (var item in mouldingList)
                {
                    unitOfWork.MouldingsRepository.UpdateMoulding(item);
                }

                await unitOfWork.SaveChangesAsync();

                return Ok(mouldingList);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        ///// <summary>
        /////  Load moulding  of a patient in specific admission.
        ///// </summary>
        ///// <param name="partographId"></param>
        ///// <returns></returns>
        //[HttpGet]
        //[Route("[action]/{partographId}")]
        //public async Task<IActionResult> LoadMouldings(Guid partographId)
        //{
        //    try
        //    {
        //        var mouldingInDb = await unitOfWork.MouldingsRepository
        //            .GetAll()
        //            .Where(m => m.PartographID == partographId && m.IsRowDeleted.Equals(false))
        //            .OrderByDescending(o => o.DateCreated)
        //            .ToListAsync();

        //        var data = mouldingInDb.Select(x => new long[]
        //        {
        //            x.MouldingTime,
        //            x.MouldingDetails
        //        })
        //        .OrderBy(i => i[0])
        //        .ToList();
        //        var moulding = new MouldingCreateDto();
        //        if (data.Count > 0)
        //        {
        //            moulding = new MouldingCreateDto()
        //            {
        //                PartographID = partographId,
        //                Data = data
        //            };
        //        }

        //        return Ok(moulding);
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogError(ex.Message);
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
        //    }
        //}

        ///// <summary>
        /////  Find moulding post by key
        ///// </summary>
        ///// <param name="key"></param>
        ///// <returns></returns>
        //[HttpGet]
        //[Route("[action]/{key}")]
        //public async Task<IActionResult> FindMouldingByKey(Guid key)
        //{
        //    try
        //    {
        //        if (key == Guid.Empty)
        //            return BadRequest("Invalid key!");

        //        var mouldingInDb = await unitOfWork.MouldingsRepository.GetByIdAsync(key);

        //        if (mouldingInDb == null)
        //            return NotFound();

        //        return Ok(mouldingInDb);
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogError(ex.Message);
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
        //    }
        //}

        ///// <summary>
        ///// Updates existing moulding.
        ///// </summary>
        ///// <param name="moulding"></param>
        ///// <returns></returns>
        //[HttpPut]
        //[Route("[action]")]
        //public async Task<IActionResult> EditMouldings([FromBody] MouldingDTO moulding)
        //{
        //    try
        //    {
        //        if (moulding.MouldingID == Guid.Empty)
        //            return BadRequest();

        //        var mouldingInDb = await unitOfWork.MouldingsRepository.GetByIdAsync(moulding.MouldingID);
        //        if (mouldingInDb == null)
        //            return NotFound();

        //        mouldingInDb.MouldingDetails = moulding.MouldingDetails;
        //        mouldingInDb.MouldingTime = moulding.MouldingTime;
        //        mouldingInDb.PartographID = moulding.PartographID;

        //        var updatedmouldings = unitOfWork.MouldingsRepository.Update(mouldingInDb);
        //        await unitOfWork.SaveChangesAsync();

        //        var mouldingsToReturn = await unitOfWork.MouldingsRepository.GetByIdAsync(updatedmouldings.MouldingID);

        //        return Ok(mouldingsToReturn);
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogError(ex.Message);
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
        //    }
        //}
    }
}