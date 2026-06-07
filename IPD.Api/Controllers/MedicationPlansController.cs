using IPD.Domain.Dto;
using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IPD.Api.Controllers
{
    /// <summary>
    /// MedicationPlans controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MedicationPlansController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<MedicationPlansController> logger;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="unitOfWork">IUnitOfWork</param>
        /// <param name="logger">ILogger</param>
        public MedicationPlansController(IUnitOfWork unitOfWork, ILogger<MedicationPlansController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        /// <summary>
        /// Add MedicationPlans.
        /// </summary>
        /// <param name="medicationPlan"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddMedicationPlans([FromHeader(Name = "x-facility-code")] string facilityCode, [FromBody] MedicationPlanDto medicationPlan)
        {
            try
            {
                var medicationPlanInDb = new MedicationPlan
                {
                    MedicationPlanID = medicationPlan.MedicationPlanID,
                    Dose = medicationPlan.Dose,
                    Durations = medicationPlan.Durations,
                    IntervalsID = medicationPlan.IntervalsID,
                    MedicationsID = medicationPlan.MedicationsID,
                    DirectionsID = medicationPlan.DirectionsID,
                    FacilityCode = facilityCode
                };

                var medicationAdded = unitOfWork.MedicationPlanRepository.Add(medicationPlanInDb);
                await unitOfWork.SaveChangesAsync();

                var medicationToReturn = await unitOfWork.MedicationPlanRepository.GetByIdAsync(medicationAdded.MedicationPlanID);

                return Ok(medicationToReturn);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Load Medication  of a patient in specific admission.
        /// </summary>
        /// <param name="prescriptionsId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{prescriptionsId}")]
        public async Task<IActionResult> LoadMedicationPlans(Guid prescriptionsId)
        {
            try
            {
                var medicationInDb = await unitOfWork.MedicationPlanRepository
                     .GetAll()
                    .Include(x => x.Intervals)
                    .Where(s => s.PrescriptionsID == prescriptionsId && s.IsRowDeleted.Equals(false))
                    .OrderByDescending(o => o.DateCreated)
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
        ///  Find Medication by key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{key}")]
        public async Task<IActionResult> FindMedicationPlanByKey(Guid key)
        {
            try
            {
                if (key == Guid.Empty)
                    return BadRequest("Invalid key!");

                var medicationInDb = await unitOfWork.MedicationPlanRepository.GetByIdAsync(key);

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
        /// Updates existing Medicationplan.
        /// </summary>
        /// <param name="medicationPlan"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> EditMedicationPlans([FromBody] MedicationPlanDto medicationPlan)
        {
            try
            {
                if (medicationPlan.MedicationPlanID == Guid.Empty)
                    return BadRequest();

                var medicationPlanInDb = await unitOfWork.MedicationPlanRepository.GetByIdAsync(medicationPlan.MedicationPlanID);
                if (medicationPlanInDb == null)
                    return NotFound();

                medicationPlanInDb.MedicationPlanID = medicationPlan.MedicationPlanID;
                medicationPlanInDb.Dose = medicationPlan.Dose;
                medicationPlanInDb.Durations = medicationPlan.Durations;
                medicationPlanInDb.IntervalsID = medicationPlan.IntervalsID;
                medicationPlanInDb.MedicationsID = medicationPlan.MedicationsID;
                medicationPlanInDb.DirectionsID = medicationPlan.DirectionsID;

                var medicationPlanUpdated = unitOfWork.MedicationPlanRepository.Update(medicationPlanInDb);
                await unitOfWork.SaveChangesAsync();

                var medicationToReturn = await unitOfWork.MedicationPlanRepository.GetByIdAsync(medicationPlanUpdated.MedicationPlanID);

                return Ok(medicationToReturn);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Get latest medication plan of a patient in specific admission.
        /// </summary>
        /// <param name="admissionId"></param>
        /// <param name="facilityCode"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{admissionId}")]
        public async Task<IActionResult> GetLatestMedicationPlan(Guid admissionId, [FromHeader(Name = "x-facility-code")] string facilityCode)
        {
            try
            {
                var prescriptions = await unitOfWork.PrescriptionsRepository.GetLatestPescritionList(admissionId, facilityCode);

                return Ok(prescriptions);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }
    }
}