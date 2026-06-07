#nullable disable
using IPD.Domain.Entities;
using IPD.Infrastructure.Sql;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IPD.Admin.Controllers
{
    public class AllergiesController : Controller
    {
        private readonly DataContext context;

        public AllergiesController(DataContext context)
        {
            this.context = context;
        }

        // GET: Allergies
        public async Task<IActionResult> Index()
        {
            return View(await context.Allergies.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAllergy()
        {
            return Json(await context.Allergies.ToListAsync());
        }

        // GET: Allergies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var allergy = await context.Allergies
                .FirstOrDefaultAsync(m => m.AllergiesID == id);
            if (allergy == null)
            {
                return NotFound();
            }

            return View(allergy);
        }

        // GET: Allergies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Allergies/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Allergy allergy)
        {
            var IsExist = IsAllergiesDuplicate(allergy);
            if (!IsExist)
            {
                if (ModelState.IsValid)
                {
                    allergy.IsRowDeleted = false;
                    context.Add(allergy);
                    await context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(allergy);
            }
            else
            {
                ModelState.AddModelError("AllergiesName", "Duplicate Found!");
                return BadRequest("Duplicate Found!");
            }
        }

        // GET: Allergies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var allergy = await context.Allergies.FindAsync(id);
            if (allergy == null)
            {
                return NotFound();
            }
            return View(allergy);
        }

        // POST: Allergies/Edit/5
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AllergiesID,AllergiesName,FacilityCode,DateCreated,CreatedBy,DateModified,ModifiedBy,IsRowDeleted")] Allergy allergy)
        {
            var IsExist = IsAllergiesDuplicate(allergy);
            if (!IsExist)
            {
                if (id != allergy.AllergiesID)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        allergy.IsRowDeleted = false;   
                        context.Update(allergy);
                        await context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!AllergyExists(allergy.AllergiesID))
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
                return View(allergy);
            }
            else
            {
                ModelState.AddModelError("AllergiesName", "Duplicate Found!");
                return View();
            }
        }

        // GET: Allergies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var allergy = await context.Allergies
                .FirstOrDefaultAsync(m => m.AllergiesID == id);
            if (allergy == null)
            {
                return NotFound();
            }

            return View(allergy);
        }

        // POST: Allergies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var allergy = await context.Allergies.FindAsync(id);
            context.Allergies.Remove(allergy);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AllergyExists(int id)
        {
            return context.Allergies.Any(e => e.AllergiesID == id);
        }

        /// <summary>
        /// Logic for duplicate check
        /// </summary>
        /// <param name="ncds"></param>
        /// <returns></returns>
        public bool IsAllergiesDuplicate(Allergy allergy)
        {
            try
            {
                var allergyInDb = context.Allergies.FirstOrDefault(c => c.AllergiesName.ToLower().Replace(" ", "-") == allergy.AllergiesName.ToLower().Replace(" ", "-"));

                if (allergyInDb != null)
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