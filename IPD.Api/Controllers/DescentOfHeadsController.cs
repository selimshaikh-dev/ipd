using IPD.Domain.Dto;
using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IPD.Api.Controllers
{
    /// <summary>
    /// DescentOfHeads controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DescentOfHeadsController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<DescentOfHeadsController> logger;

        /// <summary>
        /// DescentOfHeads constructor
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="logger"></param>
        public DescentOfHeadsController(IUnitOfWork unitOfWork, ILogger<DescentOfHeadsController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        /// <summary>
        /// Add new descentOfHeads.
        /// </summary>
        /// <param name="descentOfHead"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddDescentOfHeads([FromBody] DescentOfHeadCreateDto descentOfHead)
        {
            try
            {
                var descentOfHeadList = descentOfHead.Data?.Select(x => new DescentOfHead()
                {
                    DescentOfHeadDetails = Convert.ToInt32(x[1]),
                    DescentOfHeadTime = Convert.ToInt64(x[0]),
                    PartographID = descentOfHead.PartographID,
                }).ToList() ?? new List<DescentOfHead>();

                foreach (var item in descentOfHeadList)
                {
                    unitOfWork.DescentOfHeadsRepository.UpdateDescentOfHead(item);
                }

                await unitOfWork.SaveChangesAsync();

                return Ok(descentOfHeadList);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        ///  Load descentOfHead  of a patient in specific admission.
        /// </summary>
        /// <param name="partographId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{partographId}")]
        public async Task<IActionResult> LoaddescentOfHead(Guid partographId)
        {
            try
            {
                var descentOfHeadInDb = await unitOfWork.DescentOfHeadsRepository
                    .GetAll()
                    .Where(d => d.PartographID == partographId && d.IsRowDeleted.Equals(false))
                    .OrderByDescending(o => o.DateCreated)
                    .ToListAsync();

                var data = descentOfHeadInDb.Select(x => new long[]
                {
                    DateTime.Now.Ticks,
                    x.DescentOfHeadDetails
                })
                .OrderBy(i => i[0])
                .ToList();

                var descentOfHead = new DescentOfHeadCreateDto();
                if (data.Count > 0)
                {
                    descentOfHead = new DescentOfHeadCreateDto()
                    {
                        PartographID = partographId,
                        Data = data
                    };
                }

                return Ok(descentOfHead);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }
    }
}