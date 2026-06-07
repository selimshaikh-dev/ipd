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
    public class FacilitiesController : Controller
    {
        private readonly DataContext _context;

        public FacilitiesController(DataContext context)
        {
            _context = context;
        }

        // GET: Facilities
        public async Task<IActionResult> Index()
        {            
            return View(await _context.Facilities.Include(e=>e.Region).ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFacilities()
        {
            return Json(await _context.Facilities.ToListAsync());
        }

        // GET: Facilities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facility = await _context.Facilities
                .Include(f => f.Region)
                .FirstOrDefaultAsync(m => m.FacilityID == id);
            if (facility == null)
            {
                return NotFound();
            }

            return View(facility);
        }

        // GET: Facilities/Create
        public IActionResult Create()
        {
            ViewData["RegionID"] = new SelectList(_context.Regions, "RegionID", "RegionName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Facility facility)
        {
            //var IsExist = IsFacilitiesDuplicate(facility);
            //if (!IsExist)
            //{
                if (ModelState.IsValid)
                {
                    facility.IsRowDeleted= false;
                    _context.Add(facility);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ViewData["RegionID"] = new SelectList(_context.Regions, "RegionID", "RegionName", facility.RegionID);
                return View(facility);
            //}
            //else
            //{
            //    ModelState.AddModelError("RegionName", "Duplicate Found!");
            //    return BadRequest("Duplicate Found!");
            //}
        }

        // GET: Facilities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facility = await _context.Facilities.FindAsync(id);
            if (facility == null)
            {
                return NotFound();
            }
            ViewData["RegionID"] = new SelectList(_context.Regions, "RegionID", "RegionName", facility.RegionID);
            return View(facility);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FacilityID,FacilityName,FacilityCode,Longitude,Latitude,RegionID,Telephone,DateCreated,CreatedBy,DateModified,ModifiedBy,IsRowDeleted")] Facility facility)
        {
            //var IsExist = IsFacilitiesDuplicate(facility);
            //if (!IsExist)
            //{
                if (id != facility.FacilityID)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        facility.IsRowDeleted = false;
                        _context.Update(facility);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!FacilityExists(facility.FacilityID))
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
                ViewData["RegionID"] = new SelectList(_context.Regions, "RegionID", "RegionName", facility.RegionID);
                return View(facility);
            //}
            //else
            //{
            //    ModelState.AddModelError("RegionName", "Duplicate Found!");
            //    return BadRequest("Duplicate Found!");
            //}
        }

        // GET: Facilities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facility = await _context.Facilities
                .Include(f => f.Region)
                .FirstOrDefaultAsync(m => m.FacilityID == id);
            if (facility == null)
            {
                return NotFound();
            }

            return View(facility);
        }

        // POST: Facilities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var facility = await _context.Facilities.FindAsync(id);
            _context.Facilities.Remove(facility);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacilityExists(int id)
        {
            return _context.Facilities.Any(e => e.FacilityID == id);
        }

        /// <summary>
        /// Logic for duplicate check
        /// </summary>
        /// <param name="facilities"></param>
        /// <returns></returns>
        //public bool IsFacilitiesDuplicate(Facility facilities)
        //{
        //    try
        //    {
        //        var facilityInDb = _context.Facilities.FirstOrDefault(c => c.FacilityName.ToLower().Replace(" ", "-") == facilities.FacilityName.ToLower().Replace(" ", "-"));

        //        if (facilityInDb != null)
        //        {
        //            return true;
        //        }

        //        return false;
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}
    }
}