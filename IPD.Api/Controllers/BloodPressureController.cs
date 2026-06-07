using IPD.Domain.Dto;
using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IPD.Api.Controllers
{
    /// <summary>
    ///BloodPressure Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BloodPressureController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<BloodPressureController> logger;

        /// <summary>
        ///Default constructor
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="logger"></param>
        public BloodPressureController(IUnitOfWork unitOfWork, ILogger<BloodPressureController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        /// <summary>
        /// Add new BloodPressure
        /// </summary>
        /// <param name="bloodPressure"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddBloodPressure([FromBody] BloodPressureCreateDto bloodPressure)
        {
            try
            {
                var bloodPressureList = bloodPressure.Data?.Select(x => new BloodPressure()
                {
                    SystolicPressure = Convert.ToInt32(x[1]),
                    DiastolicPressure = Convert.ToInt32(x[2]),
                    BloodPressureTime = Convert.ToInt64(x[0]),
                    PartographID = bloodPressure.PartographID,
                }).ToList() ?? new List<BloodPressure>();

                foreach (var item in bloodPressureList)
                {
                    unitOfWork.BloodPressureRepository.UpdateBloodPressure(item);
                }

                await unitOfWork.SaveChangesAsync();

                return Ok(bloodPressureList);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Load BloodPressure of a patient in specific admission.
        /// </summary>
        /// <param name="partographId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{partographId}")]
        public async Task<IActionResult> LoadBloodPressure(Guid partographId)
        {
            try
            {
                var bloodPressureInDb = await unitOfWork.BloodPressureRepository
                    .GetAll()
                    .Where(c => c.PartographID == partographId && c.IsRowDeleted.Equals(false))
                    .OrderByDescending(o => o.DateCreated)
                    .ToListAsync();

                var data = bloodPressureInDb.Select(x => new long[]
                {
                    DateTime.Now.Ticks,
                    x.SystolicPressure,
                    x.DiastolicPressure
                })
                .OrderBy(i => i[0])
                .ToList();

                var bloodPressure = new BloodPressureCreateDto();
                if (data.Count > 0)
                {
                    bloodPressure = new BloodPressureCreateDto()
                    {
                        PartographID = partographId,
                        Data = data
                    };
                }

                return Ok(bloodPressure);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }
    }
}