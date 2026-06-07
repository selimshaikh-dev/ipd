using IPD.Domain.Dto;
using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IPD.Api.Controllers
{
    /// <summary>
    /// Medicines controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MedicinesController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<MedicinesController> logger;

        /// <summary>
        /// Medicines constructor
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="logger"></param>
        public MedicinesController(IUnitOfWork unitOfWork, ILogger<MedicinesController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        /// <summary>
        /// Add new medicines.
        /// </summary>
        /// <param name="medicines"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddMedicines([FromBody] MedicineCreateDto medicines)
        {
            try
            {
                var medicinesList = medicines.Data?.Select(x => new Medicine()
                {
                    MedicinesName = Convert.ToString(x[1]),
                    MedicinesTime = Convert.ToInt64(x[0]),
                    PartographID = medicines.PartographID,
                }).ToList() ?? new List<Medicine>();

                foreach (var item in medicinesList)
                {
                    unitOfWork.MedicinesRepository.UpdateMedicine(item);
                }

                await unitOfWork.SaveChangesAsync();

                return Ok(medicinesList);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        /// <summary>
        ///  Load medicines  of a patient in specific admission.
        /// </summary>
        /// <param name="partographId"></param>
        /// <returns></returns>
        //[HttpGet]
        //[Route("[action]/{partographId}")]
        //public async Task<IActionResult> LoadMedicines(Guid partographId)
        //{

        //    try
        //    {
        //        var medicineInDb = await unitOfWork.MedicinesRepository
        //            .GetAll()
        //            .Where(l => l.PartographID == partographId && l.IsRowDeleted.Equals(false))
        //            .OrderByDescending(o => o.DateCreated)
        //            .ToListAsync();

        //        var data = medicineInDb.Select(x => new long[]
        //        {
        //                x.MedicinesTime,
        //                x.MedicinesName
        //        })
        //        .OrderBy(i => i[0])
        //        .ToList();
        //        var liquors = new LiquorCreateDto();
        //        if (data.Count > 0)
        //        {
        //            liquors = new LiquorCreateDto()
        //            {
        //                PartographID = partographId,
        //                Data = data
        //            };
        //        }

        //        return Ok(liquors);
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogError(ex.Message);
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
        //    }
        //}
    

        /// <summary>
        ///  Find medicines post by key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        //[HttpGet]
        //[Route("[action]/{key}")]
        //public async Task<IActionResult> FindMedicinesByKey(Guid key)
        //{
        //    try
        //    {
        //        if (key == Guid.Empty)
        //            return BadRequest("Invalid key!");

        //        var medicinesInDb = await unitOfWork.MedicinesRepository.GetByIdAsync(key);

        //        if (medicinesInDb == null)
        //            return NotFound();

        //        return Ok(medicinesInDb);
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogError(ex.Message);
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
        //    }
        //}

        /// <summary>
        /// Updates existing medicines.
        /// </summary>
        /// <param name="medicines"></param>
        /// <returns></returns>
        //[HttpPut]
        //[Route("[action]")]
        //public async Task<IActionResult> EditMedicines([FromBody] MedicinesDTO medicines)
        //{
        //    try
        //    {
        //        if (medicines.MedicinesID == Guid.Empty)
        //            return BadRequest();

        //        var medicinesInDb = await unitOfWork.MedicinesRepository.GetByIdAsync(medicines.MedicinesID);
        //        if (medicinesInDb == null)
        //            return NotFound();

        //        medicinesInDb.MedicinesName = medicines.MedicinesName;
        //        //medicinesInDb.Time = medicines.Time;
        //        medicinesInDb.PartographID = medicines.PartographID;

        //        var updatedmedicines = unitOfWork.MedicinesRepository.Update(medicinesInDb);
        //        await unitOfWork.SaveChangesAsync();

        //        var medicinesToReturn = await unitOfWork.MedicinesRepository.GetByIdAsync(updatedmedicines.MedicinesID);

        //        return Ok(medicinesToReturn);
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogError(ex.Message);
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
        //    }
        //}
    }
}