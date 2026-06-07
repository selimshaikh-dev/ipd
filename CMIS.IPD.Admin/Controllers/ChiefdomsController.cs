#nullable disable
using IPD.Domain.Entities;
using IPD.Infrastructure.Sql;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace IPD.Admin.Controllers
{
    public class ChiefdomsController : Controller
    {
        private readonly DataContext _context;

        public ChiefdomsController(DataContext context)
        {
            _context = context;
        }

        // GET: Chiefdoms
        public async Task<IActionResult> Index()
        {
            return View(await _context.Chiefdoms.Include(e => e.Tinkhundla).ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> GetAllChiefdoms()
        {
            return Json(await _context.Chiefdoms.ToListAsync());
        }

        // GET: Chiefdoms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chiefdoms = await _context.Chiefdoms
                .Include(c => c.Tinkhundla)
                .FirstOrDefaultAsync(m => m.ChiefdomID == id);
            if (chiefdoms == null)
            {
                return NotFound();
            }

            return View(chiefdoms);
        }

        // GET: Chiefdoms/Create
        public IActionResult Create()
        {
            ViewData["TinkhundlaID"] = new SelectList(_context.Tinkhundla, "TinkhundlaID", "Name");
            return View();
        }

        // POST: Chiefdoms/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Chiefdom chiefdoms)
        {
            var IsExist = IsChiefdomsDuplicate(chiefdoms);
            if (!IsExist)
            {
                if (ModelState.IsValid)
                {
                    chiefdoms.IsRowDeleted = false;

                    _context.Add(chiefdoms);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(chiefdoms);
            }
            else
            {
                ModelState.AddModelError("CategoryName", "Duplicate Found!");
                return BadRequest("Duplicate Found!");
            }
        }

        // GET: Chiefdoms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chiefdoms = await _context.Chiefdoms.FindAsync(id);
            if (chiefdoms == null)
            {
                return NotFound();
            }
            ViewData["TinkhundlaID"] = new SelectList(_context.Tinkhundla, "TinkhundlaID", "Name", chiefdoms.TinkhundlaID);
            return View(chiefdoms);
        }

        // POST: Chiefdoms/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Chiefdom chiefdoms)
        {
            var IsExist = IsChiefdomsDuplicate(chiefdoms);
            if (!IsExist)
            {
                if (id != chiefdoms.ChiefdomID)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        chiefdoms.IsRowDeleted = false;
                        _context.Update(chiefdoms);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ChiefdomsExists(chiefdoms.ChiefdomID))
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
                return View(chiefdoms);
            }
            else
            {
                ModelState.AddModelError("Name", "Duplicate Found!");
                return View();
            }
        }

        // GET: Chiefdoms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chiefdoms = await _context.Chiefdoms
                .Include(c => c.Tinkhundla)
                .FirstOrDefaultAsync(m => m.ChiefdomID == id);
            if (chiefdoms == null)
            {
                return NotFound();
            }

            return View(chiefdoms);
        }

        // POST: Chiefdoms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chiefdoms = await _context.Chiefdoms.FindAsync(id);
            _context.Chiefdoms.Remove(chiefdoms);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChiefdomsExists(int id)
        {
            return _context.Chiefdoms.Any(e => e.ChiefdomID == id);
        }

        /// <summary>
        /// Logic for duplicate check
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public bool IsChiefdomsDuplicate(Chiefdom chiefdoms)
        {
            try
            {
                var chiefdomsInDb = _context.Chiefdoms.FirstOrDefault(c => c.Name.ToLower().Replace(" ", "-") == chiefdoms.Name.ToLower().Replace(" ", "-"));

                if (chiefdomsInDb != null)
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