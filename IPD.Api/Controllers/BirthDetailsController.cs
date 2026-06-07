using IPD.Domain.Dto;
using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IPD.Api.Controllers
{
    /// <summary>
    /// BirthDetails Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BirthDetailsController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<BirthDetailsController> logger;

        /// <summary>
        ///  BirthDetails constructor
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="logger"></param>
        public BirthDetailsController(IUnitOfWork unitOfWork, ILogger<BirthDetailsController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        /// <summary>
        /// Add new BirthDetails.
        /// </summary>
        /// <param name="birthDetails"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddBirthDetails([FromBody] BirthDetailsDto birthDetails)
        {
            try
            {
                var birthDetailsInDb = new BirthDetail
                {
                    AdmissionID = birthDetails.AdmissionID,
                    IsSuccessfulDelivery = birthDetails.IsSuccessfulDelivery,
                    Remarks = birthDetails.Remarks,
                    Gender = birthDetails.Gender,
                    Weight = birthDetails.Weight,
                    TypeOfDelivery = birthDetails.TypeOfDelivery,
                    BirthDate = birthDetails.BirthDate,
                    BirthTime = birthDetails.BirthTime,
                   
                };

                var birthDetailAdded = unitOfWork.BirthDetailsRepository.Add(birthDetailsInDb);
                await unitOfWork.SaveChangesAsync();

                var birthDetailToReturn = await unitOfWork.BirthDetailsRepository.GetByIdAsync(birthDetailAdded.BirthDetailsID);

                return Ok(birthDetailToReturn);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Load birthDetails in specific admission.
        /// </summary>
        /// <param name="admissionId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{admissionId}")]
        public async Task<IActionResult> LoadBirthDetails(Guid admissionId)
        {
            try
            {
                var birthDetailsInDb = await unitOfWork.BirthDetailsRepository
                    .GetAll()
                    .Where(p => p.AdmissionID == admissionId && p.IsRowDeleted.Equals(false))
                    .OrderByDescending(o => o.DateCreated)
                    .ToListAsync();

                return Ok(birthDetailsInDb);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }


        /// <summary>
        /// Load birthDetails in specific admission.
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{patientId}")]
        public async Task<IActionResult> LoadBirthDetailsByPatientId(Guid patientId)
        {
            try
            {
                var birthDetailsInDb = await unitOfWork.BirthDetailsRepository.GetByPatientId(patientId);

                return Ok(birthDetailsInDb);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }


        [HttpGet]
        [Route("[action]/{admissionID}")]
        public async Task<IActionResult> LoadBirthDetailsByadmissionID(Guid admissionID)
        {
            try
            {
                var birthDetailsInDb = await unitOfWork.BirthDetailsRepository.GetByAdmissionID(admissionID);

                return Ok(birthDetailsInDb);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }


        /// <summary>
        /// Find Birth Details By Key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{key}")]
        public async Task<IActionResult> FindBirthDetailByKey(Guid key)
        {
            try
            {
                if (key == Guid.Empty)
                    return BadRequest("Invalid key!");

                var birthDetailsInDb = await unitOfWork.BirthDetailsRepository.GetByIdAsync(key);

                if (birthDetailsInDb == null)
                    return NotFound();

                return Ok(birthDetailsInDb);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Updates existing birthDetails.
        /// </summary>
        /// <param name="birthDetails"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> EditBirthDetails([FromBody] BirthDetailsDto birthDetails)
        {
            try
            {
                if (birthDetails.BirthDetailsID == Guid.Empty)
                    return BadRequest();

                var birthDetailsInDb = await unitOfWork.BirthDetailsRepository.GetByIdAsync(birthDetails.BirthDetailsID);

                if (birthDetailsInDb == null)
                    return NotFound();

                birthDetailsInDb.BirthDetailsID = birthDetails.BirthDetailsID;
                birthDetailsInDb.AdmissionID = birthDetails.AdmissionID;
                birthDetailsInDb.IsSuccessfulDelivery = birthDetails.IsSuccessfulDelivery;
                birthDetailsInDb.Remarks = birthDetails.Remarks;
                birthDetailsInDb.Gender = birthDetails.Gender;
                birthDetailsInDb.Weight = birthDetails.Weight;
                birthDetailsInDb.TypeOfDelivery = birthDetails.TypeOfDelivery;
                birthDetailsInDb.BirthDate = birthDetails.BirthDate;
                birthDetailsInDb.BirthTime = birthDetails.BirthTime;

                var updatedBirthDetail = unitOfWork.BirthDetailsRepository.Update(birthDetailsInDb);
                await unitOfWork.SaveChangesAsync();

                var birthDetailsInDbToReturn = await unitOfWork.BirthDetailsRepository.GetByIdAsync(updatedBirthDetail.BirthDetailsID);

                return Ok(birthDetailsInDbToReturn);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }
    }
}