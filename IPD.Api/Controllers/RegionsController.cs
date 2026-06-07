using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IPD.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<RegionsController> logger;

        public RegionsController(IUnitOfWork unitOfWork, ILogger<RegionsController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        [HttpGet]
        [Route("GetTinkhundlaById")]
        public IActionResult GetRegionById(int resignId)
        {
            try
            {
                var GetRegionById = unitOfWork.RegionRepository.GetById(resignId);
                return Ok(GetRegionById);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        #region SaveOrUpdate

        [HttpPost]
        public IActionResult SaveOrUpdate([FromBody] Region region)
        {
            if (region.RegionID == 0)
            {
                var RegionAdd = unitOfWork.RegionRepository.Add(region);
                unitOfWork.SaveChanges();
                return Ok(RegionAdd);
            }
            else
            {
                var RegionUp = unitOfWork.RegionRepository.Update(region);
                unitOfWork.SaveChanges();

                return Ok(RegionUp);
            }
        }

        #endregion SaveOrUpdate

        /// <summary>
        /// GetAll Region
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> LoadRegions()
        {
            try
            {
                var regionInDb = await unitOfWork.RegionRepository.GetAll()
                    .Where(x => x.IsRowDeleted.Equals(false))
                    .ToListAsync();

                return Ok(regionInDb);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }
    }
}