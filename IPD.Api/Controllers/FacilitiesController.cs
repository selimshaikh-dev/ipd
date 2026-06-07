using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IPD.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacilitiesController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<FacilitiesController> logger;

        public FacilitiesController(IUnitOfWork unitOfWork, ILogger<FacilitiesController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        [HttpGet]
        [Route("GetFacilitiesById")]
        public IActionResult GetFacilitiesById(int Facilities)
        {
            try
            {
                var GetFacilitiesByID = unitOfWork.FacilityRepository.GetById(Facilities);
                return Ok(GetFacilitiesByID);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetFacilityNameById")]
        public IActionResult GetFacilityNameById(int Facilities)
        {
            try
            {
                var facilityName = unitOfWork.FacilityRepository.GetById(Facilities)?.FacilityName;
                return Ok(facilityName);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        #region SaveOrUpdate

        [HttpPost]
        public IActionResult SaveOrUpdate([FromBody] Facility facility)
        {
            if (facility.FacilityID == 0)
            {
                var FacilityAdd = unitOfWork.FacilityRepository.Add(facility);
                unitOfWork.SaveChanges();
                return Ok(FacilityAdd);
            }
            else
            {
                var FacilityUp = unitOfWork.FacilityRepository.Update(facility);
                unitOfWork.SaveChanges();

                return Ok(FacilityUp);
            }
        }

        #endregion SaveOrUpdate

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> LoadFacilityName()
        {
            try
            {
                var facilityName = await unitOfWork.FacilityRepository
                    .GetAll()
                    .Where(x => x.IsRowDeleted.Equals(false))
                    .ToListAsync();
                return Ok(facilityName);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }
    }
}