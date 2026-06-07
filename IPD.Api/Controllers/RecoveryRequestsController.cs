using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace IPD.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecoveryRequestsController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<RecoveryRequestsController> logger;

        public RecoveryRequestsController(IUnitOfWork unitOfWork, ILogger<RecoveryRequestsController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        [HttpGet]
        [Route("GetFacilitiesById")]
        public IActionResult GetRecoveryById(int recoveryRequest)
        {
            try
            {
                var GetrecoveryRequestId = unitOfWork.RecoveryRequestRepository.GetById(recoveryRequest);
                return Ok(GetrecoveryRequestId);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        #region SaveOrUpdate

        [HttpPost]
        public IActionResult SaveOrUpdate([FromBody] RecoveryRequest recoveryRequest)
        {
            if (recoveryRequest.RecoveryRequestID == Guid.Empty)
            {
                var RecoveryAdd = unitOfWork.RecoveryRequestRepository.Add(recoveryRequest);
                unitOfWork.SaveChanges();
                return Ok(RecoveryAdd);
            }
            else
            {
                var RecoveryUp = unitOfWork.RecoveryRequestRepository.Update(recoveryRequest);
                unitOfWork.SaveChanges();

                return Ok(RecoveryUp);
            }
        }

        #endregion SaveOrUpdate
    }
}