#nullable disable
using IPD.Domain.Entities;
using IPD.Infrastructure.Sql;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IPD.Admin.Controllers
{
    public class SurgicalProceduresController : Controller
    {
        private readonly DataContext context;

        public SurgicalProceduresController(DataContext context)
        {
            this.context = context;
        }

        // GET: SurgicalProcedures
        public async Task<IActionResult> Index()
        {
            return View(await context.SurgicalProcedures.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSurgicalProcedure()
        {
            return Json(await context.SurgicalProcedures.ToListAsync());
        }

        // GET: SurgicalProcedures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var surgicalProcedure = await context.SurgicalProcedures
                .FirstOrDefaultAsync(m => m.SurgicalProcedureID == id);
            if (surgicalProcedure == null)
            {
                return NotFound();
            }

            return View(surgicalProcedure);
        }

        // GET: SurgicalProcedures/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SurgicalProcedureID,ProcedureName,FacilityCode,DateCreated,CreatedBy,DateModified,ModifiedBy,IsRowDeleted")] SurgicalProcedure surgicalProcedure)
        {
            var IsExist = IsSurgicalProcedureDuplicate(surgicalProcedure);
            if (!IsExist)
            {
                if (ModelState.IsValid)
                {
                    surgicalProcedure.IsRowDeleted = false;
                    context.Add(surgicalProcedure);
                    await context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(surgicalProcedure);
            }
            else
            {
                ModelState.AddModelError("ProcedureName", "Duplicate Found!");
                return BadRequest("Duplicate Found!");
            }
        }

        // GET: SurgicalProcedures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var surgicalProcedure = await context.SurgicalProcedures.FindAsync(id);
            if (surgicalProcedure == null)
            {
                return NotFound();
            }
            return View(surgicalProcedure);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SurgicalProcedureID,ProcedureName,FacilityCode,DateCreated,CreatedBy,DateModified,ModifiedBy,IsRowDeleted")] SurgicalProcedure surgicalProcedure)
        {
            var IsExist = IsSurgicalProcedureDuplicate(surgicalProcedure);
            if (!IsExist)
            {
                if (id != surgicalProcedure.SurgicalProcedureID)
                {
                    return NotFound();
                }
                if (ModelState.IsValid)
                {
                    try
                    {
                        surgicalProcedure.IsRowDeleted = false;
                        context.Update(surgicalProcedure);
                        await context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!SurgicalProcedureExists(surgicalProcedure.SurgicalProcedureID))
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
                return View(surgicalProcedure);
            }
            else
            {
                return View(surgicalProcedure);
            }
        }

        // GET: SurgicalProcedures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var surgicalProcedure = await context.SurgicalProcedures
                .FirstOrDefaultAsync(m => m.SurgicalProcedureID == id);
            if (surgicalProcedure == null)
            {
                return NotFound();
            }

            return View(surgicalProcedure);
        }

        // POST: SurgicalProcedures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var surgicalProcedure = await context.SurgicalProcedures.FindAsync(id);
            context.SurgicalProcedures.Remove(surgicalProcedure);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SurgicalProcedureExists(int id)
        {
            return context.SurgicalProcedures.Any(e => e.SurgicalProcedureID == id);
        }

        /// <summary>
        /// Logic for duplicate check
        /// </summary>
        /// <param name="surgicalProcedure"></param>
        /// <returns></returns>
        public bool IsSurgicalProcedureDuplicate(SurgicalProcedure surgicalProcedure)
        {
            try
            {
                var SurInDb = context.SurgicalProcedures.FirstOrDefault(c => c.ProcedureName.ToLower().Replace(" ", "-") == surgicalProcedure.ProcedureName.ToLower().Replace(" ", "-"));

                if (SurInDb != null)
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
