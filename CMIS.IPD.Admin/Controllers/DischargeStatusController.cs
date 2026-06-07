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
    public class DischargeStatusController : Controller
    {
        private readonly DataContext context;

        public DischargeStatusController(DataContext context)
        {
            this.context = context;
        }

        // GET: DischargeStatus
        public async Task<IActionResult> Index()
        {
              return View(await context.DischargeStatuses.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDischargeStatus()
        {
            return Json(await context.DischargeStatuses.ToListAsync());
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || context.DischargeStatuses == null)
            {
                return NotFound();
            }

            var dischargeStatus = await context.DischargeStatuses
                .FirstOrDefaultAsync(m => m.DischargeStatusID == id);
            if (dischargeStatus == null)
            {
                return NotFound();
            }

            return View(dischargeStatus);
        }

        // GET: DischargeStatus/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DischargeStatus dischargeStatus)
        {
            var IsExist = IsDischargeStatusDuplicate(dischargeStatus);
            if (!IsExist)
            {
                if (ModelState.IsValid)
                {
                    dischargeStatus.IsRowDeleted = false;
                    //dischargeStatus.DischargeStatusID = Guid.NewGuid();
                    context.Add(dischargeStatus);
                    await context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(dischargeStatus);
            }
            else
            {
                ModelState.AddModelError("DischargeStatus", "Duplicate Found!");
                return BadRequest("Duplicate Found!");
            }
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dischargeStatus = await context.DischargeStatuses.FindAsync(id);
            if (dischargeStatus == null)
            {
                return NotFound();
            }
            return View(dischargeStatus);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, DischargeStatus dischargeStatus)
        {
            var IsExist = IsDischargeStatusDuplicate(dischargeStatus);
            if (!IsExist)
            {
                if (id != dischargeStatus.DischargeStatusID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    dischargeStatus.IsRowDeleted = false;
                    context.Update(dischargeStatus);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DischargeStatusExists(dischargeStatus.DischargeStatusID))
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
            return View(dischargeStatus);
            }
            else
            {
                ModelState.AddModelError("DischargesStatus", "Duplicate Found!");
                return View(dischargeStatus);
            }
        }      

        // GET: DischargeStatus/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || context.DischargeStatuses == null)
            {
                return NotFound();
            }

            var dischargeStatus = await context.DischargeStatuses
                .FirstOrDefaultAsync(m => m.DischargeStatusID == id);
            if (dischargeStatus == null)
            {
                return NotFound();
            }

            return View(dischargeStatus);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (context.DischargeStatuses == null)
            {
                return Problem("Entity set 'DataContext.DischargeStatuses'  is null.");
            }
            var dischargeStatus = await context.DischargeStatuses.FindAsync(id);
            if (dischargeStatus != null)
            {
                context.DischargeStatuses.Remove(dischargeStatus);
            }
            
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DischargeStatusExists(Guid id)
        {
            return context.Discharges.Any(e => e.DischargeID == id);
        }

        /// <summary>
        /// Logic for duplicate check
        /// </summary>
        /// <param name="dischargeStatus"></param>
        /// <returns></returns>
        public bool IsDischargeStatusDuplicate(DischargeStatus dischargeStatus)
        {
            try
            {
                var DischargeStatusInDb = context.DischargeStatuses.FirstOrDefault(c => c.DischargesStatus.ToLower().Replace(" ", "-") == dischargeStatus.DischargesStatus.ToLower().Replace(" ", "-"));

                if (DischargeStatusInDb != null)
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
