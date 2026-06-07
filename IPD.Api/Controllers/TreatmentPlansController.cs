using IPD.Domain.Dto;
using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IPD.Api.Controllers
{
    /// <summary>
    /// TreatmentPlansController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TreatmentPlansController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<TreatmentPlansController> logger;

        /// <summary>
        ///  Default constructor.
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="logger"></param>
        public TreatmentPlansController(IUnitOfWork unitOfWork, ILogger<TreatmentPlansController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        /// <summary>
        /// Add TreatmentPlans.
        /// </summary>
        /// <param name="treatmentPlan"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddTreatmentPlans([FromHeader(Name = "x-facility-code")] string facilityCode, [FromBody] TreatmentPlanDto treatmentPlan)
        {
            try
            {
                var treatmentPlanInDb = new TreatmentPlan
                {
                    AdmissionID = treatmentPlan.AdmissionID,
                    TreatementPlanDetails = treatmentPlan.TreatementPlanDetails,
                    FacilityCode = facilityCode,
                };

                var treatmentPlanAdded = unitOfWork.TreatmentPlansRepository.Add(treatmentPlanInDb);
                await unitOfWork.SaveChangesAsync();

                return Ok(treatmentPlanAdded);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Load Admission  of a patient in specific admission.
        /// </summary>
        /// <param name="admissionId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{admissionId}")]
        public async Task<IActionResult> LoadTreatmentPlans(Guid admissionId)
        {
            try
            {
                var treatmentPlanInDb = await unitOfWork.TreatmentPlansRepository
                     .GetAllTreatmentPlan(admissionId);

                return Ok(treatmentPlanInDb);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        ///  Find TreatmentPlan by key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{key}")]
        public async Task<IActionResult> FindTreatmentPlanByKey(Guid key)
        {
            try
            {
                if (key == Guid.Empty)
                    return BadRequest("Invalid key!");

                var treatmentPlanInDb = await unitOfWork.TreatmentPlansRepository.GetByIdAsync(key);

                if (treatmentPlanInDb == null)
                    return NotFound();

                return Ok(treatmentPlanInDb);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Updates existing treatmentPlan.
        /// </summary>
        /// <param name="treatmentPlan"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> EdittreatmentPlans([FromBody] TreatmentPlanDto treatmentPlan)
        {
            try
            {
                if (treatmentPlan.TreatmentPlanID == Guid.Empty)
                    return BadRequest();

                var treatmentPlanInDb = await unitOfWork.TreatmentPlansRepository.GetByIdAsync(treatmentPlan.TreatmentPlanID);
                if (treatmentPlanInDb == null)
                    return NotFound();

                treatmentPlanInDb.AdmissionID = treatmentPlan.AdmissionID;
                treatmentPlanInDb.TreatementPlanDetails = treatmentPlan.TreatementPlanDetails;

                var treatmentPlanUpdated = unitOfWork.TreatmentPlansRepository.Update(treatmentPlanInDb);
                await unitOfWork.SaveChangesAsync();

                return Ok(treatmentPlanUpdated);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }
    }
}