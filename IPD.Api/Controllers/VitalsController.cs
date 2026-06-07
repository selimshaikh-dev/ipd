using IPD.Domain.Dto;
using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace IPD.Api.Controllers
{
    /// <summary>
    /// VitalsController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class VitalsController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<VitalsController> logger;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="unitOfWork">IUnitOfWork</param>
        /// <param name="logger">ILogger</param>
        public VitalsController(IUnitOfWork unitOfWork, ILogger<VitalsController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        /// <summary>
        /// Saves patient's vitals.
        /// </summary>
        /// <param name="vital">Vital</param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddVitals([FromHeader(Name = "x-facility-code")] string facilityCode, [FromBody] VitalDto vital)
        {
            try
            {
                var vitalsInDb = new Vital
                {
                    VitalID = vital.VitalID,
                    DateCollected = vital.DateCollected,
                    TimeCollected = vital.TimeCollected,
                    Weight = vital.Weight,
                    Height = vital.Height,
                    Temperature = vital.Temperature,
                    MUAC = vital.MUAC,
                    Systolic = vital.Systolic,
                    Diastolic = vital.Diastolic,
                    RespiratoryRate = vital.RespiratoryRate,
                    Pulse = vital.Pulse,
                    OxygenSaturation = vital.OxygenSaturation,
                    BMI = vital.BMI,
                    NutritionalStatus = vital.NutritionalStatus,
                    OtherVitals = vital.OtherVitals,
                    FacilityCode = facilityCode,
                    AdmissionID = vital.AdmissionID,
                    DateCreated = DateTime.Now
                };

                var vitalAdded = unitOfWork.VitalRepository.Add(vitalsInDb);
                await unitOfWork.SaveChangesAsync();

                var vitalToReturn = await unitOfWork.VitalRepository.GetByIdAsync(vitalAdded.VitalID);

                return Ok(vitalToReturn);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Load vitals of a patient in specific admission.
        /// </summary>
        /// <param name="admissionId">Guid</param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{admissionId}")]
        public async Task<IActionResult> LoadVitals(Guid admissionId)
        {
            try
            {
                var vitalsInDb = await unitOfWork.VitalRepository
                .GetAllLoadVital(admissionId);
                
                return Ok(vitalsInDb);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Finds patient's vital information from Vitals  table using primary key.
        /// </summary>
        /// <param name="key">Guid</param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{key}")]
        public async Task<IActionResult> FindVitalByKey(Guid key)
        {
            try
            {
                if (key == Guid.Empty)
                    return BadRequest("Invalid key!");

                var vitalsInDb = await unitOfWork.VitalRepository.GetByIdAsync(key);

                if (vitalsInDb == null)
                    return NotFound();

                return Ok(vitalsInDb);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Updates existing Vital.
        /// </summary>
        /// <param name="vital">Vital</param>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> EditVitals([FromBody] VitalDto vital)
        {
            try
            {
                if (vital.VitalID == Guid.Empty)
                    return BadRequest();

                var vitalsInDb = await unitOfWork.VitalRepository.GetByIdAsync(vital.VitalID);

                if (vitalsInDb == null)
                    return NotFound();

                vitalsInDb.VitalID = vital.VitalID;
                vitalsInDb.DateCollected = vital.DateCollected;
                vitalsInDb.TimeCollected = vital.TimeCollected;
                vitalsInDb.Weight = vital.Weight;
                vitalsInDb.Height = vital.Height;
                vitalsInDb.Temperature = vital.Temperature;
                vitalsInDb.MUAC = vital.MUAC;
                vitalsInDb.Systolic = vital.Systolic;
                vitalsInDb.Diastolic = vital.Diastolic;
                vitalsInDb.RespiratoryRate = vital.RespiratoryRate;
                vitalsInDb.Pulse = vital.Pulse;
                vitalsInDb.OxygenSaturation = vital.OxygenSaturation;
                vitalsInDb.BMI = vital.BMI;
                vitalsInDb.NutritionalStatus = vital.NutritionalStatus;
                vitalsInDb.OtherVitals = vital.OtherVitals;
                vitalsInDb.FacilityCode = vital.FacilityCode;
                vitalsInDb.AdmissionID = vital.AdmissionID;

                var updatedVitals = unitOfWork.VitalRepository.Update(vitalsInDb);
                await unitOfWork.SaveChangesAsync();

                var vitalToReturn = await unitOfWork.VitalRepository.GetByIdAsync(updatedVitals.VitalID);

                return Ok(vitalToReturn);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }
    }
}