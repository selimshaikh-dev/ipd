using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IPD.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<CountriesController> logger;

        public CountriesController(IUnitOfWork unitOfWork, ILogger<CountriesController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        [HttpGet]
        [Route("GetCountryById")]
        public IActionResult GetCountryById(int CountryID)
        {
            try
            {
                var GetCountryByID = unitOfWork.CountriesRepository.GetById(CountryID);
                return Ok(GetCountryByID);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        #region SaveOrUpdate

        [HttpPost]
        public IActionResult SaveOrUpdate([FromBody] Country country)
        {
            if (country.CountryID == 0)
            {
                var CountryAdd = unitOfWork.CountriesRepository.Add(country);
                unitOfWork.SaveChanges();
                return Ok(CountryAdd);
            }
            else
            {
                var CountryUp = unitOfWork.CountriesRepository.Update(country);
                unitOfWork.SaveChanges();

                return Ok(CountryUp);
            }
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> LoadCountryName()
        {
            try
            {
                var CountryName = await unitOfWork.CountriesRepository
                    .GetAll()
                    .Where(x => x.IsRowDeleted.Equals(false))
                    .ToListAsync();
                return Ok(CountryName);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }
        #endregion SaveOrUpdate
    }
}