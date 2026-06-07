using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IPD.Api.Controllers
{
    /// <summary>
    /// Tinkhundlas Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TinkhundlasController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<TinkhundlasController> logger;

        /// <summary>
        /// Tinkhundlas constructor
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="logger"></param>
        public TinkhundlasController(IUnitOfWork unitOfWork, ILogger<TinkhundlasController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        /// <summary>
        /// GetTinkhundlaById
        /// </summary>
        /// <param name="tinkhundlaId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetTinkhundlaById")]
        public IActionResult GetTinkhundlaById(int tinkhundlaId)
        {
            try
            {
                var GetTinkhundlaIdByID = unitOfWork.TinkhundlaRepository.GetById(tinkhundlaId);
                return Ok(GetTinkhundlaIdByID);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }


        #region SaveOrUpdate
        /// <summary>
        /// SaveOrUpdate
        /// </summary>
        /// <param name="tinkhundla"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SaveOrUpdate([FromBody] Tinkhundla tinkhundla)
        {
            if (tinkhundla.TinkhundlaID == 0)
            {
                var TinkhundlaAdd = unitOfWork.TinkhundlaRepository.Add(tinkhundla);
                unitOfWork.SaveChanges();
                return Ok(TinkhundlaAdd);
            }
            else
            {
                var TinkhundlaUp = unitOfWork.TinkhundlaRepository.Update(tinkhundla);
                unitOfWork.SaveChanges();

                return Ok(TinkhundlaUp);
            }
        }

        #endregion SaveOrUpdate
        /// <summary>
        /// Load Inkhundla
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> LoadInkhundla()
        {
            try
            {
                var Inkhundla = await unitOfWork.TinkhundlaRepository
                    .GetAll()
                    .Where(x => x.IsRowDeleted.Equals(false))
                    .ToListAsync();
                return Ok(Inkhundla);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }
    }
}