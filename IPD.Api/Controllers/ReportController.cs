using IPD.Domain.Dto;
using IPD.Infrastructure.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IPD.Api.Controllers
{
    /// <summary>
    /// Report controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<ReportController> logger;
        public ReportController(IUnitOfWork unitOfWork, ILogger<ReportController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> LoadReport([FromBody] DateTimeDto dateTimeDto)
        {
            try
            {
                var reportInDb = await unitOfWork.ReportRepository
                    .GetAllLoadReports(dateTimeDto);

                return Ok(reportInDb.ToList());
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }
    }
}
