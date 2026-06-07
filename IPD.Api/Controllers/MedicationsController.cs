using IPD.Domain.Dto;
using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IPD.Api.Controllers
{
    /// <summary>
    /// Medications Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MedicationsController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<MedicationsController> logger;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="unitOfWork">IUnitOfWork</param>
        /// <param name="logger">ILogger</param>
        public MedicationsController(IUnitOfWork unitOfWork, ILogger<MedicationsController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        /// <summary>
        /// Add new Medications.
        /// </summary>
        /// <param name="medication"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddMedications([FromBody] MedicationDto medication)
        {
            try
            {
                var medicationInDb = new Medication
                {
                    MedicationName = medication.MedicationName,
                };
                var medicationAdded = unitOfWork.MedicationRepository.Add(medicationInDb);
                await unitOfWork.SaveChangesAsync();

                var medicationToReturn =
                    await unitOfWork.MedicationRepository.GetByIdAsync(medicationAdded.MedicationID);

                return Ok(medicationToReturn);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// GetAll medication
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> LoadMedications()
        {
            try
            {
                var medicationInDb = await unitOfWork.MedicationRepository.GetAll()
                    .Where(x => x.IsRowDeleted.Equals(false))
                    .ToListAsync();

                return Ok(medicationInDb);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Find medicationByKey
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{key}")]
        public async Task<IActionResult> FindMedicationByKey(int key)
        {
            try
            {
                if (key == 0)
                    return BadRequest("Invalid key!");

                var medicationInDb = await unitOfWork.MedicationRepository.GetByIdAsync(key);

                if (medicationInDb == null)
                    return NotFound();

                return Ok(medicationInDb);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Updates existing interval.
        /// </summary>
        /// <param name="surgicalProcedure"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> EditMedications([FromBody] MedicationDto medication)
        {
            try
            {
                if (medication.MedicationID == Guid.Empty)
                    return BadRequest();

                var medicationInDb = await unitOfWork.MedicationRepository.GetByIdAsync(medication.MedicationID);
                if (medicationInDb == null)
                    return NotFound();

                medicationInDb.MedicationName = medication.MedicationName;

                var medicationUpdated = unitOfWork.MedicationRepository.Update(medicationInDb);
                await unitOfWork.SaveChangesAsync();

                var medicationToReturn =
                    await unitOfWork.MedicationRepository.GetByIdAsync(medicationUpdated.MedicationID);

                return Ok(medicationToReturn);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }
    }
}