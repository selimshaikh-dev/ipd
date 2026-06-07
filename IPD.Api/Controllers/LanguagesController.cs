using IPD.Domain.Dto;
using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IPD.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguagesController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<LanguagesController> logger;

        public LanguagesController(IUnitOfWork unitOfWork, ILogger<LanguagesController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        /// <summary>
        /// Add new Language.
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddLanguage([FromBody] LanguageDto language)
        {
            try
            {
                var languageInDb = new Language
                {
                    LanguageName = language.LanguageName
                };
                var languageAdded = unitOfWork.LanguageRepository.Add(languageInDb);
                await unitOfWork.SaveChangesAsync();

                var languageToReturn =
                    await unitOfWork.LanguageRepository.GetByIdAsync(languageAdded.LanguageID);

                return Ok(languageToReturn);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// GetAll Procedure
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> LoadLanguages()
        {
            try
            {
                var languageInDb = await unitOfWork.LanguageRepository.GetAll()
                    .Where(x => x.IsRowDeleted.Equals(false))
                    .ToListAsync();

                return Ok(languageInDb);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Find language By Key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{key}")]
        public async Task<IActionResult> FindlanguageByKey(int key)
        {
            try
            {
                if (key == 0)
                    return BadRequest("Invalid key!");

                var languageInDb = await unitOfWork.LanguageRepository.GetByIdAsync(key);

                if (languageInDb == null)
                    return NotFound();

                return Ok(languageInDb);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Updates existing Language.
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> EditLanguageInDb([FromBody] LanguageDto language)
        {
            try
            {
                if (language.LanguageID == 0)
                    return BadRequest();

                var languageInDb = await unitOfWork.LanguageRepository.GetByIdAsync(language.LanguageID);
                if (languageInDb == null)
                    return NotFound();

                languageInDb.LanguageName = language.LanguageName;

                var languageUpdated = unitOfWork.LanguageRepository.Update(languageInDb);
                await unitOfWork.SaveChangesAsync();

                var surgeryToReturn =
                    await unitOfWork.LanguageRepository.GetByIdAsync(languageUpdated.LanguageID);

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