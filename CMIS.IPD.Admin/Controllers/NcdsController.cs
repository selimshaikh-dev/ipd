#nullable disable
using IPD.Domain.Entities;
using IPD.Infrastructure.Sql;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IPD.Admin.Controllers
{
    public class NcdsController : Controller
    {
        private readonly DataContext context;

        public NcdsController(DataContext context)
        {
            this.context = context;
        }

        // GET: Ncds
        public async Task<IActionResult> Index()
        {
            return View(await context.Ncds.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNcd()
        {
            return Json(await context.Ncds.ToListAsync());
        }

        // GET: Ncds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ncds = await context.Ncds
                .FirstOrDefaultAsync(m => m.NcdsID == id);
            if (ncds == null)
            {
                return NotFound();
            }

            return View(ncds);
        }

        // GET: Ncds/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ncds/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Ncd ncds)
        {
            var IsExist = IsNcdsDuplicate(ncds);
            if (!IsExist)
            {
                if (ModelState.IsValid)
                {
                    ncds.IsRowDeleted = false;
                    context.Add(ncds);
                    await context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(ncds);
            }
            else
            {
                ModelState.AddModelError("NcdName", "Duplicate Found!");
                return BadRequest("Duplicate Found!");
            }
        }

        // GET: Ncds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ncds = await context.Ncds.FindAsync(id);
            if (ncds == null)
            {
                return NotFound();
            }
            return View(ncds);
        }

        // POST: Ncds/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NcdsID,NcdName,FacilityCode,DateCreated,CreatedBy,DateModified,ModifiedBy,IsRowDeleted")] Ncd ncds)
        {
            var IsExist = IsNcdsDuplicate(ncds);
            if (!IsExist)
            {
                if (id != ncds.NcdsID)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        ncds.IsRowDeleted = false;
                        context.Update(ncds);
                        await context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!NcdsExists(ncds.NcdsID))
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
                return View(ncds);
            }
            else
            {
                ModelState.AddModelError("NcdName", "Duplicate Found!");
                return View();
            }
        }

        // GET: Ncds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ncds = await context.Ncds
                .FirstOrDefaultAsync(m => m.NcdsID == id);
            if (ncds == null)
            {
                return NotFound();
            }

            return View(ncds);
        }

        // POST: Ncds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ncds = await context.Ncds.FindAsync(id);
            context.Ncds.Remove(ncds);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NcdsExists(int id)
        {
            return context.Ncds.Any(e => e.NcdsID == id);
        }

        /// <summary>
        /// Logic for duplicate check
        /// </summary>
        /// <param name="ncds"></param>
        /// <returns></returns>
        public bool IsNcdsDuplicate(Ncd ncds)
        {
            try
            {
                var ncdInDb = context.Ncds.FirstOrDefault(c => c.NcdName.ToLower().Replace(" ", "-") == ncds.NcdName.ToLower().Replace(" ", "-"));

                if (ncdInDb != null)
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
