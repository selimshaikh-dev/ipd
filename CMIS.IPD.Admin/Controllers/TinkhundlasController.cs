#nullable disable
using IPD.Domain.Entities;
using IPD.Infrastructure.Sql;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IPD.Admin.Controllers
{
    public class TinkhundlasController : Controller
    {
        private readonly DataContext _context;

        public TinkhundlasController(DataContext context)
        {
            _context = context;
        }

        // GET: Tinkhundlas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tinkhundla.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTinkhundla()
        {
            return Json(await _context.Tinkhundla.ToListAsync());
        }

        // GET: Tinkhundlas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tinkhundla = await _context.Tinkhundla
                .FirstOrDefaultAsync(m => m.TinkhundlaID == id);
            if (tinkhundla == null)
            {
                return NotFound();
            }

            return View(tinkhundla);
        }

        // GET: Tinkhundlas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tinkhundlas/Create        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Tinkhundla tinkhundla)
        {
            var IsExist = IsTinkhundlaDuplicate(tinkhundla);
            if (!IsExist)
            {
                if (ModelState.IsValid)
                {
                    tinkhundla.IsRowDeleted = false;

                    _context.Add(tinkhundla);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                return View(tinkhundla);
            }
            else
            {
                ModelState.AddModelError("Name", "Duplicate Found!");
                return BadRequest("Duplicate Found!");
            }
        }

        // GET: Tinkhundlas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tinkhundla = await _context.Tinkhundla.FindAsync(id);
            if (tinkhundla == null)
            {
                return NotFound();
            }
            return View(tinkhundla);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Tinkhundla tinkhundla)
        {
            var IsExist = IsTinkhundlaDuplicate(tinkhundla);
            if (!IsExist)
            {
                if (id != tinkhundla.TinkhundlaID)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        tinkhundla.IsRowDeleted = false;
                        _context.Update(tinkhundla);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!TinkhundlaExists(tinkhundla.TinkhundlaID))
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
                return View(tinkhundla);
            }
            else
            {
                ModelState.AddModelError("TinkhundlaName", "Duplicate Found!");
                return View();  
            }
        }

        // GET: Tinkhundlas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tinkhundla = await _context.Tinkhundla
                .FirstOrDefaultAsync(m => m.TinkhundlaID == id);
            if (tinkhundla == null)
            {
                return NotFound();
            }

            return View(tinkhundla);
        }

        // POST: Tinkhundlas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tinkhundla = await _context.Tinkhundla.FindAsync(id);
            _context.Tinkhundla.Remove(tinkhundla);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool TinkhundlaExists(int id)
        {
            return _context.Tinkhundla.Any(e => e.TinkhundlaID == id);
        }

        /// <summary>
        /// Logic for duplicate check
        /// </summary>
        /// <param name="tinkhundla"></param>
        /// <returns></returns>
        public bool IsTinkhundlaDuplicate(Tinkhundla tinkhundla)
        {
            try
            {
                var tinkhundlaInDb = _context.Tinkhundla.FirstOrDefault(c => c.Name.ToLower().Replace(" ", "-") == tinkhundla.Name.ToLower().Replace(" ", "-"));

                if (tinkhundlaInDb != null)
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