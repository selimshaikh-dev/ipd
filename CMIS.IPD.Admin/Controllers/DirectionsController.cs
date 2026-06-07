#nullable disable
using IPD.Domain.Entities;
using IPD.Infrastructure.Sql;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IPD.Admin.Controllers
{
    public class DirectionsController : Controller
    {
        private readonly DataContext context;

        public DirectionsController(DataContext context)
        {
            this.context = context;
        }

        // GET: Directions
        public async Task<IActionResult> Index()
        {
            return View(await context.Directions.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDirection()
        {
            return Json(await context.Directions.ToListAsync());
        }

        // GET: Directions/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var direction = await context.Directions
                .FirstOrDefaultAsync(m => m.DirectionID == id);
            if (direction == null)
            {
                return NotFound();
            }

            return View(direction);
        }

        // GET: Directions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Directions/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Direction direction)
        {
            var IsExist = IsDirectionDuplicate(direction);
            if (!IsExist)
            {
                if (ModelState.IsValid)
                {
                    direction.IsRowDeleted = false;
                    context.Add(direction);
                    await context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(direction);
            }
            else
            {
                ModelState.AddModelError("DirectionDetails", "Duplicate Found!");
                return BadRequest("Duplicate Found!");
            }
        }

        // GET: Directions/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var direction = await context.Directions.FindAsync(id);
            if (direction == null)
            {
                return NotFound();
            }
            return View(direction);
        }

        // POST: Directions/Edit/5
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("DirectionID,DirectionDetails,FacilityCode,DateCreated,CreatedBy,DateModified,ModifiedBy,IsRowDeleted")] Direction direction)
        {
            var IsExist = IsDirectionDuplicate(direction);
            if (!IsExist)
            {
                if (id != direction.DirectionID)
                {
                    return NotFound();
                }
                if (ModelState.IsValid)
                {
                    try
                    {
                        direction.IsRowDeleted = false;
                        context.Update(direction);
                        await context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!DirectionExists(direction.DirectionID))
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
                return View(direction);
            }
            else
            {
                ModelState.AddModelError("DirectionDetails", "Duplicate Found!");
                //return BadRequest("Duplicate Found!");
                return View(direction);
            }

        }

        // GET: Directions/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var direction = await context.Directions
                .FirstOrDefaultAsync(m => m.DirectionID == id);
            if (direction == null)
            {
                return NotFound();
            }

            return View(direction);
        }

        // POST: Directions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var direction = await context.Directions.FindAsync(id);
            context.Directions.Remove(direction);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DirectionExists(Guid id)
        {
            return context.Directions.Any(e => e.DirectionID == id);
        }

        /// <summary>
        /// Logic for duplicate check
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public bool IsDirectionDuplicate(Direction direction)
        {
            try
            {
                var dirInDb = context.Directions.FirstOrDefault(c => c.DirectionDetails.ToLower().Replace(" ", "-") == direction.DirectionDetails.ToLower().Replace(" ", "-"));

                if (dirInDb != null)
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