#nullable disable
using IPD.Domain.Entities;
using IPD.Infrastructure.Sql;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IPD.Admin.Controllers
{
    public class ProceduresController : Controller
    {
        private readonly DataContext context;

        public ProceduresController(DataContext context)
        {
            this.context = context;
        }

        // GET: Procedures
        public async Task<IActionResult> Index()
        {
            return View(await context.Procedure.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProcedure()
        {
            return Json(await context.Procedure.ToListAsync());
        }

        // GET: Procedures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var procedure = await context.Procedure
                .FirstOrDefaultAsync(m => m.ProcedureID == id);
            if (procedure == null)
            {
                return NotFound();
            }

            return View(procedure);
        }

        // GET: Procedures/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProcedureID,ProcedureName,FacilityCode,DateCreated,CreatedBy,DateModified,ModifiedBy,IsRowDeleted")] Procedure procedure)
        {
            var IsExist = IsProcedureDuplicate(procedure);
            if (!IsExist)
            {
                if (ModelState.IsValid)
                {
                    procedure.IsRowDeleted = false;
                    context.Add(procedure);
                    await context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(procedure);
            }
            else
            {
                ModelState.AddModelError("ProcedureName", "Duplicate Found!");
                return BadRequest("Duplicate Found!");
            }
        }

        // GET: Procedures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var procedure = await context.Procedure.FindAsync(id);
            if (procedure == null)
            {
                return NotFound();
            }
            return View(procedure);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProcedureID,ProcedureName,FacilityCode,DateCreated,CreatedBy,DateModified,ModifiedBy,IsRowDeleted")] Procedure procedure)
        {
            var IsExist = IsProcedureDuplicate(procedure);
            if (!IsExist)
            {
                if (id != procedure.ProcedureID)
                {
                    return NotFound();
                }
                if (ModelState.IsValid)
                {
                    try
                    {
                        procedure.IsRowDeleted = false;
                        context.Update(procedure);
                        await context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ProcedureExists(procedure.ProcedureID))
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
                return View(procedure);
            }
            else
            {
                ModelState.AddModelError("ProcedureName", "Duplicate Found!");
                return View();
            }
        }

        // GET: Procedures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var procedure = await context.Procedure
                .FirstOrDefaultAsync(m => m.ProcedureID == id);
            if (procedure == null)
            {
                return NotFound();
            }

            return View(procedure);
        }

        // POST: Procedures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var procedure = await context.Procedure.FindAsync(id);
            context.Procedure.Remove(procedure);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProcedureExists(int id)
        {
            return context.Procedure.Any(e => e.ProcedureID == id);
        }

        /// <summary>
        /// Logic for duplicate check
        /// </summary>
        /// <param name="procedure"></param>
        /// <returns></returns>
        public bool IsProcedureDuplicate(Procedure procedure)
        {
            try
            {
                var proInDb = context.Procedure.FirstOrDefault(c => c.ProcedureName.ToLower().Replace(" ", "-") == procedure.ProcedureName.ToLower().Replace(" ", "-"));

                if (proInDb != null)
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
