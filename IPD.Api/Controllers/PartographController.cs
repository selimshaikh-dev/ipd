using IPD.Domain.Dto;
using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IPD.Api.Controllers
{
    /// <summary>
    /// Partograph controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PartographController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<PartographController> logger;

        /// <summary>
        /// Partograph constructor
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="logger"></param>
        public PartographController(IUnitOfWork unitOfWork, ILogger<PartographController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        /// <summary>
        /// Add new partograph.
        /// </summary>
        /// <param name="partograph"></param>
        /// <returns></returns>
        //[HttpPost]
        //[Route("[action]")]
        //public async Task<IActionResult> AddPartograph([FromBody] PartographDto partograph)
        //{
        //    try
        //    {
        //        var partographInDb = new Partograph
        //        {
        //            AdmissionID = partograph.AdmissionID,
        //            Gravida = partograph.Gravida,
        //            Parity = partograph.Parity,
        //            SBOrNND = partograph.SBOrNND,
        //            Abortion = partograph.Abortion,
        //            EDD = partograph.EDD,
        //            BorderlineRiskFactors = partograph.BorderlineRiskFactors,
        //            Height = partograph.Height,
        //            RegularContractions = partograph.RegularContractions,
        //            MembranesRuptured = partograph.MembranesRuptured,
        //            InitiateDate = partograph.InitiateDate,
        //            InitiateTime = partograph.InitiateTime
        //        };

        //        var partographAdded = unitOfWork.PartographRepository.Add(partographInDb);
        //        await unitOfWork.SaveChangesAsync();

        //        var partographToReturn = await unitOfWork.PartographRepository.GetByIdAsync(partographAdded.PartographID);

        //        return Ok(partographToReturn);
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogError(ex.Message);
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
        //    }
        //}


        #region SaveOrUpdate
        /// <summary>
        /// Add new and update partograph.
        /// </summary>
        /// <param name="partograph"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> SaveOrUpdatePartograph([FromBody] PartographDto partograph)
        {
            try
            {
                if (partograph.PartographID == Guid.Empty)
                {
                    var partographInDb = new Partograph
                    {
                        AdmissionID = partograph.AdmissionID,
                        Gravida = partograph.Gravida,
                        Parity = partograph.Parity,
                        SBOrNND = partograph.SBOrNND,
                        Abortion = partograph.Abortion,
                        EDD = partograph.EDD,
                        BorderlineRiskFactors = partograph.BorderlineRiskFactors,
                        Height = partograph.Height,
                        RegularContractions = partograph.RegularContractions,
                        MembranesRuptured = partograph.MembranesRuptured,
                        InitiateDate = partograph.InitiateDate,
                        InitiateTime = partograph.InitiateTime
                    };

                    var partographAdd = unitOfWork.PartographRepository.Add(partographInDb);
                    await unitOfWork.SaveChangesAsync();
                    return Ok(partographAdd);
                }
                else
                {
                    var partographInDb = await unitOfWork.PartographRepository.GetByIdAsync(partograph.PartographID);

                    if (partographInDb == null)
                        return NotFound();

                    partographInDb.AdmissionID = partograph.AdmissionID;
                    partographInDb.Gravida = partograph.Gravida;
                    partographInDb.Parity = partograph.Parity;
                    partographInDb.SBOrNND = partograph.SBOrNND;
                    partographInDb.Abortion = partograph.Abortion;
                    partographInDb.EDD = partograph.EDD;
                    partographInDb.BorderlineRiskFactors = partograph.BorderlineRiskFactors;
                    partographInDb.Height = partograph.Height;
                    partographInDb.RegularContractions = partograph.RegularContractions;
                    partographInDb.MembranesRuptured = partograph.MembranesRuptured;
                    //partographInDb.InitiateDate = partograph.InitiateDate;
                    //partographInDb.InitiateTime = partograph.InitiateTime;

                    var updatedPartograph = unitOfWork.PartographRepository.Update(partographInDb);
                    await unitOfWork.SaveChangesAsync();

                    return Ok(updatedPartograph);
                }             
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
        #endregion

        /// <summary>
        ///  Load partograph  of a patient in specific admission.
        /// </summary>
        /// <param name="admissionId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{admissionId}")]
        public async Task<IActionResult> LoadPartograph(Guid admissionId)
        {
            try
            {
                var partographInDb = await unitOfWork.PartographRepository
                    .GetAll()
                    .Where(p => p.AdmissionID == admissionId && p.IsRowDeleted.Equals(false))
                    .OrderByDescending(o => o.DateCreated)
                    .ToListAsync();

                return Ok(partographInDb);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        ///  Load partograph  of a patient in specific admission.
        /// </summary>
        /// <param name="admissionId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{admissionId}")]
        public async Task<IActionResult> LoadPartographDetailsAdmissionId(Guid admissionId)
        {
            try
            {
                var partographInDb = await unitOfWork.PartographRepository
                    .GetPartographByAdmissionId(admissionId);
                return Ok(partographInDb);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }


        /// <summary>
        ///  Load partograph  of a patient in specific admission.
        /// </summary>
        /// <param name="admissionId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{admissionId}")]
        public async Task<IActionResult> LoadPartographByAdmissionId(Guid admissionId)
        {
            try
            {
                var partographInDb = await unitOfWork.PartographRepository
                    .GetPartographIdByAdmissionId(admissionId);                
                    return Ok(partographInDb);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }
        /// <summary>
        ///  Find partograph post by key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{key}")]
        public async Task<IActionResult> FindPartographByKey(Guid key)
        {
            try
            {
                if (key == Guid.Empty)
                    return BadRequest("Invalid key!");

                var partographInDb = await unitOfWork.PartographRepository.GetByIdAsync(key);

                if (partographInDb == null)
                    return NotFound();

                return Ok(partographInDb);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Updates existing partograph.
        /// </summary>
        /// <param name="partograph"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> EditPartograph([FromBody] PartographDto partograph)
        {
            try
            {
                if (partograph.PartographID == Guid.Empty)
                    return BadRequest();

                var partographInDb = await unitOfWork.PartographRepository.GetByIdAsync(partograph.PartographID);
                if (partographInDb == null)
                    return NotFound();

                partographInDb.AdmissionID = partograph.AdmissionID;
                partographInDb.Gravida = partograph.Gravida;
                partographInDb.Parity = partograph.Parity;
                partographInDb.SBOrNND = partograph.SBOrNND;
                partographInDb.Abortion = partograph.Abortion;
                partographInDb.EDD = partograph.EDD;
                partographInDb.BorderlineRiskFactors = partograph.BorderlineRiskFactors;
                partographInDb.Height = partograph.Height;
                partographInDb.RegularContractions = partograph.RegularContractions;
                partographInDb.MembranesRuptured = partograph.MembranesRuptured;
                partographInDb.InitiateDate = partograph.InitiateDate;
                partographInDb.InitiateTime = partograph.InitiateTime;

                var updatedPartograph = unitOfWork.PartographRepository.Update(partographInDb);
                await unitOfWork.SaveChangesAsync();

                var partographInDbToReturn = await unitOfWork.PartographRepository.GetByIdAsync(updatedPartograph.PartographID);

                return Ok(partographInDbToReturn);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }
    }
}