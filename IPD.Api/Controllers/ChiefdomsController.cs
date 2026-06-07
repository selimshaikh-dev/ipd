using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace IPD.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChiefdomsController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<ChiefdomsController> logger;

        public ChiefdomsController(IUnitOfWork unitOfWork, ILogger<ChiefdomsController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        [HttpGet]
        [Route("GetChiefdomById")]
        public IActionResult GetChiefdomById(int ChiefdomId)
        {
            try
            {
                var GetClientByID = unitOfWork.ChiefdomsRepository.GetById(ChiefdomId);
                return Ok(GetClientByID);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        #region SaveOrUpdate

        [HttpPost]
        public IActionResult SaveOrUpdate([FromBody] Chiefdom chiefdoms)
        {
            if (chiefdoms.ChiefdomID == 0)
            {
                var chiefdomsAdd = unitOfWork.ChiefdomsRepository.Add(chiefdoms);
                unitOfWork.SaveChanges();
                return Ok(chiefdomsAdd);
            }
            else
            {
                var chiefdomsUp = unitOfWork.ChiefdomsRepository.Update(chiefdoms);
                unitOfWork.SaveChanges();

                return Ok(chiefdomsUp);
            }
        }

        #endregion SaveOrUpdate

        [HttpGet]
        [Route("LoadChiefdom")]
        public async Task<IActionResult> LoadChiefdom(int InkhundlaID)
        {
            try
            {
                var dropdown = await unitOfWork.ChiefdomsRepository.GetChiefdomListAsync(InkhundlaID);
                return Ok(dropdown);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}