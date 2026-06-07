using IPD.Domain.Dto;
using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace IPD.Api.Controllers
{
    /// <summary>
    /// Volumes Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class VolumesController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<VolumesController> logger;

        /// <summary>
        ///Default constructor
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="logger"></param>
        public VolumesController(IUnitOfWork unitOfWork, ILogger<VolumesController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        /// <summary>
        /// Add new Volumes
        /// </summary>
        /// <param name="volumes"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddVolumes([FromBody] VolumeCreateDto volumes)
        {
            try
            {
                var volumesList = volumes.Data?.Select(x => new Volume()
                {
                    VolumesDetails = Convert.ToString(x[1]),
                    VolumesTime = Convert.ToInt64(x[0]),
                    PartographID = volumes.PartographID,
                }).ToList() ?? new List<Volume>();

                foreach (var item in volumesList)
                {
                    unitOfWork.VolumesRepository.UpdateVolume(item);
                }

                await unitOfWork.SaveChangesAsync();

                return Ok(volumesList);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Load Volumes of a patient in specific admission.
        /// </summary>
        /// <param name="partographId"></param>
        /// <returns></returns>
        //[HttpGet]
        //[Route("[action]/{partographId}")]
        //public async Task<IActionResult> LoadVolumes(Guid partographId)
        //{
        //    try
        //    {
        //        var volumeInDb = await unitOfWork.VolumesRepository
        //            .GetAll()
        //            .Where(l => l.PartographID == partographId && l.IsRowDeleted.Equals(false))
        //            .OrderByDescending(o => o.DateCreated)
        //            .ToListAsync();

        //        var data = volumeInDb.Select(x => new long[]
        //        {
        //                x.VolumesTime,
        //                x.VolumesDetails
        //        })
        //        .OrderBy(i => i[0])
        //        .ToList();
        //        var volumes = new VolumeCreateDto();
        //        if (data.Count > 0)
        //        {
        //            volumes = new VolumeCreateDto()
        //            {
        //                PartographID = partographId,
        //                Data = data
        //            };
        //        }

        //        return Ok(volumes);
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogError(ex.Message);
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
        //    }
        //}

        /// <summary>
        /// Finds patient's volumes information from Volumes table using primary key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        //[HttpGet]
        //[Route("[action]/{key}")]
        //public async Task<IActionResult> FindVolumesByKey(Guid key)
        //{
        //    try
        //    {
        //        if (key == Guid.Empty)
        //            return BadRequest("Invalid key!");

        //        var volumesInDb = await unitOfWork.VolumesRepository.GetByIdAsync(key);

        //        if (volumesInDb == null)
        //            return NotFound();

        //        return Ok(volumesInDb);
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogError(ex.Message);
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
        //    }
        //}

        /// <summary>
        /// Updates existing Volumes.
        /// </summary>
        /// <param name="volumes"></param>
        /// <returns></returns>
        //[HttpPut]
        //[Route("[action]")]
        //public async Task<IActionResult> EditVolumes([FromBody] VolumeCreateDto volumes)
        //{
        //    try
        //    {
        //        if (volumes.VolumesID == Guid.Empty)
        //            return BadRequest();

        //        var volumesInDb = await unitOfWork.VolumesRepository.GetByIdAsync(volumes.VolumesID);
        //        if (volumesInDb == null)
        //            return NotFound();

        //        volumesInDb.VolumesDetails = volumes.VolumesDetails;
        //        //volumesInDb.Time = volumes.Time;
        //        volumesInDb.PartographID = volumes.PartographID;

        //        var updatedVolumes = unitOfWork.VolumesRepository.Update(volumesInDb);
        //        await unitOfWork.SaveChangesAsync();

        //        var volumesInDbToReturn = await unitOfWork.VolumesRepository.GetByIdAsync(updatedVolumes.VolumesID);

        //        return Ok(volumesInDbToReturn);
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogError(ex.Message);
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
        //    }
        //}
    }
}