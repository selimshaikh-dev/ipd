using IPD.Domain.Dto;
using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IPD.Api.Controllers
{
    /// <summary>
    /// ICDDiagonosisCodesController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ICDDiagonosisCodesController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<ICDDiagonosisCodesController> logger;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="logger"></param>
        public ICDDiagonosisCodesController(IUnitOfWork unitOfWork, ILogger<ICDDiagonosisCodesController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        /// <summary>
        /// Add new ICDDiagonosisCode.
        /// </summary>
        /// <param name="ICDDiagonosisCode"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddICDDiagonosisCodes([FromBody] ICDDigonosisCodeDto iCDDigonosisCode)
        {
            try
            {
                var iCDDigonosisCodeInDb = new ICDDigonosisCode
                {
                    Description = iCDDigonosisCode.Description
                };
                var iCDDigonosisCodeAdded = unitOfWork.ICDDiagonosisCodeRepository.Add(iCDDigonosisCodeInDb);
                await unitOfWork.SaveChangesAsync();

                var iCDDigonosisCodeToReturn =
                    await unitOfWork.ICDDiagonosisCodeRepository.GetByIdAsync(iCDDigonosisCodeAdded.DiseaseID);

                return Ok(iCDDigonosisCodeToReturn);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// GetAll ICDDiagonosisCode
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> LoadICDDiagonosisCodes()
        {
            try
            {
                var iCDDigonosisCodeInDb = await unitOfWork.ICDDiagonosisCodeRepository.GetAll()
                    .Where(x => x.IsRowDeleted.Equals(false))
                    .ToListAsync();

                return Ok(iCDDigonosisCodeInDb);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Find ICDDiagonosisCode By Key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{key}")]
        public async Task<IActionResult> FindICDDiagonosisCodeByKey(int key)
        {
            try
            {
                if (key == 0)
                    return BadRequest("Invalid key!");

                var iCDDigonosisCodeInDb = await unitOfWork.ICDDiagonosisCodeRepository.GetByIdAsync(key);

                if (iCDDigonosisCodeInDb == null)
                    return NotFound();

                return Ok(iCDDigonosisCodeInDb);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Updates existing ICDDiagonosisCode.
        /// </summary>
        /// <param name="ICDDiagonosisCode"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> EditICDDiagonosisCodes([FromBody] ICDDigonosisCodeDto iCDDigonosisCode)
        {
            try
            {
                if (iCDDigonosisCode.DiseaseID == 0)
                    return BadRequest();

                var iCDDigonosisCodeInDb = await unitOfWork.ICDDiagonosisCodeRepository.GetByIdAsync(iCDDigonosisCode.DiseaseID);
                if (iCDDigonosisCodeInDb == null)
                    return NotFound();

                iCDDigonosisCodeInDb.Description = iCDDigonosisCode.Description;

                var iCDDigonosisCodeUpdated = unitOfWork.ICDDiagonosisCodeRepository.Update(iCDDigonosisCodeInDb);
                await unitOfWork.SaveChangesAsync();

                var iCDDigonosisCodeToReturn =
                    await unitOfWork.ICDDiagonosisCodeRepository.GetByIdAsync(iCDDigonosisCodeUpdated.DiseaseID);

                return Ok(iCDDigonosisCodeToReturn);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }
    }
}