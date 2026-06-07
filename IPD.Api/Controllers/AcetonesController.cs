using IPD.Domain.Dto;
using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IPD.Api.Controllers
{
    /// <summary>
    /// Acetones Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AcetonesController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<AcetonesController> logger;

        /// <summary>
        ///Default constructor
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="logger"></param>
        public AcetonesController(IUnitOfWork unitOfWork, ILogger<AcetonesController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        /// <summary>
        /// Add new Acetones
        /// </summary>
        /// <param name="acetones"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddAcetones([FromBody] AcetonesCreateDto acetones)
        {
            try
            {
                var acetonesList = acetones.Data?.Select(x => new Acetone()
                {
                    AcetonesDetails = Convert.ToString(x[1]),
                    AcetoneTime = Convert.ToInt64(x[0]),
                    PartographID = acetones.PartographID,
                }).ToList() ?? new List<Acetone>();
                foreach (var item in acetonesList)
                {
                    unitOfWork.AcetonesRepository.UpdateAcetone(item);
                }

                await unitOfWork.SaveChangesAsync();

                return Ok(acetonesList);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

    }
}