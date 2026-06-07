using IPD.Domain.Dto;
using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IPD.Api.Controllers
{
    /// <summary>
    /// PostSurgeriesController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PostSurgeriesController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<PostSurgeriesController> logger;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="unitOfWork">IUnitOfWork</param>
        /// <param name="logger">ILogger</param>
        public PostSurgeriesController(IUnitOfWork unitOfWork, ILogger<PostSurgeriesController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        /// <summary>
        /// Add PostSurgery.
        /// </summary>
        /// <param name="postSurgery"></param>
        /// <returns></returns>
        [HttpPost]
        //[Route("AddPostSurgeries")]
        [Route("[action]")]
        public async Task<IActionResult> AddPostSurgeries([FromHeader(Name = "x-facility-code")] string facilityCode, [FromBody] PostSurgeriesDto postSurgery)
        {
            try
            {
                var postSurgeriesInDb = new PostSurgery
                {
                    SurgeryID = postSurgery.SurgeryID,
                    Findings = postSurgery.Findings,
                    PatientsCondition = postSurgery.PatientsCondition,
                    PostSurgeryPlan = postSurgery.PostSurgeryPlan,
                    SurgeryDetails = postSurgery.SurgeryDetails,
                    FacilityCode = facilityCode
                };

                var postSurgeryAdded = unitOfWork.PostSurgeriesReposity.Add(postSurgeriesInDb);
                await unitOfWork.SaveChangesAsync();

                var postSurgeryToReturn = await unitOfWork.PostSurgeriesReposity.GetByIdAsync(postSurgeryAdded.PostSurgeryID);

                return Ok(postSurgeryToReturn);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        ///  Load Surgery  of a patient in specific surgery.
        /// </summary>
        /// <param name="surgeryId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{surgeryId}")]
        public async Task<IActionResult> LoadPostSurgeries(Guid surgeryId)
        {
            try
            {
                var postSurgeriesInDb = await unitOfWork.PostSurgeriesReposity
                    .GetAll()
                    .Where(p => p.SurgeryID == surgeryId && p.IsRowDeleted.Equals(false))
                    .OrderByDescending(o => o.DateCreated)
                    .ToListAsync();

                return Ok(postSurgeriesInDb);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        ///  Find Surgery post by key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{key}")]
        public async Task<IActionResult> FindPostSurgeryByKey(Guid key)
        {
            try
            {
                if (key == Guid.Empty)
                    return BadRequest("Invalid key!");

                var postSurgeriesInDb = await unitOfWork.PostSurgeriesReposity.GetByIdAsync(key);

                if (postSurgeriesInDb == null)
                    return NotFound();

                return Ok(postSurgeriesInDb);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Updates existing postSurgeries.
        /// </summary>
        /// <param name="postSurgery"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> EditPostSurgeries([FromBody] PostSurgeriesDto postSurgery)
        {
            try
            {
                if (postSurgery.PostSurgeryID == Guid.Empty)
                    return BadRequest();

                var postSurgeryInDb = await unitOfWork.PostSurgeriesReposity.GetByIdAsync(postSurgery.PostSurgeryID);
                if (postSurgeryInDb == null)
                    return NotFound();

                postSurgeryInDb.SurgeryDetails = postSurgery.SurgeryDetails;
                postSurgeryInDb.Findings = postSurgery.Findings;
                postSurgeryInDb.PostSurgeryPlan = postSurgery.PostSurgeryPlan;
                postSurgeryInDb.PatientsCondition = postSurgery.PatientsCondition;
                postSurgeryInDb.SurgeryID = postSurgery.SurgeryID;

                var updatedPostSurgery = unitOfWork.PostSurgeriesReposity.Update(postSurgeryInDb);
                await unitOfWork.SaveChangesAsync();

                var postSurgeryToReturn = await unitOfWork.PostSurgeriesReposity.GetByIdAsync(updatedPostSurgery.PostSurgeryID);

                return Ok(postSurgeryToReturn);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }
    }
}