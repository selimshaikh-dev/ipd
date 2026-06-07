using IPD.Domain.Dto;
using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IPD.Api.Controllers
{
    /// <summary>
    /// ProceduresController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProceduresController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<ProceduresController> logger;

        public ProceduresController(IUnitOfWork unitOfWork, ILogger<ProceduresController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        /// <summary>
        /// Add new Procedure.
        /// </summary>
        /// <param name="Procedure"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddProcedures([FromBody] ProceduresDto procedure)
        {
            try
            {
                var procedureInDb = new Procedure
                {
                    ProcedureName = procedure.ProcedureName
                };
                var procedureAdded = unitOfWork.ProceduresRepository.Add(procedureInDb);
                await unitOfWork.SaveChangesAsync();

                var procedureToReturn =
                    await unitOfWork.ProceduresRepository.GetByIdAsync(procedureAdded.ProcedureID);

                return Ok(procedureToReturn);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// GetAll Procedure
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> LoadProcedures()
        {
            try
            {
                var procedureInDb = await unitOfWork.ProceduresRepository.GetAll()
                    .Where(x => x.IsRowDeleted.Equals(false))
                    .ToListAsync();

                return Ok(procedureInDb);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Find Procedure By Key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{key}")]
        public async Task<IActionResult> FindProcedureByKey(int key)
        {
            try
            {
                if (key == 0)
                    return BadRequest("Invalid key!");

                var procedureInDb = await unitOfWork.ProceduresRepository.GetByIdAsync(key);

                if (procedureInDb == null)
                    return NotFound();

                return Ok(procedureInDb);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Updates existing procedure.
        /// </summary>
        /// <param name="procedure"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> EditProcedures([FromBody] ProceduresDto procedure)
        {
            try
            {
                if (procedure.ProcedureID == 0)
                    return BadRequest();

                var procedureInDb = await unitOfWork.ProceduresRepository.GetByIdAsync(procedure.ProcedureID);
                if (procedureInDb == null)
                    return NotFound();

                procedureInDb.ProcedureName = procedure.ProcedureName;

                var procedureUpdated = unitOfWork.ProceduresRepository.Update(procedureInDb);
                await unitOfWork.SaveChangesAsync();

                var surgeryToReturn =
                    await unitOfWork.ProceduresRepository.GetByIdAsync(procedureUpdated.ProcedureID);

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