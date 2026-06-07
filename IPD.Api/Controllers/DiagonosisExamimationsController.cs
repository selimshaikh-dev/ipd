using IPD.Domain.Dto;
using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IPD.Api.Controllers
{
    /// <summary>
    /// DiagonosisExamimationsController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DiagonosisExamimationsController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<DiagonosisExamimationsController> logger;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="logger"></param>
        public DiagonosisExamimationsController(IUnitOfWork unitOfWork, ILogger<DiagonosisExamimationsController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        /// <summary>
        /// Add new DiagonosisExamination.
        /// </summary>
        /// <param name="diagonosisExamination"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddDiagonosisExaminations([FromBody] DiagonosisExaminationDto diagonosisExamination)
        {
            try
            {
                var diagonosisExaminationInDb = new DiagnosisExamination
                {
                    DigonosisExaminationsName = diagonosisExamination.DigonosisExaminationsName
                };
                var diagonosisExaminationAdded = unitOfWork.DiagonosisExamimationsRepository.Add(diagonosisExaminationInDb);
                await unitOfWork.SaveChangesAsync();

                var diagonosisExaminationToReturn =
                    await unitOfWork.DiagonosisExamimationsRepository.GetByIdAsync(diagonosisExaminationAdded.DigonosisExaminationID);

                return Ok(diagonosisExaminationToReturn);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// GetAll DiagonosisExamination
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> LoadDiagonosisExaminations()
        {
            try
            {
                var diagonosisexaminationindb = await unitOfWork.DiagonosisExamimationsRepository
                 .GetAll()
                 .Where(x => x.IsRowDeleted.Equals(false))
                 .ToListAsync();
                return Ok(diagonosisexaminationindb);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Find DiagonosisExamination By Key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{key}")]
        public async Task<IActionResult> FindSurgeryTypeByKey(int key)
        {
            try
            {
                var diagonosisExaminationInDb = await unitOfWork.DiagonosisExamimationsRepository.GetByIdAsync(key);

                if (diagonosisExaminationInDb == null)
                    return NotFound();

                return Ok(diagonosisExaminationInDb);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Updates existing DiagonosisExamination.
        /// </summary>
        /// <param name="diagonosisExamination"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> EditDiagonosisExaminations([FromBody] DiagonosisExaminationDto diagonosisExamination)
        {
            try
            {
                if (diagonosisExamination.DigonosisExaminationID == 0)
                    return BadRequest();

                var diagonosisExaminationInDb = await unitOfWork.DiagonosisExamimationsRepository.GetByIdAsync(diagonosisExamination.DigonosisExaminationID);
                if (diagonosisExaminationInDb == null)
                    return NotFound();

                diagonosisExaminationInDb.DigonosisExaminationsName = diagonosisExamination.DigonosisExaminationsName;

                var diagonosisExaminationUpdated = unitOfWork.DiagonosisExamimationsRepository.Update(diagonosisExaminationInDb);
                await unitOfWork.SaveChangesAsync();

                var diagonosisExaminationToReturn =
                    await unitOfWork.DiagonosisExamimationsRepository.GetByIdAsync(diagonosisExaminationUpdated.DigonosisExaminationID);

                return Ok(diagonosisExaminationToReturn);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }
    }
}