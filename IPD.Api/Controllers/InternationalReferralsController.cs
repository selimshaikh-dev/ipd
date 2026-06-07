using IPD.Domain.Dto;
using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IPD.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InternationalReferralsController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<InternationalReferralsController> logger;

        /// <summary>
        /// InternationalReferrals Controller constructor
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="logger"></param>
        public InternationalReferralsController(IUnitOfWork unitOfWork, ILogger<InternationalReferralsController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        [HttpPost]
        [Route("AddInternationalReferrals")]
        public async Task<IActionResult> AddInternationalReferrals([FromHeader(Name = "x-facility-code")] string facilityCode, [FromBody] InternationalReferral internationalReferral)
        {
            try
            {
                var internationalReferralInDb = new InternationalReferral
                {
                    InternationalReferralID = internationalReferral.InternationalReferralID,
                    Phalala = internationalReferral.Phalala,
                    CivilServent = internationalReferral.CivilServent,
                    EmploymentNumber = internationalReferral.EmploymentNumber,
                    ReferralType = internationalReferral.ReferralType,
                    ReferringSpecialist = internationalReferral.ReferringSpecialist,
                    PracticeNumber = internationalReferral.PracticeNumber,
                    Discipline = internationalReferral.Discipline,
                    ReasonReferral = internationalReferral.ReasonReferral,
                    ShortHistory = internationalReferral.ShortHistory,
                    Investigation = internationalReferral.Investigation,
                    ContactDetails = internationalReferral.ContactDetails,
                    Date = internationalReferral.Date,
                    Time = internationalReferral.Time,
                    PatientsTransferApparatus = internationalReferral.PatientsTransferApparatus,
                    PassportNumber = internationalReferral.PassportNumber,
                    IDNumber = internationalReferral.IDNumber,
                    Language = internationalReferral.Language,
                    Occupation = internationalReferral.Occupation,
                    Relegion = internationalReferral.Relegion,
                    Employer = internationalReferral.Employer,
                    Allergies = internationalReferral.Allergies,
                    ChronicIllness = internationalReferral.ChronicIllness,
                    Medication = internationalReferral.Medication,
                    RegionID = internationalReferral.RegionID,
                    AdmissionID = internationalReferral.AdmissionID,
                    ProcedureID = internationalReferral.ProcedureID,
                    FacilityCode = facilityCode
                };

                var internationalReferralAdded = unitOfWork.InternationalReferralRepository.Add(internationalReferralInDb);
                await unitOfWork.SaveChangesAsync();

                var internationalReferralToReturn = await unitOfWork.InternationalReferralRepository.GetByIdAsync(internationalReferralAdded.InternationalReferralID);

                return Ok(internationalReferralToReturn);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        ///  Load internationalReferrals  of a patient in specific surgery.
        /// </summary>
        /// <param name="admissionId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{admissionId}")]
        public async Task<IActionResult> LoadInternationalReferrals(Guid admissionId)
        {
            try
            {
                var internationalReferralInDb = await unitOfWork.InternationalReferralRepository
                    .GetAll()
                    .Where(l => l.AdmissionID == admissionId && l.IsRowDeleted.Equals(false))
                    .OrderByDescending(o => o.DateCreated)
                    .ToListAsync();

                return Ok(internationalReferralInDb);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        ///  Find internationalReferrals post by key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{key}")]
        public async Task<IActionResult> FindInternationalReferralByKey(Guid key)
        {
            try
            {
                if (key == Guid.Empty)
                    return BadRequest("Invalid key!");

                var internationalReferralInDb = await unitOfWork.InternationalReferralRepository.GetByIdAsync(key);

                if (internationalReferralInDb == null)
                    return NotFound();

                return Ok(internationalReferralInDb);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Updates existing InternationalReferral.
        /// </summary>
        /// <param name="internationalReferral"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> EditInternationalReferrals([FromBody] InternationalReferralsDto internationalReferral)
        {
            try
            {
                if (internationalReferral.InternationalReferralID == Guid.Empty)
                    return BadRequest();

                var internationalReferralInDb = await unitOfWork.InternationalReferralRepository.GetByIdAsync(internationalReferral.InternationalReferralID);
                if (internationalReferralInDb == null)
                    return NotFound();

                internationalReferralInDb.Phalala = internationalReferral.Phalala;
                internationalReferralInDb.CivilServent = internationalReferral.CivilServent;
                internationalReferralInDb.EmploymentNumber = internationalReferral.EmploymentNumber;
                internationalReferralInDb.ReferralType = internationalReferral.ReferralType;
                internationalReferralInDb.ReferringSpecialist = internationalReferral.ReferringSpecialist;
                internationalReferralInDb.PracticeNumber = internationalReferral.PracticeNumber;
                internationalReferralInDb.Discipline = internationalReferral.Discipline;
                internationalReferralInDb.ReasonReferral = internationalReferral.ReasonReferral;
                internationalReferralInDb.ShortHistory = internationalReferral.ShortHistory;
                internationalReferralInDb.Investigation = internationalReferral.Investigation;
                internationalReferralInDb.ContactDetails = internationalReferral.ContactDetails;
                internationalReferralInDb.Date = internationalReferral.Date;
                internationalReferralInDb.Time = internationalReferral.Time;
                internationalReferralInDb.PatientsTransferApparatus = internationalReferral.PatientsTransferApparatus;
                internationalReferralInDb.PassportNumber = internationalReferral.PassportNumber;
                internationalReferralInDb.IDNumber = internationalReferral.IDNumber;
                internationalReferralInDb.Language = internationalReferral.Language;
                internationalReferralInDb.Occupation = internationalReferral.Occupation;
                internationalReferralInDb.Relegion = internationalReferral.Relegion;
                internationalReferralInDb.Employer = internationalReferral.Employer;
                internationalReferralInDb.Allergies = internationalReferral.Allergies;
                internationalReferralInDb.ChronicIllness = internationalReferral.ChronicIllness;
                internationalReferralInDb.Medication = internationalReferral.Medication;
                internationalReferralInDb.RegionID = internationalReferral.RegionID;
                internationalReferralInDb.AdmissionID = internationalReferral.AdmissionID;
                internationalReferralInDb.ProcedureID = internationalReferral.ProcedureID;

                var updatedinternationalReferral = unitOfWork.InternationalReferralRepository.Update(internationalReferralInDb);
                await unitOfWork.SaveChangesAsync();

                var localinternationalReferral = await unitOfWork.InternationalReferralRepository.GetByIdAsync(updatedinternationalReferral.InternationalReferralID);

                return Ok(localinternationalReferral);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }
    }
}