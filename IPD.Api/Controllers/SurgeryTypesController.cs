using IPD.Domain.Dto;
using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IPD.Api.Controllers
{
    /// <summary>
    /// SurgeryTypesController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SurgeryTypesController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<SurgeryTypesController> logger;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="unitOfWork">IUnitOfWork</param>
        /// <param name="logger">ILogger</param>
        public SurgeryTypesController(IUnitOfWork unitOfWork, ILogger<SurgeryTypesController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        /// <summary>
        /// Add new SurgeryType.
        /// </summary>
        /// <param name="surgeryType"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddSurgeryTypes([FromBody] SurgeryTypeDto surgeryType)
        {
            try
            {
                var surgeryTypeInDb = new SurgeryType
                {
                    TypeName = surgeryType.TypeName
                };
                var surgeryTypeAdded = unitOfWork.SurgeryTypesRepository.Add(surgeryTypeInDb);
                await unitOfWork.SaveChangesAsync();

                var surgeryToReturn =
                    await unitOfWork.SurgeryTypesRepository.GetByIdAsync(surgeryTypeAdded.SurgeryTypeID);

                return Ok(surgeryToReturn);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// GetAll SurgeryType
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> LoadSurgeryTypes()
        {
            try
            {
                var surgeryTypesInDb = await unitOfWork.SurgeryTypesRepository.GetAll()
                    .Where(x => x.IsRowDeleted.Equals(false))
                    .ToListAsync();

                return Ok(surgeryTypesInDb);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Find SurgeryType By Key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{key}")]
        public async Task<IActionResult> FindSurgeryTypeByKey(int key)
        {
            try
            {
                if (key == 0)
                    return BadRequest("Invalid key!");

                var surgeryTypesInDb = await unitOfWork.SurgeryTypesRepository.GetByIdAsync(key);

                if (surgeryTypesInDb == null)
                    return NotFound();

                return Ok(surgeryTypesInDb);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Updates existing surgeryType.
        /// </summary>
        /// <param name="surgeryType"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> EditSurgeryTypes([FromBody] SurgeryTypeDto surgeryType)
        {
            try
            {
                if (surgeryType.SurgeryTypeID == 0)
                    return BadRequest();

                var surgeryTypesInDb = await unitOfWork.SurgeryTypesRepository.GetByIdAsync(surgeryType.SurgeryTypeID);
                if (surgeryTypesInDb == null)
                    return NotFound();

                surgeryTypesInDb.TypeName = surgeryType.TypeName;

                var surgeryTypeUpdated = unitOfWork.SurgeryTypesRepository.Update(surgeryTypesInDb);
                await unitOfWork.SaveChangesAsync();

                var surgeryToReturn =
                    await unitOfWork.SurgeryTypesRepository.GetByIdAsync(surgeryTypeUpdated.SurgeryTypeID);

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