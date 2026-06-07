#nullable disable
using IPD.Domain.Entities;
using IPD.Infrastructure.Sql;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IPD.Admin.Controllers
{
    public class IntervalsController : Controller
    {
        private readonly DataContext context;

        public IntervalsController(DataContext context)
        {
            this.context = context;
        }

        // GET: Intervals
        public async Task<IActionResult> Index()
        {
            return View(await context.Intervals.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> GetAllInterval()
        {
            return Json(await context.Intervals.ToListAsync());
        }

        // GET: Intervals/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var interval = await context.Intervals
                .FirstOrDefaultAsync(m => m.IntervalID == id);
            if (interval == null)
            {
                return NotFound();
            }

            return View(interval);
        }

        // GET: Intervals/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Intervals/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Interval interval)
        {
            var IsExist = IsIntervalDuplicate(interval);
            if (!IsExist)
            {
                if (ModelState.IsValid)
                {
                    interval.IsRowDeleted = false;
                    context.Add(interval);
                    await context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(interval);
            }
            else
            {
                ModelState.AddModelError("IntervalName", "Duplicate Found!");
                return BadRequest("Duplicate Found!");
            }
        }

        // GET: Intervals/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var interval = await context.Intervals.FindAsync(id);
            if (interval == null)
            {
                return NotFound();
            }
            return View(interval);
        }

        // POST: Intervals/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("IntervalID,IntervalName,FacilityCode,DateCreated,CreatedBy,DateModified,ModifiedBy,IsRowDeleted")] Interval interval)
        {
            var IsExist = IsIntervalDuplicate(interval);
            if (!IsExist)
            {
                if (id != interval.IntervalID)
                {
                    return NotFound();
                }
                if (ModelState.IsValid)
                {
                    try
                    {   
                        interval.IsRowDeleted = false;
                        context.Update(interval);
                        await context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!IntervalExists(interval.IntervalID))
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
                return View(interval);
            }
            else
            {
                ModelState.AddModelError("IntervalName", "Duplicate Found!");
                //return BadRequest("Duplicate Found!");
                return View(interval);
            }
        }

        // GET: Intervals/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var interval = await context.Intervals
                .FirstOrDefaultAsync(m => m.IntervalID == id);
            if (interval == null)
            {
                return NotFound();
            }

            return View(interval);
        }

        // POST: Intervals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var interval = await context.Intervals.FindAsync(id);
            context.Intervals.Remove(interval);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IntervalExists(Guid id)
        {
            return context.Intervals.Any(e => e.IntervalID == id);
        }

        /// <summary>
        /// Logic for duplicate check
        /// </summary>
        /// <param name="interval"></param>
        /// <returns></returns>
        public bool IsIntervalDuplicate(Interval interval)
        {
            try
            {
                var intInDb = context.Intervals.FirstOrDefault(i => i.IntervalName.ToLower().Replace(" ", "-") == interval.IntervalName.ToLower().Replace(" ", "-"));

                if (intInDb != null)
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
