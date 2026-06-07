using IPD.Domain.Dto;
using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IPD.Api.Controllers
{
    /// <summary>
    /// SurgicalProceduresController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SurgicalProceduresController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<SurgicalProceduresController> logger;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="unitOfWork">IUnitOfWork</param>
        /// <param name="logger">ILogger</param>
        public SurgicalProceduresController(IUnitOfWork unitOfWork, ILogger<SurgicalProceduresController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        /// <summary>
        /// Add new SurgicalProcedure.
        /// </summary>
        /// <param name="surgicalProcedure"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddSurgicalProcedures([FromBody] SurgicalProceduresDto surgicalProcedure)
        {
            try
            {
                var surgicalProcedureInDb = new SurgicalProcedure
                {
                    ProcedureName = surgicalProcedure.ProcedureName,
                };
                var surgicalProcedureAdded = unitOfWork.SurgicalProceduresRepository.Add(surgicalProcedureInDb);
                await unitOfWork.SaveChangesAsync();

                var surgeryToReturn =
                    await unitOfWork.SurgicalProceduresRepository.GetByIdAsync(surgicalProcedureAdded.SurgicalProcedureID);

                return Ok(surgeryToReturn);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// GetAll SurgicalProcedure
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> LoadSurgicalProcedures()
        {
            try
            {
                var surgicalProceduresInDb = await unitOfWork.SurgicalProceduresRepository.GetAll()
                    .Where(x => x.IsRowDeleted.Equals(false))
                    .ToListAsync();

                return Ok(surgicalProceduresInDb);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Find SurgicalProcedure By Key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{key}")]
        public async Task<IActionResult> FindSurgicalProcedureByKey(int key)
        {
            try
            {
                if (key == 0)
                    return BadRequest("Invalid key!");

                var surgicalProceduresInDb = await unitOfWork.SurgicalProceduresRepository.GetByIdAsync(key);

                if (surgicalProceduresInDb == null)
                    return NotFound();

                return Ok(surgicalProceduresInDb);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Updates existing surgicalProcedure.
        /// </summary>
        /// <param name="surgicalProcedure"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> EditSurgicalProcedures([FromBody] SurgicalProceduresDto surgicalProcedure)
        {
            try
            {
                if (surgicalProcedure.SurgicalProcedureID == 0)
                    return BadRequest();

                var surgicalProceduresInDb = await unitOfWork.SurgicalProceduresRepository.GetByIdAsync(surgicalProcedure.SurgicalProcedureID);
                if (surgicalProceduresInDb == null)
                    return NotFound();

                surgicalProceduresInDb.ProcedureName = surgicalProcedure.ProcedureName;

                var surgicalProcedureUpdated = unitOfWork.SurgicalProceduresRepository.Update(surgicalProceduresInDb);
                await unitOfWork.SaveChangesAsync();

                var surgeryToReturn =
                    await unitOfWork.SurgicalProceduresRepository.GetByIdAsync(surgicalProcedureUpdated.SurgicalProcedureID);

                return Ok(surgeryToReturn);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }
    }
}