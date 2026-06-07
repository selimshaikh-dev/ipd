using IPD.Domain.Dto;
using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IPD.Api.Controllers
{
    /// <summary>
    /// Partograph details controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PartographDetailsController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<PartographDetailsController> logger;

        /// <summary>
        /// Partograph details Controller constructor
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="logger"></param>
        public PartographDetailsController(IUnitOfWork unitOfWork, ILogger<PartographDetailsController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        /// <summary>
        /// Add new partographDetails.
        /// </summary>
        /// <param name="partographDetails"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddPartographDetails([FromBody] PartographDetailDto partographDetail)
        {
            try
            {
                var partographDetailInDb = new PartographDetail
                {
                    PartographID = partographDetail.PartographID,
                    Liquor = partographDetail.Liquor,
                    LiquorTime = partographDetail.LiquorTime,
                    Moulding = partographDetail.Moulding,
                    MouldingTime = partographDetail.MouldingTime,
                    Cervix = partographDetail.Cervix,
                    CervixTime = partographDetail.CervixTime,
                    DescentOfHead = partographDetail.DescentOfHead,
                    DescentOfHeadTime = partographDetail.DescentOfHeadTime,
                    Contractions = partographDetail.Contractions,
                    ContractionsDuration = partographDetail.ContractionsDuration,
                    ContractionsTime = partographDetail.ContractionsTime,
                    Oxytocin = partographDetail.Oxytocin,
                    OxytocinTime = partographDetail.OxytocinTime,
                    Drops = partographDetail.Drops,
                    DropsTime = partographDetail.DropsTime,
                    Medicine = partographDetail.Medicine,
                    MedicineTime = partographDetail.MedicineTime,
                    Systolic = partographDetail.Systolic,
                    Diastolic = partographDetail.Diastolic,
                    BpTime = partographDetail.BpTime,
                    Pulse = partographDetail.Pulse,
                    PulseTime = partographDetail.PulseTime,
                    Temp = partographDetail.Temp,
                    TempTime = partographDetail.TempTime,
                    Protein = partographDetail.Protein,
                    ProteinTime = partographDetail.ProteinTime,
                    Acetone = partographDetail.Acetone,
                    AcetoneTime = partographDetail.AcetoneTime,
                    Volume = partographDetail.Volume,
                    VolumeTime = partographDetail.VolumeTime,
                    FetalRate = partographDetail.FetalRate,
                    FetalRateTime = partographDetail.FetalRateTime,
                };

                var partographDetailAdded = unitOfWork.PartographDetailsRepository.Add(partographDetailInDb);
                await unitOfWork.SaveChangesAsync();

                var partographDetailToReturn = await unitOfWork.PartographDetailsRepository.GetByIdAsync(partographDetailAdded.PartographDetailsID);

                return Ok(partographDetailToReturn);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        ///  Load partographDetail  of a patient in specific admission.
        /// </summary>
        /// <param name="partographId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{partographId}")]
        public async Task<IActionResult> LoadPartographDetails(Guid partographId)
        {
            try
            {
                var partographDetailInDb = await unitOfWork.PartographDetailsRepository
                    .GetAll()
                    .Where(p => p.PartographID == partographId && p.IsRowDeleted.Equals(false))
                    .OrderByDescending(o => o.DateCreated)
                    .ToListAsync();

                return Ok(partographDetailInDb);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        ///  Find PartographDetails post by key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{key}")]
        public async Task<IActionResult> FindPartographDetailsByKey(Guid key)
        {
            try
            {
                if (key == Guid.Empty)
                    return BadRequest("Invalid key!");

                var partographDetailInDb = await unitOfWork.PartographDetailsRepository.GetByIdAsync(key);

                if (partographDetailInDb == null)
                    return NotFound();

                return Ok(partographDetailInDb);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        ///  Find PartographDetails post by key
        /// </summary>
        /// <param name="parographId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{parographId}")]
        public async Task<IActionResult> FindPartographDetailsByParographId(Guid parographId)
        {
            try
            {
                if (parographId == Guid.Empty)
                    return BadRequest("Invalid key!");

                var partographDetailInDb = await unitOfWork.PartographDetailsRepository.GetPartographDetailsAsync(parographId);

                if (partographDetailInDb == null)
                    return NotFound();

                return Ok(partographDetailInDb);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Updates existing partographDetail.
        /// </summary>
        /// <param name="partographDetail"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> EditPartographDetails([FromBody] PartographDetailDto partographDetail)
        {
            try
            {
                if (partographDetail.PartographDetailsID == Guid.Empty)
                    return BadRequest();

                var partographDetailInDb = await unitOfWork.PartographDetailsRepository.GetByIdAsync(partographDetail.PartographDetailsID);
                if (partographDetailInDb == null)
                    return NotFound();

                partographDetailInDb.PartographID = partographDetail.PartographID;
                partographDetailInDb.Liquor = partographDetail.Liquor;
                partographDetailInDb.LiquorTime = partographDetail.LiquorTime;
                partographDetailInDb.Moulding = partographDetail.Moulding;
                partographDetailInDb.MouldingTime = partographDetail.MouldingTime;
                partographDetailInDb.Cervix = partographDetail.Cervix;
                partographDetailInDb.CervixTime = partographDetail.CervixTime;
                partographDetailInDb.DescentOfHead = partographDetail.DescentOfHead;
                partographDetailInDb.DescentOfHeadTime = partographDetail.DescentOfHeadTime;
                partographDetailInDb.Contractions = partographDetail.Contractions;
                partographDetailInDb.ContractionsDuration = partographDetail.ContractionsDuration;
                partographDetailInDb.ContractionsTime = partographDetail.ContractionsTime;
                partographDetailInDb.Oxytocin = partographDetail.Oxytocin;
                partographDetailInDb.OxytocinTime = partographDetail.OxytocinTime;
                partographDetailInDb.Drops = partographDetail.Drops;
                partographDetailInDb.DropsTime = partographDetail.DropsTime;
                partographDetailInDb.Medicine = partographDetail.Medicine;
                partographDetailInDb.MedicineTime = partographDetail.MedicineTime;
                partographDetailInDb.Systolic = partographDetail.Systolic;
                partographDetailInDb.Diastolic = partographDetail.Diastolic;
                partographDetailInDb.BpTime = partographDetail.BpTime;
                partographDetailInDb.Pulse = partographDetail.Pulse;
                partographDetailInDb.Temp = partographDetail.Temp;
                partographDetailInDb.TempTime = partographDetail.TempTime;
                partographDetailInDb.Protein = partographDetail.Protein;
                partographDetailInDb.ProteinTime = partographDetail.ProteinTime;
                partographDetailInDb.Acetone = partographDetail.Acetone;
                partographDetailInDb.AcetoneTime = partographDetail.AcetoneTime;
                partographDetailInDb.Volume = partographDetail.Volume;
                partographDetailInDb.VolumeTime = partographDetail.VolumeTime;
                partographDetailInDb.FetalRate = partographDetail.FetalRate;
                partographDetailInDb.FetalRateTime = partographDetail.FetalRateTime;

                var updatedpartographDetails = unitOfWork.PartographDetailsRepository.Update(partographDetailInDb);
                await unitOfWork.SaveChangesAsync();

                var partographDetailsToReturn = await unitOfWork.PartographDetailsRepository.GetByIdAsync(updatedpartographDetails.PartographDetailsID);

                return Ok(partographDetailsToReturn);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }
    }
}