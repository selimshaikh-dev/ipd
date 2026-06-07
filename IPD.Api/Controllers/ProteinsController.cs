using IPD.Domain.Dto;
using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace IPD.Api.Controllers
{
    /// <summary>
    /// Proteins controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProteinsController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<ProteinsController> logger;

        /// <summary>
        /// Proteins constructor
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="logger"></param>
        public ProteinsController(IUnitOfWork unitOfWork, ILogger<ProteinsController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        /// <summary>
        /// Add new proteins.
        /// </summary>
        /// <param name="proteins"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddProteins([FromBody] ProteinsCreateDto proteins)
        {
            try
            {
                var proteinsList = proteins.Data?.Select(x => new Protein()
                {
                    ProteinsDetails = Convert.ToString(x[1]),
                    ProteinsTime = Convert.ToInt64(x[0]),
                    PartographID = proteins.PartographID,
                }).ToList() ?? new List<Protein>();

                foreach (var item in proteinsList)
                {
                    unitOfWork.ProteinsRepository.UpdateProtein(item);
                }

                await unitOfWork.SaveChangesAsync();

                return Ok(proteinsList);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        ///  Load proteins  of a patient in specific admission.
        /// </summary>
        /// <param name="partographId"></param>
        /// <returns></returns>
        //[HttpGet]
        //[Route("[action]/{partographId}")]
        //public async Task<IActionResult> LoadProteins(Guid partographId)
        //{
        //    try
        //    {
        //        var proteinsInDb = await unitOfWork.ProteinsRepository
        //            .GetAll()
        //            .Where(l => l.PartographID == partographId && l.IsRowDeleted.Equals(false))
        //            .OrderByDescending(o => o.DateCreated)
        //            .ToListAsync();

        //        var data = proteinsInDb.Select(x => new long[]
        //        {
        //                x.ProteinsTime,
        //                x.ProteinsDetails
        //        })
        //        .OrderBy(i => i[0])
        //        .ToList();
        //        var proteins = new ProteinsCreateDto();
        //        if (data.Count > 0)
        //        {
        //            proteins = new ProteinsCreateDto()
        //            {
        //                PartographID = partographId,
        //                Data = data
        //            };
        //        }

        //        return Ok(proteins);
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogError(ex.Message);
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
        //    }


        /// <summary>
        ///  Find proteins post by key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        //[HttpGet]
        //[Route("[action]/{key}")]
        //public async Task<IActionResult> FindProteinsByKey(Guid key)
        //{
        //    try
        //    {
        //        if (key == Guid.Empty)
        //            return BadRequest("Invalid key!");

        //        var proteinsInDb = await unitOfWork.ProteinsRepository.GetByIdAsync(key);

        //        if (proteinsInDb == null)
        //            return NotFound();

        //        return Ok(proteinsInDb);
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogError(ex.Message);
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
        //    }
        //}

        /// <summary>
        /// Updates existing proteins.
        /// </summary>
        /// <param name="proteins"></param>
        /// <returns></returns>
        //[HttpPut]
        //[Route("[action]")]
        //public async Task<IActionResult> EditProteins([FromBody] ProteinsCreateDto proteins)
        //{
        //    try
        //    {
        //        if (proteins.ProteinsID == Guid.Empty)
        //            return BadRequest();

        //        var proteinsInDb = await unitOfWork.ProteinsRepository.GetByIdAsync(proteins.ProteinsID);
        //        if (proteinsInDb == null)
        //            return NotFound();

        //        proteinsInDb.ProteinsDetails = proteins.ProteinsDetails;
        //        //proteinsInDb.Time = proteins.Time;
        //        proteinsInDb.PartographID = proteins.PartographID;

        //        var updatedproteins = unitOfWork.ProteinsRepository.Update(proteinsInDb);
        //        await unitOfWork.SaveChangesAsync();

        //        var proteinsToReturn = await unitOfWork.ProteinsRepository.GetByIdAsync(updatedproteins.ProteinsID);

        //        return Ok(proteinsToReturn);
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogError(ex.Message);
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
        //    }
        //}
    }
}