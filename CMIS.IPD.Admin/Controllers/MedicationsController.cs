#nullable disable
using IPD.Domain.Entities;
using IPD.Infrastructure.Sql;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IPD.Admin.Controllers
{
    public class MedicationsController : Controller
    {
        private readonly DataContext context;

        public MedicationsController(DataContext context)
        {
            this.context = context;
        }

        // GET: Medications
        public async Task<IActionResult> Index()
        {
            return View(await context.Medications.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMedication()
        {
            return Json(await context.Medications.ToListAsync());
        }

        // GET: Medications/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medication = await context.Medications
                .FirstOrDefaultAsync(m => m.MedicationID == id);
            if (medication == null)
            {
                return NotFound();
            }

            return View(medication);
        }

        // GET: Medications/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Medications/Create
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Medication medication)
        {
            var IsExist = IsMedicationDuplicate(medication);
            if (!IsExist)
            {
                if (ModelState.IsValid)
                {
                    medication.IsRowDeleted = false;
                    context.Add(medication);
                    await context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(medication);
            }
            else
            {
                ModelState.AddModelError("MedicationName", "Duplicate Found!");
                return BadRequest("Duplicate Found!");
            }
        }

        // GET: Medications/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medication = await context.Medications.FindAsync(id);
            if (medication == null)
            {
                return NotFound();
            }
            return View(medication);
        }

        // POST: Medications/Edit/5
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("MedicationID,MedicationName,FacilityCode,DateCreated,CreatedBy,DateModified,ModifiedBy,IsRowDeleted")] Medication medication)
        {
            var IsExist = IsMedicationDuplicate(medication);
            if (!IsExist)
            {
                if (id != medication.MedicationID)
                {
                    return NotFound();
                }
                if (ModelState.IsValid)
                {
                    try
                    {  
                        medication.IsRowDeleted = false;
                        context.Update(medication);
                        await context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!MedicationExists(medication.MedicationID))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                return View(medication);
            }
            else
            {
                ModelState.AddModelError("MedicationName", "Duplicate Found!");
                return View();
            }
        }

        // GET: Medications/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medication = await context.Medications
                .FirstOrDefaultAsync(m => m.MedicationID == id);
            if (medication == null)
            {
                return NotFound();
            }

            return View(medication);
        }

        // POST: Medications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var medication = await context.Medications.FindAsync(id);
            context.Medications.Remove(medication);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedicationExists(Guid id)
        {
            return context.Medications.Any(e => e.MedicationID == id);
        }

        /// <summary>
        /// Logic for duplicate check
        /// </summary>
        /// <param name="medication"></param>
        /// <returns></returns>
        public bool IsMedicationDuplicate(Medication medication)
        {
            try
            {
                var medInDb = context.Medications.FirstOrDefault(c => c.MedicationName.ToLower().Replace(" ", "-") == medication.MedicationName.ToLower().Replace(" ", "-"));

                if (medInDb != null)
                {
                    return true;
                }

                return false;
            }
            catch
            {
                throw;
            }
        }
    }
}
