using IPD.Domain.Dto;
using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IPD.Api.Controllers
{
    /// <summary>
    /// Pulse controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PulseController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<PulseController> logger;

        /// <summary>
        /// Pulse constructor
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="logger"></param>
        public PulseController(IUnitOfWork unitOfWork, ILogger<PulseController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        /// <summary>
        /// Add new pulse.
        /// </summary>
        /// <param name="pulse"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddPulse([FromBody] PulseCreateDto pulse)
        {
            try
            {
                var pulseList = pulse.Data?.Select(x => new Pulse()
                {
                    PulseDetails = Convert.ToInt32(x[1]),
                    PulseTime = Convert.ToInt64(x[0]),
                    PartographID = pulse.PartographID,
                }).ToList() ?? new List<Pulse>();

                foreach (var item in pulseList)
                {
                    unitOfWork.PulseRepository.UpdatePulse(item);
                }

                await unitOfWork.SaveChangesAsync();

                return Ok(pulseList);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        ///  Load pulse  of a patient in specific admission.
        /// </summary>
        /// <param name="partographId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{partographId}")]
        public async Task<IActionResult> LoadPulse(Guid partographId)
        {
            try
            {
                var pulseInDb = await unitOfWork.PulseRepository
                    .GetAll()
                    .Where(p => p.PartographID == partographId && p.IsRowDeleted.Equals(false))
                    .OrderByDescending(o => o.DateCreated)
                    .ToListAsync();

                var data = pulseInDb.Select(x => new long[]
                {
                    DateTime.Now.Ticks,
                    x.PulseDetails
                })
                .OrderBy(i => i[0])
                .ToList();
                var pulse = new PulseCreateDto();
                if (data.Count > 0)
                {
                    pulse = new PulseCreateDto()
                    {
                        PartographID = partographId,
                        Data = data
                    };
                }

                return Ok(pulse);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        ///  Find pulse post by key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        //[HttpGet]
        //[Route("[action]/{key}")]
        //public async Task<IActionResult> FindPulseByKey(Guid key)
        //{
        //    try
        //    {
        //        if (key == Guid.Empty)
        //            return BadRequest("Invalid key!");

        //        var pulseInDb = await unitOfWork.PulseRepository.GetByIdAsync(key);

        //        if (pulseInDb == null)
        //            return NotFound();

        //        return Ok(pulseInDb);
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogError(ex.Message);
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
        //    }
        //}

        /// <summary>
        /// Updates existing pulse.
        /// </summary>
        /// <param name="pulse"></param>
        /// <returns></returns>
        //[HttpPut]
        //[Route("[action]")]
        //public async Task<IActionResult> EditPulse([FromBody] PulseCreateDto pulse)
        //{
        //    try
        //    {
        //        if (pulse.PulseID == Guid.Empty)
        //            return BadRequest();

        //        var pulseInDb = await unitOfWork.PulseRepository.GetByIdAsync(pulse.PulseID);
        //        if (pulseInDb == null)
        //            return NotFound();

        //        pulseInDb.PulseDetails = pulse.PulseDetails;
        //        //pulseInDb.Time = pulse.Time;
        //        pulseInDb.PartographID = pulse.PartographID;

        //        var updatedpulse = unitOfWork.PulseRepository.Update(pulseInDb);
        //        await unitOfWork.SaveChangesAsync();

        //        var pulseToReturn = await unitOfWork.PulseRepository.GetByIdAsync(updatedpulse.PulseID);

        //        return Ok(pulseToReturn);
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogError(ex.Message);
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
        //    }
        //}
    }
}