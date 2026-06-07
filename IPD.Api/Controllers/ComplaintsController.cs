using IPD.Domain.Dto;
using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IPD.Api.Controllers
{
    /// <summary>
    /// Complaints Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ComplaintsController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<ComplaintsController> logger;

        /// <summary>
        /// Complaints constructor
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="logger"></param>
        public ComplaintsController(IUnitOfWork unitOfWork, ILogger<ComplaintsController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        /// <summary>
        /// Saves a complaint.
        /// </summary>complaint
        /// <param name="complaint"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddComplaints([FromHeader(Name = "x-facility-code")] string facilityCode, [FromBody] ComplaintDto complaint)
        {
            try
            {
                var complaintInDb = new Complaint
                {
                    ComplaintName = complaint.ComplaintName,
                    ComplaintHistory = complaint.ComplaintHistory,
                    SystemsReview = complaint.SystemsReview,
                    Diabetes = complaint.Diabetes,
                    Hypertention = complaint.Hypertention,
                    Epilepsy = complaint.Epilepsy,
                    SpecialNote = complaint.SpecialNote,
                    AdmissionID = complaint.AdmissionID,
                    FacilityCode = facilityCode,
                };

                complaintInDb.PatientsNcds = complaint.PatientsNcds?.Any() ?? false
                    ? complaint.PatientsNcds.Select(patientsNcd => new PatientsNcd
                    {
                        NcdsID = patientsNcd.NcdsID,
                        IsRowDeleted = false
                    }).ToList()
                    : new List<PatientsNcd>();

                complaintInDb.PatientAllergy = complaint.PatientAllergy?.Any() ?? false
                    ? complaint.PatientAllergy.Select(patientAllergy => new PatientAllergy
                    {
                        AllergiesID = patientAllergy.AllergiesID,
                        IsRowDeleted = false
                    }).ToList()
                    : new List<PatientAllergy>();

                var complaintAdded = unitOfWork.ComplaintsRepository.Add(complaintInDb);
                await unitOfWork.SaveChangesAsync();

                var complaintToReturn = await unitOfWork.ComplaintsRepository.GetByIdAsync(complaintAdded.ComplaintID);

                return Ok(complaintToReturn);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Load Complaint.
        /// </summary>
        /// <param name="admissionId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{admissionId}")]
        public async Task<IActionResult> LoadComplaints(Guid admissionId)
        {
            try
            {

                var complaintInDb = await unitOfWork.ComplaintsRepository
                    .GetAllLoadCompient(admissionId);

                return Ok(complaintInDb);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }
       

        /// <summary>
        /// Finds a Complaint
        /// <param name="key"></param>
        /// <returns></returns>Complaint
        [HttpGet]
        [Route("[action]/{key}")]
        public async Task<IActionResult> FindComplaintByKey(Guid key)
        {
            try
            {
                var complaintInDb = await unitOfWork.ComplaintsRepository
                    .GetAll()
                    .Include(x => x.PatientsNcds)
                    .Include(x => x.PatientAllergy)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.ComplaintID == key);

                if (complaintInDb == null)
                {
                    return NotFound();
                }

                return Ok(complaintInDb);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        ///  Updates existing  Complaint.
        /// </summary>
        /// <param name="complaint"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> EditComplaints([FromBody] ComplaintDto complaint)
        {
            try
            {
                if (complaint.ComplaintID == Guid.Empty)
                {
                    return BadRequest();
                }

                var complaintInDb = await unitOfWork.ComplaintsRepository
                    .GetAll()
                    .Include(x => x.PatientsNcds)
                    .Include(x => x.PatientAllergy)
                    .FirstOrDefaultAsync(x => x.ComplaintID == complaint.ComplaintID);

                if (complaintInDb == null)
                {
                    return NotFound();
                }

                complaintInDb.ComplaintName = complaint.ComplaintName;
                complaintInDb.ComplaintHistory = complaint.ComplaintHistory;
                complaintInDb.SystemsReview = complaint.SystemsReview;
                complaintInDb.Diabetes = complaint.Diabetes;
                complaintInDb.Hypertention = complaint.Hypertention;
                complaintInDb.Epilepsy = complaint.Epilepsy;
                complaintInDb.SpecialNote = complaint.SpecialNote;
                complaintInDb.AdmissionID = complaint.AdmissionID;

                unitOfWork.PatientsNcdRepository.RemoveRange(complaintInDb.PatientsNcds);

                if (complaint.PatientsNcds?.Any() ?? false)
                {
                    unitOfWork.PatientsNcdRepository.AddRange(complaint.PatientsNcds.Select(patientsNcd => new PatientsNcd
                    {
                        NcdsID = patientsNcd.NcdsID,
                        ComplaintID = complaintInDb.ComplaintID,
                        IsRowDeleted = false
                    }).ToList());
                }

                unitOfWork.PatientAllergyRepository.RemoveRange(complaintInDb.PatientAllergy);

                if (complaint.PatientAllergy?.Any() ?? false)
                {
                    unitOfWork.PatientAllergyRepository.AddRange(complaint.PatientAllergy.Select(patientAllergy => new PatientAllergy
                    {
                        AllergiesID = patientAllergy.AllergiesID,
                        ComplaintID = complaintInDb.ComplaintID,
                        IsRowDeleted = false
                    }).ToList());
                }

                var complaintUpdated = unitOfWork.ComplaintsRepository.Update(complaintInDb);
                await unitOfWork.SaveChangesAsync();

                var complaintToReturn = await unitOfWork.ComplaintsRepository.GetByIdAsync(complaintUpdated.ComplaintID);

                return Ok(complaintToReturn);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }
    }
}