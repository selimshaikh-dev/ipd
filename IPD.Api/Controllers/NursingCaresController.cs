using IPD.Domain.Dto;
using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IPD.Api.Controllers
{
    /// <summary>
    /// NursingCaresController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class NursingCaresController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<NursingCaresController> logger;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="unitOfWork">IUnitOfWork</param>
        /// <param name="logger">ILogger</param>
        public NursingCaresController(IUnitOfWork unitOfWork, ILogger<NursingCaresController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        /// <summary>
        /// Saves a Nursing Care info.
        /// </summary>
        /// <param name="nursingCare">NursingCare</param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddNursingCare([FromHeader(Name = "x-facility-code")] string facilityCode, [FromBody] NursingCaresDto nursingCare)
        {
            try
            {
                var nursingCareInDb = new NursingCare
                {
                    NursingCareID = nursingCare.NursingCareID,
                    DateOfCare = nursingCare.DateOfCare,
                    TimeOfCare = nursingCare.TimeOfCare,
                    Problem = nursingCare.Problem,
                    Diagnosis = nursingCare.Diagnosis,
                    Objective = nursingCare.Objective,
                    Intervension = nursingCare.Intervension,
                    Rational = nursingCare.Rational,
                    Evaluation = nursingCare.Evaluation,
                    AdmissionID = nursingCare.AdmissionID,
                    DateCreated = nursingCare.DateCreated,
                    FacilityCode = facilityCode,
                };

                var nursingCareAdded = unitOfWork.NursingCareRepository.Add(nursingCareInDb);
                await unitOfWork.SaveChangesAsync();

                var nursingCareToReturn = await unitOfWork.NursingCareRepository.GetByIdAsync(nursingCareAdded.NursingCareID);

                return Ok(nursingCareToReturn);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Load NursingCares  of a patient in specific admission.
        /// </summary>
        /// <param name="admissionId">Guid</param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{admissionId}")]
        public async Task<IActionResult> LoadNursingCares(Guid admissionId)
        {
            try
            {
                var nursingCareInDb = await unitOfWork.NursingCareRepository
                    .GetAll()
                    .Where(x => x.AdmissionID == admissionId && x.IsRowDeleted.Equals(false))
                    .OrderByDescending(o => o.DateCreated)
                    .ToListAsync();

                return Ok(nursingCareInDb);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Finds  a NursingCare info  from   NursingCares  table using primary key.
        /// </summary>
        /// <param name="key">Guid</param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{key}")]
        public async Task<IActionResult> FindNursingCareByKey(Guid key)
        {
            try
            {
                if (key == Guid.Empty)
                    return BadRequest("Invalid key!");

                var nursingCaresInDb = await unitOfWork.NursingCareRepository.GetByIdAsync(key);

                if (nursingCaresInDb == null)
                    return NotFound();

                return Ok(nursingCaresInDb);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Updates existing NursingCare.
        /// </summary>
        /// <param name="nursingCare">NursingCare</param>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> EditNursingCare([FromBody] NursingCaresDto nursingCare)
        {
            try
            {
                if (nursingCare.NursingCareID == Guid.Empty)
                    return BadRequest();

                var nursingCareInDb = await unitOfWork.NursingCareRepository.GetByIdAsync(nursingCare.NursingCareID);

                if (nursingCareInDb == null)
                    return NotFound();

                nursingCareInDb.NursingCareID = nursingCare.NursingCareID;
                nursingCareInDb.DateOfCare = nursingCare.DateOfCare;
                nursingCareInDb.TimeOfCare = nursingCare.TimeOfCare;
                nursingCareInDb.Problem = nursingCare.Problem;
                nursingCareInDb.Diagnosis = nursingCare.Diagnosis;
                nursingCareInDb.Objective = nursingCare.Objective;
                nursingCareInDb.Intervension = nursingCare.Intervension;
                nursingCareInDb.Rational = nursingCare.Rational;
                nursingCareInDb.Evaluation = nursingCare.Evaluation;
                nursingCareInDb.FacilityCode = nursingCare.FacilityCode;
                nursingCareInDb.AdmissionID = nursingCare.AdmissionID;
                nursingCareInDb.DateCreated = nursingCare.DateCreated;

                var UpdatedNursingCare = unitOfWork.NursingCareRepository.Update(nursingCareInDb);
                await unitOfWork.SaveChangesAsync();

                var NCToReturn = await unitOfWork.NursingCareRepository.GetByIdAsync(UpdatedNursingCare.NursingCareID);

                return Ok(NCToReturn);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }
    }
}