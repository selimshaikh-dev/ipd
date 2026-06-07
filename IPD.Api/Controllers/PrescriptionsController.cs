using IPD.Domain.Dto;
using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IPD.Api.Controllers
{
    /// <summary>
    /// PrescriptionsController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionsController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<PrescriptionsController> logger;

        /// <summary>
        /// constructor for PrescriptionsController
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="logger"></param>
        public PrescriptionsController(IUnitOfWork unitOfWork, ILogger<PrescriptionsController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        /// <summary>
        /// Saves a Prescriptions.
        /// </summary>
        /// <param name="prescriptions"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddPrescriptions([FromHeader(Name = "x-facility-code")] string facilityCode, [FromBody] PrescriptionsDto prescriptions)
        {
            try
            {
                var medicationPlans = prescriptions.MedicationPlans.Select(item => new MedicationPlan
                {
                    MedicationPlanID = item.MedicationPlanID,
                    Dose = item.Dose,
                    Durations = item.Durations,
                    IntervalsID = item.IntervalsID,
                    MedicationsID = item.MedicationsID,
                    DirectionsID = item.DirectionsID,
                    FacilityCode = facilityCode
                }).ToList();

                var prescriptionsInDb = new Prescription
                {
                    PrescriptionsID = prescriptions.PrescriptionsID,
                    AdmissionID = prescriptions.AdmissionID,
                    DoctorName = prescriptions.DoctorName,
                    PrescriptionsDate = prescriptions.PrescriptionsDate,
                    DateCreated = DateTime.Now,
                    MedicationPlans = medicationPlans,
                    FacilityCode = facilityCode
                };

                var prescriptionsAdded = unitOfWork.PrescriptionsRepository.Add(prescriptionsInDb);
                await unitOfWork.SaveChangesAsync();

                var prescriptionsToReturn = await unitOfWork.PrescriptionsRepository.GetByIdAsync(prescriptionsInDb.PrescriptionsID);

                return Ok(prescriptionsToReturn);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        ///  Load cervix  of a patient in specific admission.
        /// </summary>
        /// <param name="prescriptionId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{prescriptionId}")]
        public async Task<IActionResult> LoadPrescriptions(Guid prescriptionId)
        {
            try
            {
                var prescriptionsInDb = await unitOfWork.PrescriptionsRepository
                    .GetAll()
                    .FirstOrDefaultAsync(p => p.PrescriptionsID == prescriptionId && p.IsRowDeleted.Equals(false));
                if (prescriptionsInDb == null)
                {
                    prescriptionsInDb = new Prescription();
                }

                var directionDetails = unitOfWork.DirectionRepository.GetAll().ToList();
                var intervalsDetails = unitOfWork.IntervalRepository.GetAll().ToList();
                var medicationsDetails = unitOfWork.MedicationRepository.GetAll().ToList();

                var medicationPlanList = await unitOfWork.MedicationPlanRepository
                    .GetAll()
                    .Where(i => i.PrescriptionsID == prescriptionId)
                    .ToListAsync();
                var medicationPlans = new List<MedicationPlanDetailsDto>();
                foreach (var i in medicationPlanList)
                {
                    var directions = directionDetails?.FirstOrDefault(j => j.DirectionID == i.DirectionsID)?.DirectionDetails ?? string.Empty;
                    var intervals = intervalsDetails?.FirstOrDefault(j => j.IntervalID == i.IntervalsID)?.IntervalName ?? string.Empty;
                    var medications = medicationsDetails?.FirstOrDefault(j => j.MedicationID == i.MedicationsID)?.MedicationName ?? string.Empty;
                    var medicationPlan = new MedicationPlanDetailsDto()
                    {
                        MedicationPlanID = i.MedicationPlanID,
                        Dose = i.Dose,
                        Durations = i.Durations,
                        DirectionsID = i.DirectionsID,
                        IntervalsID = i.IntervalsID,
                        MedicationsID = i.MedicationsID,
                        Directions = directions,
                        Intervals = intervals,
                        Medications = medications,
                    };
                    medicationPlans.Add(medicationPlan);
                }

                var prescrition = new PrescriptionDetailsDto()
                {
                    PrescriptionsID = prescriptionsInDb.PrescriptionsID,
                    AdmissionID = prescriptionsInDb.AdmissionID,
                    PrescriptionsDate = prescriptionsInDb.PrescriptionsDate,
                    DoctorName = prescriptionsInDb.DoctorName,
                    MedicationPlans = medicationPlans
                };
                return Ok(prescrition);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        ///  Load cervix  of a patient in specific admission.
        /// </summary>
        /// <param name="addmissionId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{addmissionId}")]
        public async Task<IActionResult> LoadPrescriptionsByAddmissionId(Guid addmissionId)
        {
            try
            {
                var prescriptionsInDb = await unitOfWork.PrescriptionsRepository
                    .GetAll()
                    .Where(p => p.AdmissionID == addmissionId && p.IsRowDeleted.Equals(false))
                    .OrderByDescending(p => p.PrescriptionsDate)
                    .ToListAsync();
                if (prescriptionsInDb == null)
                {
                    prescriptionsInDb = new List<Prescription>();
                }

                var directionDetails = unitOfWork.DirectionRepository.GetAll().ToList();
                var intervalsDetails = unitOfWork.IntervalRepository.GetAll().ToList();
                var medicationsDetails = unitOfWork.MedicationRepository.GetAll().ToList();
                var prescriptionDtoList = new List<PrescriptionDetailsDto>();
                foreach (var item in prescriptionsInDb)
                {
                    var medicationPlanList = await unitOfWork.MedicationPlanRepository
                    .GetAll()
                    .Where(i => i.PrescriptionsID == item.PrescriptionsID)
                    .ToListAsync();
                    var medicationPlans = new List<MedicationPlanDetailsDto>();
                    foreach (var i in medicationPlanList)
                    {
                        var directions = directionDetails?.FirstOrDefault(j => j.DirectionID == i.DirectionsID)?.DirectionDetails ?? string.Empty;
                        var intervals = intervalsDetails?.FirstOrDefault(j => j.IntervalID == i.IntervalsID)?.IntervalName ?? string.Empty;
                        var medications = medicationsDetails?.FirstOrDefault(j => j.MedicationID == i.MedicationsID)?.MedicationName ?? string.Empty;
                        var medicationPlan = new MedicationPlanDetailsDto()
                        {
                            MedicationPlanID = i.MedicationPlanID,
                            Dose = i.Dose,
                            Durations = i.Durations,
                            Directions = directions,
                            Intervals = intervals,
                            Medications = medications,
                        };
                        medicationPlans.Add(medicationPlan);
                    }

                    var prescrition = new PrescriptionDetailsDto()
                    {
                        PrescriptionsID = item.PrescriptionsID,
                        AdmissionID = item.AdmissionID,
                        PrescriptionsDate = item.PrescriptionsDate,
                        DoctorName = item.DoctorName,
                        MedicationPlans = medicationPlans
                    };
                    prescriptionDtoList.Add(prescrition);
                }

                return Ok(prescriptionDtoList);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        ///  Find Prescription post by key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{key}")]
        public async Task<IActionResult> FindPrescriptionByKey(Guid key)
        {
            try
            {
                if (key == Guid.Empty)
                    return BadRequest("Invalid key!");

                var prescriptionsInDb = await unitOfWork.PrescriptionsRepository.GetByIdAsync(key);

                if (prescriptionsInDb == null)
                    return NotFound();

                return Ok(prescriptionsInDb);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        /// Updates existing prescriptions.
        /// </summary>
        /// <param name="prescriptions"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> EditPrescriptions([FromBody] PrescriptionsDto prescriptions)
        {
            try
            {
                if (prescriptions.PrescriptionsID == Guid.Empty)
                    return BadRequest();

                var prescriptionsInDb = await unitOfWork.PrescriptionsRepository.GetByIdAsync(prescriptions.PrescriptionsID);
                if (prescriptionsInDb == null)
                    return NotFound();

                var medicationPlans = prescriptions.MedicationPlans.Select(item => new MedicationPlan
                {
                    MedicationPlanID = item.MedicationPlanID,
                    Dose = item.Dose,
                    Durations = item.Durations,
                    IntervalsID = item.IntervalsID,
                    MedicationsID = item.MedicationsID,
                    DirectionsID = item.DirectionsID
                }).ToList();

                prescriptionsInDb.PrescriptionsID = prescriptions.PrescriptionsID;
                prescriptionsInDb.AdmissionID = prescriptions.AdmissionID;
                prescriptionsInDb.DoctorName = prescriptions.DoctorName;
                prescriptionsInDb.PrescriptionsDate = prescriptions.PrescriptionsDate;
                prescriptionsInDb.DateCreated = DateTime.Now;
                prescriptionsInDb.MedicationPlans = medicationPlans;

                var updatedprescriptions = unitOfWork.PrescriptionsRepository.Update(prescriptionsInDb);
                await unitOfWork.SaveChangesAsync();

                var prescriptionsToReturn = await unitOfWork.PrescriptionsRepository.GetByIdAsync(updatedprescriptions.PrescriptionsID);

                return Ok(prescriptionsToReturn);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }
    }
}