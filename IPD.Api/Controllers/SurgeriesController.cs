using IPD.Domain.Dto;
using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IPD.Api.Controllers
{
    /// <summary>
    /// Surgeries Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SurgeriesController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<SurgeriesController> logger;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="unitOfWork">IUnitOfWork</param>
        /// <param name="logger">ILogger</param>
        public SurgeriesController(IUnitOfWork unitOfWork, ILogger<SurgeriesController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        /// <summary>
        /// Add Surgery.
        /// </summary>
        /// <param name="surgery"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddSurgeries([FromHeader(Name = "x-facility-code")] string facilityCode, [FromBody] SurgeriesDto surgery)
        {
            try
            {
                var surgeriesInDb = new Surgery
                {
                    AdmissionID = surgery.AdmissionID,
                    Diagnosis = surgery.Diagnosis,
                    AnaesthetistAssessment = surgery.AnaesthetistAssessment,
                    HasPatientsConcent = surgery.HasPatientsConcent,
                    OtherSurgeryType = surgery.OtherSurgeryType,
                    ProcedureIndication = surgery.ProcedureIndication,
                    SurgeryTeam = surgery.SurgeryTeam,
                    SurgeryTypeID = surgery.SurgeryTypeID,
                    SurgicalProcedureID = surgery.SurgicalProcedureID,
                    SurgeryDate = surgery.SurgeryDate,
                    SurgeryTime = surgery.SurgeryTime,
                    FacilityCode = facilityCode,
                };

                var surgeryAdded = unitOfWork.SurgeriesRepository.Add(surgeriesInDb);
                await unitOfWork.SaveChangesAsync();

                var surgeryToReturn = await unitOfWork.SurgeriesRepository.GetByIdAsync(surgeryAdded.SurgeryID);

                return Ok(surgeryToReturn);
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
        public async Task<IActionResult> LoadSurgeries(Guid admissionId)
        {
            try
            {
                var surgeriesInDb = await unitOfWork.SurgeriesRepository
                    .GetAll()
                    .Include(x => x.PostSurgeries)
                    .Where(s => s.AdmissionID == admissionId && s.IsRowDeleted.Equals(false))
                    .OrderByDescending(o => o.DateCreated)
                    .ToListAsync();

                return Ok(surgeriesInDb);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        ///  Find Surgery by key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{key}")]
        public async Task<IActionResult> FindSurgeryByKey(Guid key)
        {
            try
            {
                if (key == Guid.Empty)
                    return BadRequest("Invalid key!");

                var surgeriesInDb = await unitOfWork.SurgeriesRepository.GetByIdAsync(key);

                if (surgeriesInDb == null)
                    return NotFound();

                return Ok(surgeriesInDb);
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
        /// <param name="surgery"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> EditSurgeries([FromBody] SurgeriesDto surgery)
        {
            try
            {
                if (surgery.SurgeryID == Guid.Empty)
                    return BadRequest();

                var surgeriesInDb = await unitOfWork.SurgeriesRepository.GetByIdAsync(surgery.SurgeryID);
                if (surgeriesInDb == null)
                    return NotFound();

                surgeriesInDb.SurgeryDate = surgery.SurgeryDate;
                surgeriesInDb.SurgeryTime = surgery.SurgeryTime;
                surgeriesInDb.HasPatientsConcent = surgery.HasPatientsConcent;
                surgeriesInDb.Diagnosis = surgery.Diagnosis;
                surgeriesInDb.AnaesthetistAssessment = surgery.AnaesthetistAssessment;
                surgeriesInDb.OtherSurgeryType = surgery.OtherSurgeryType;
                surgeriesInDb.SurgeryTeam = surgery.SurgeryTeam;
                surgeriesInDb.ProcedureIndication = surgery.ProcedureIndication;
                surgeriesInDb.SurgeryTypeID = surgery.SurgeryTypeID;
                surgeriesInDb.SurgicalProcedureID = surgery.SurgicalProcedureID;
                surgeriesInDb.AdmissionID = surgery.AdmissionID;

                var surgeryUpdated = unitOfWork.SurgeriesRepository.Update(surgeriesInDb);
                await unitOfWork.SaveChangesAsync();

                var surgeryToReturn = await unitOfWork.SurgeriesRepository.GetByIdAsync(surgeryUpdated.SurgeryID);

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