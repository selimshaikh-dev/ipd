using IPD.Domain.Dto;
using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IPD.Api.Controllers
{
    /// <summary>
    /// Controller for discharge
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DischargeController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<DischargeController> logger;

        /// <summary>
        /// constructor for Discharge Controller
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="logger"></param>
        public DischargeController(IUnitOfWork unitOfWork, ILogger<DischargeController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        /// <summary>
        /// Get all discharges
        /// </summary>
        /// <param name="admissionId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{admissionId}")]
        public async Task<IActionResult> LoadDischarges(Guid admissionId)
        {
            try
            {
                var dischargesInDb = await unitOfWork.DischargeRepository
                    .GetAll()
                    .Where(x => x.AdmissionID == admissionId && x.IsRowDeleted.Equals(false))
                    .ToListAsync();

                return Ok(dischargesInDb);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// get discharge by key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{key}")]
        public async Task<IActionResult> FindDischargeByKey(Guid key)
        {
            try
            {
                if (key == Guid.Empty)
                {
                    return BadRequest("Invalid key!");
                }

                var dischargeInDb = await unitOfWork.DischargeRepository.GetByIdAsync(key);
                if (dischargeInDb == null)
                {
                    return NotFound();
                }

                return Ok(dischargeInDb);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Add new discharge
        /// </summary>
        /// <param name="discharge"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddDischarges([FromHeader(Name = "x-facility-code")] string facilityCode, [FromBody] DischargesDto discharge)
        {
            try
            {
                var dischargesInDb = new Discharge
                {
                    DischargeID = discharge.DischargeID,
                    DischargeDate = discharge.DischargeDate,
                    DischargeTime = discharge.DischargeTime,
                    Advice = discharge.Advice,
                    DietNutritionAdvice = discharge.DietNutritionAdvice,
                    MedicationAdvice = discharge.MedicationAdvice,
                    Remarks = discharge.Remarks,
                    FinalDiagnosis = discharge.FinalDiagnosis,
                    DischargeStatusID = discharge.DischargeStatusID,
                    AdmissionID = discharge.AdmissionID,
                    FacilityCode = facilityCode,
                };

                var dischargesAdded = unitOfWork.DischargeRepository.Add(dischargesInDb);
                await unitOfWork.SaveChangesAsync();

                var dischargesToReturn = await unitOfWork.DischargeRepository.GetByIdAsync(dischargesAdded.DischargeID);

                return Ok(dischargesToReturn);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Update discharge
        /// </summary>
        /// <param name="discharges"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> EditDischarges([FromBody] DischargesDto discharges)
        {
            try
            {
                if (discharges.DischargeID == Guid.Empty)
                {
                    return BadRequest();
                }

                var dischargeInDb = await unitOfWork.DischargeRepository.GetByIdAsync(discharges.DischargeID);
                if (dischargeInDb == null)
                {
                    return NotFound();
                }

                dischargeInDb.DischargeDate = discharges.DischargeDate;
                dischargeInDb.DischargeTime = discharges.DischargeTime;
                dischargeInDb.Advice = discharges.Advice;
                dischargeInDb.DietNutritionAdvice = discharges.DietNutritionAdvice;
                dischargeInDb.MedicationAdvice = discharges.MedicationAdvice;
                dischargeInDb.Remarks = discharges.Remarks;
                dischargeInDb.FinalDiagnosis = discharges.FinalDiagnosis;
                dischargeInDb.DischargeStatusID = discharges.DischargeStatusID;
                dischargeInDb.AdmissionID = discharges.AdmissionID;

                var dischargesUpdated = unitOfWork.DischargeRepository.Update(dischargeInDb);
                await unitOfWork.SaveChangesAsync();

                var dischargesToReturn = await unitOfWork.DischargeRepository.GetByIdAsync(dischargesUpdated.DischargeID);

                return Ok(dischargesToReturn);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }
    }
}