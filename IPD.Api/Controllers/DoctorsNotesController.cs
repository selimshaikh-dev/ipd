using IPD.Domain.Dto;
using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IPD.Api.Controllers
{
    /// <summary>
    /// DoctorsNotesController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsNotesController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<DoctorsNotesController> logger;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="unitOfWork">IUnitOfWork</param>
        /// <param name="logger">ILogger</param>
        public DoctorsNotesController(IUnitOfWork unitOfWork, ILogger<DoctorsNotesController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        /// <summary>
        /// Saves a Doctors Note.
        /// </summary>
        /// <param name="doctorsNote"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddDoctorsNote([FromHeader(Name = "x-facility-code")] string facilityCode, [FromBody] DoctorNotesDto doctorsNote)
        {
            try
            {
                var doctorNoteInDb = new DoctorsNote
                {
                    DoctorsNoteID = doctorsNote.DoctorsNoteID,
                    DateOfNote = doctorsNote.DateOfNote,
                    TimeOfNote = doctorsNote.TimeOfNote,
                    Observation = doctorsNote.Observation,
                    TestRequest = doctorsNote.TestRequest,
                    FacilityCode = facilityCode,
                    AdmissionID = doctorsNote.AdmissionID,
                    DateCreated = DateTime.Now
                };

                var doctorsNoteAdded = unitOfWork.DoctorsNoteRepository.Add(doctorNoteInDb);
                await unitOfWork.SaveChangesAsync();

                var doctorNoteToReturn = await unitOfWork.DoctorsNoteRepository.GetByIdAsync(doctorsNoteAdded.DoctorsNoteID);

                return Ok(doctorNoteToReturn);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Load DoctorsNotes of a patient in specific admission.
        /// </summary>
        /// <param name="admissionId">Guid</param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{admissionId}")]
        public async Task<IActionResult> LoadDoctorsNotes(Guid admissionId)
        {
            try
            {
                var doctorsNoteInDb = await unitOfWork.DoctorsNoteRepository
                    .GetAllDoctorNote(admissionId);
                //.GetAll()
                //.Where(x => x.AdmissionID == admissionId && x.IsRowDeleted.Equals(false))
                //.OrderByDescending(o => o.DateCreated)
                //.ToListAsync();

                return Ok(doctorsNoteInDb);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Finds a DoctorsNote info  from DoctorsNotes table using primary key.
        /// </summary>
        /// <param name="key">Guid</param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{key}")]
        public async Task<IActionResult> FindDoctorsNoteByKey(Guid key)
        {
            try
            {
                if (key == Guid.Empty)
                    return BadRequest("Invalid key!");

                var doctorsNoteInDb = await unitOfWork.DoctorsNoteRepository.GetByIdAsync(key);

                if (doctorsNoteInDb == null)
                    return NotFound();

                return Ok(doctorsNoteInDb);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Updates existing  DoctorsNote.
        /// </summary>
        /// <param name="doctorsNote">DoctorsNote</param>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> EditDoctorsNote([FromBody] DoctorNotesDto doctorsNote)
        {
            try
            {
                if (doctorsNote.DoctorsNoteID == Guid.Empty)
                    return BadRequest();

                var doctorNoteInDb = await unitOfWork.DoctorsNoteRepository.GetByIdAsync(doctorsNote.DoctorsNoteID);

                if (doctorNoteInDb == null)
                    return NotFound();

                doctorNoteInDb.DoctorsNoteID = doctorsNote.DoctorsNoteID;
                doctorNoteInDb.DateOfNote = doctorsNote.DateOfNote;
                doctorNoteInDb.TimeOfNote = doctorsNote.TimeOfNote;
                doctorNoteInDb.Observation = doctorsNote.Observation;
                doctorNoteInDb.TestRequest = doctorsNote.TestRequest;
                doctorNoteInDb.FacilityCode = doctorsNote.FacilityCode;
                doctorNoteInDb.AdmissionID = doctorsNote.AdmissionID;

                var UpdatedDoctorsNote = unitOfWork.DoctorsNoteRepository.Update(doctorNoteInDb);
                await unitOfWork.SaveChangesAsync();

                var doctorNoteToReturn = await unitOfWork.DoctorsNoteRepository.GetByIdAsync(UpdatedDoctorsNote.DoctorsNoteID);

                return Ok(doctorNoteToReturn);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }
    }
}