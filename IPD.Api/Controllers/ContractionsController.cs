using IPD.Domain.Dto;
using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IPD.Api.Controllers
{
    /// <summary>
    /// Contractions Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ContractionsController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<ContractionsController> logger;

        /// <summary>
        ///Default constructor
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="logger"></param>
        public ContractionsController(IUnitOfWork unitOfWork, ILogger<ContractionsController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        /// <summary>
        /// Add new Contractions
        /// </summary>
        /// <param name="contractions"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddContractions([FromBody] ContractionsCreateDto contractions)
        {
            try
            {
                var contractionsList = contractions.Data?.Select(x => new Contraction()
                {
                    ContractionsTime = Convert.ToInt64(x[0]),
                    ContractionsDetails = Convert.ToInt32(x[1]),
                    Duration = Convert.ToString(x[2]),
                    PartographID = contractions.PartographID,
                }).ToList() ?? new List<Contraction>();

                foreach (var item in contractionsList)
                {
                    unitOfWork.ContractionsRepository.UpdateContraction(item);
                }

                await unitOfWork.SaveChangesAsync();

                return Ok(contractionsList);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Load Contractions of a patient in specific admission.
        /// </summary>
        /// <param name="partographId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{partographId}")]
        public async Task<IActionResult> LoadContractions(Guid partographId)
        {
            try
            {
                var contractionsInDb = await unitOfWork.ContractionsRepository
                    .GetAll()
                    .Where(p => p.PartographID == partographId && p.IsRowDeleted.Equals(false))
                    .OrderByDescending(o => o.DateCreated)
                    .ToListAsync();

                var data = contractionsInDb.Select(x => new string[]
                {
                    x.ContractionsTime.ToString(),
                    x.ContractionsDetails.ToString(),
                    x.Duration
                })
                .OrderBy(i => i[0])
                .ToList();
                var contractions = new ContractionsCreateDto();
                if (data.Count > 0)
                {
                    contractions = new ContractionsCreateDto()
                    {
                        PartographID = partographId,
                        Data = data
                    };
                }

                return Ok(contractions);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

    }
}