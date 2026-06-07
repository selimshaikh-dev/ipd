#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IPD.Domain.Entities;
using IPD.Infrastructure.Sql;

namespace IPD.Admin.Controllers
{
    public class RegionsController : Controller
    {
        private readonly DataContext context;

        public RegionsController(DataContext context)
        {
            this.context = context;
        }

        // GET: Regions
        public async Task<IActionResult> Index()
        {
            return View(await context.Regions.ToListAsync());
        } 

        [HttpGet]
        public async Task<IActionResult> GetAllRegion()
        {
            return Json(await context.Regions.ToListAsync());
        }

        // GET: Regions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var region = await context.Regions
                .FirstOrDefaultAsync(m => m.RegionID == id);
            if (region == null)
            {
                return NotFound();
            }

            return View(region);
        }

        // GET: Regions/Create
        public IActionResult Create()
        {
            return View();
        }

  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Region region)
        {
            var IsExist = IsRegionDuplicate(region);
            if (!IsExist)
            {
                if (ModelState.IsValid)
                {
                    region.IsRowDeleted = false;
                    context.Add(region);
                    await context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(region);
            }
            else
            {
                ModelState.AddModelError("CategoryName", "Duplicate Found!");
                return BadRequest("Duplicate Found!");
            }    
        }

        // GET: Regions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var region = await context.Regions.FindAsync(id);
            if (region == null)
            {
                return NotFound();
            }
            return View(region);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Region region)
        {
            var IsExist = IsRegionDuplicate(region);
            if (!IsExist)
            {
                if (id != region.RegionID)
                {
                    return NotFound();
                }
                if (ModelState.IsValid)
                {
                    try
                    {
                        region.IsRowDeleted = false;
                        context.Update(region);
                        await context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!RegionExists(region.RegionID))
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
                return View(region);
            }
            else
            {
                ModelState.AddModelError("RegionName", "Duplicate Found!");
                return View(region);
            }            
        }

        // GET: Regions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var region = await context.Regions
                .FirstOrDefaultAsync(m => m.RegionID == id);
            if (region == null)
            {
                return NotFound();
            }

            return View(region);
        }

        // POST: Regions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var region = await context.Regions.FindAsync(id);
            context.Regions.Remove(region);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegionExists(int id)
        {
            return context.Regions.Any(e => e.RegionID == id);
        }

        /// <summary>
        /// Logic for duplicate check
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public bool IsRegionDuplicate(Region region)
        {
            try
            {
                var regInDb = context.Regions.FirstOrDefault(c => c.RegionName.ToLower().Replace(" ", "-") == region.RegionName.ToLower().Replace(" ", "-"));

                if (regInDb != null)
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
