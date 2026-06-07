using IPD.Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace IPD.Api.Controllers
{
    /// <summary>
    /// PatientDetailsController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PatientDetailsController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<PatientDetailsController> logger;

        /// <summary>
        /// PatientDetails constructor.
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="logger"></param>
        public PatientDetailsController(IUnitOfWork unitOfWork, ILogger<PatientDetailsController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        /// <summary>
        /// Add PatientDetail.
        /// </summary>
        /// <param name="patientDetails"></param>
        /// <returns></returns>
        //[HttpPost]
        //[Route("[action]")]
        //public async Task<IActionResult> AddPatientDetails([FromBody] PatientDetailsDto patientDetails)
        //{
        //    try
        //    {
        //        var patientDetailsInDb = new PatientDetail
        //        {
        //            AdmissionID = patientDetails.AdmissionID,
        //            PassportNumber = patientDetails.PassportNumber,
        //            IDNumber = patientDetails.IDNumber,
        //            Language = patientDetails.Language,
        //            Occupation = patientDetails.Occupation,
        //            Relegion = patientDetails.Relegion,
        //            Employer = patientDetails.Employer,
        //            Allergies = patientDetails.Allergies,
        //            ChronicIllness = patientDetails.ChronicIllness,
        //            Medication = patientDetails.Medication,
        //            RegionID = patientDetails.RegionID,

        //        };

        //        var patientDetailsAdded = unitOfWork.PatientDetailsRepository.Add(patientDetailsInDb);
        //        await unitOfWork.SaveChangesAsync();

        //        var patientDetailsToReturn = await unitOfWork.PatientDetailsRepository.GetByIdAsync(patientDetailsAdded.PatientDetailsID);

        //        return Ok(patientDetailsToReturn);
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogError(ex.Message);
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
        //    }
        //}

        ///// <summary>
        ///// Load Admission  of a patient in specific admission.
        ///// </summary>
        ///// <param name="admissionId"></param>
        ///// <returns></returns>
        //[HttpGet]
        //[Route("[action]/{admissionId}")]
        //public async Task<IActionResult> LoadPatientDetails(Guid admissionId)
        //{
        //    try
        //    {
        //        var patientDetailsInDb = await unitOfWork.PatientDetailsRepository
        //            .GetAll()
        //            .Where(s => s.AdmissionID == admissionId && s.IsRowDeleted.Equals(false))
        //            .OrderByDescending(o => o.DateCreated)
        //            .ToListAsync();

        //        return Ok(patientDetailsInDb);
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogError(ex.Message);
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
        //    }
        //}

        ///// <summary>
        /////  Find PatientDetails by key
        ///// </summary>
        ///// <param name="key"></param>
        ///// <returns></returns>
        //[HttpGet]
        //[Route("[action]/{key}")]
        //public async Task<IActionResult> FindPatientDetailsByKey(Guid key)
        //{
        //    try
        //    {
        //        if (key == Guid.Empty)
        //            return BadRequest("Invalid key!");

        //        var patientDetailsInDb = await unitOfWork.PatientDetailsRepository.GetByIdAsync(key);

        //        if (patientDetailsInDb == null)
        //            return NotFound();

        //        return Ok(patientDetailsInDb);
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogError(ex.Message);
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
        //    }
        //}

        ///// <summary>
        ///// Updates existing PatientDetails.
        ///// </summary>
        ///// <param name="surgery"></param>
        ///// <returns></returns>
        //[HttpPut]
        //[Route("[action]")]
        //public async Task<IActionResult> EditPatientDetails([FromBody] PatientDetailsDto patientDetails)
        //{
        //    try
        //    {
        //        if (patientDetails.PatientDetailsID == Guid.Empty)
        //            return BadRequest();

        //        var patientDetailsInDb = await unitOfWork.PatientDetailsRepository.GetByIdAsync(patientDetails.PatientDetailsID);
        //        if (patientDetailsInDb == null)
        //            return NotFound();

        //        patientDetailsInDb.AdmissionID = patientDetails.AdmissionID;
        //        patientDetailsInDb.PassportNumber = patientDetails.PassportNumber;
        //        patientDetailsInDb.IDNumber = patientDetails.IDNumber;
        //        patientDetailsInDb.Language = patientDetails.Language;
        //        patientDetailsInDb.Occupation = patientDetails.Occupation;
        //        patientDetailsInDb.Relegion = patientDetails.Relegion;
        //        patientDetailsInDb.Employer = patientDetails.Employer;
        //        patientDetailsInDb.Allergies = patientDetails.Allergies;
        //        patientDetailsInDb.ChronicIllness = patientDetails.ChronicIllness;
        //        patientDetailsInDb.Medication = patientDetails.Medication;
        //        patientDetailsInDb.RegionID = patientDetails.RegionID;

        //        var patientDetailsUpdated = unitOfWork.PatientDetailsRepository.Update(patientDetailsInDb);
        //        await unitOfWork.SaveChangesAsync();

        //        var patientDetailsToReturn = await unitOfWork.PatientDetailsRepository.GetByIdAsync(patientDetailsUpdated.PatientDetailsID);

        //        return Ok(patientDetailsToReturn);
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogError(ex.Message);
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
        //    }
    }
}