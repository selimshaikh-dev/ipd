#nullable disable
using IPD.Domain.Entities;
using IPD.Infrastructure.Sql;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IPD.Admin.Controllers
{
    public class CountriesController : Controller
    {
        private readonly DataContext context;

        public CountriesController(DataContext context)
        {
            this.context = context;
        }

        // GET: Countries
        public async Task<IActionResult> Index()
        {
            return View(await context.Countries.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCountry()
        {
            return Json(await context.Countries.ToListAsync());
        }

        // GET: Countries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await context.Countries
                .FirstOrDefaultAsync(m => m.CountryID == id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // GET: Countries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Countries/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Country country)
        {
            var IsExist = IsCountryDuplicate(country);
            if (!IsExist)
            {
                if (ModelState.IsValid)
                {
                    country.IsRowDeleted = false;
                    context.Add(country);
                    await context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(country);
            }
            else
            {
                ModelState.AddModelError("Name", "Duplicate Found!");
                return BadRequest("Duplicate Found!");
            }

        }

        // GET: Countries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await context.Countries.FindAsync(id);

            if (country == null)
            {
                return NotFound();
            }
            return View(country);
        }

        // POST: Countries/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CountryID,Name,FacilityCode,DateCreated,CreatedBy,DateModified,ModifiedBy,IsRowDeleted")] Country country)
        {
            var IsExist = IsCountryDuplicate(country);
            if (!IsExist)
            {
                if (id != country.CountryID)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        country.IsRowDeleted = false;
                        context.Update(country);
                        await context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!CountryExists(country.CountryID))
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
                return View(country);
            }
            else
            {
                ModelState.AddModelError("Name", "Duplicate Found!");
                return View();
            }
        }

        // GET: Countries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await context.Countries
                .FirstOrDefaultAsync(m => m.CountryID == id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // POST: Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var country = await context.Countries.FindAsync(id);
            context.Countries.Remove(country);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CountryExists(int id)
        {
            return context.Countries.Any(e => e.CountryID == id);
        }

        /// <summary>
        /// Logic for duplicate check
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        public bool IsCountryDuplicate(Country country)
        {
            try
            {
                var counInDb = context.Countries.FirstOrDefault(c => c.Name.ToLower().Replace(" ", "-") == country.Name.ToLower().Replace(" ", "-"));

                if (counInDb != null)
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
