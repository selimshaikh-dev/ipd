using IPD.Domain.Dto;
using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IPD.Api.Controllers
{
    /// <summary>
    /// Drops Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DropsController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<DropsController> logger;

        /// <summary>
        ///Default constructor
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="logger"></param>
        public DropsController(IUnitOfWork unitOfWork, ILogger<DropsController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        /// <summary>
        /// Add new Drops
        /// </summary>
        /// <param name="drops"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddDrops([FromBody] DropCreateDto drops)
        {
            try
            {
                var dropsList = drops.Data?.Select(x => new Drop()
                {
                    DropsDetails = Convert.ToInt32(x[1]),
                    DropsTime = Convert.ToInt64(x[0]),
                    PartographID = drops.PartographID,
                }).ToList() ?? new List<Drop>();

                foreach (var item in dropsList)
                {
                    unitOfWork.DropsRepository.UpdateDrop(item);
                }

                await unitOfWork.SaveChangesAsync();

                return Ok(dropsList);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Load Drops of a patient in specific admission.
        /// </summary>
        /// <param name="partographId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{partographId}")]
        public async Task<IActionResult> LoadDrops(Guid partographId)
        {
            try
            {
                var dropsInDb = await unitOfWork.DropsRepository
                    .GetAll()
                    .Where(p => p.PartographID == partographId && p.IsRowDeleted.Equals(false))
                    .OrderByDescending(o => o.DateCreated)
                    .ToListAsync();

                var data = dropsInDb.Select(x => new long[]
                {
                    DateTime.Now.Ticks,
                    x.DropsDetails
                })
                .OrderBy(i => i[0])
                .ToList();
                var drops = new DropCreateDto();
                if (data.Count > 0)
                {
                    drops = new DropCreateDto()
                    {
                        PartographID = partographId,
                        Data = data
                    };
                }

                return Ok(drops);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Finds patient's drops information from Drops table using primary key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        //[HttpGet]
        //[Route("[action]/{key}")]
        //public async Task<IActionResult> FindDropsByKey(Guid key)
        //{
        //    try
        //    {
        //        if (key == Guid.Empty)
        //            return BadRequest("Invalid key!");

        //        var dropsInDb = await unitOfWork.DropsRepository.GetByIdAsync(key);

        //        if (dropsInDb == null)
        //            return NotFound();

        //        return Ok(dropsInDb);
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogError(ex.Message);
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
        //    }
        //}

        /// <summary>
        /// Updates existing drops.
        /// </summary>
        /// <param name="drops"></param>
        /// <returns></returns>
        //[HttpPut]
        //[Route("[action]")]
        //public async Task<IActionResult> EditDrops([FromBody] DropCreateDto drops)
        //{
        //    try
        //    {
        //        if (drops.DropsID == Guid.Empty)
        //            return BadRequest();

        //        var dropsInDb = await unitOfWork.DropsRepository.GetByIdAsync(drops.DropsID);
        //        if (dropsInDb == null)
        //            return NotFound();

        //        dropsInDb.DropsDetails = drops.DropsDetails;
        //        //dropsInDb.DropsTime = drops.DropsTime;
        //        dropsInDb.PartographID = drops.PartographID;

        //        var updatedDrops = unitOfWork.DropsRepository.Update(dropsInDb);
        //        await unitOfWork.SaveChangesAsync();

        //        var dropsInDbToReturn = await unitOfWork.DropsRepository.GetByIdAsync(updatedDrops.DropsID);

        //        return Ok(dropsInDbToReturn);
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogError(ex.Message);
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
        //    }
        //}
    }
}