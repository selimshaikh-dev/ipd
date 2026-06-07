#nullable disable
using IPD.Domain.Entities;
using IPD.Infrastructure.Sql;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IPD.Admin.Controllers
{
    public class SurgeryTypesController : Controller
    {
        private readonly DataContext context;

        public SurgeryTypesController(DataContext context)
        {
            this.context = context;
        }

        // GET: SurgeryTypes
        public async Task<IActionResult> Index()
        {
            return View(await context.SurgeryTypes.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSurgeryType()
        {
            return Json(await context.SurgeryTypes.ToListAsync());
        }

        // GET: SurgeryTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var surgeryType = await context.SurgeryTypes
                .FirstOrDefaultAsync(m => m.SurgeryTypeID == id);
            if (surgeryType == null)
            {
                return NotFound();
            }

            return View(surgeryType);
        }

        // GET: SurgeryTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SurgeryTypeID,TypeName,FacilityCode,DateCreated,CreatedBy,DateModified,ModifiedBy,IsRowDeleted")] SurgeryType surgeryType)
        {
            var IsExist = IsSurgeryTypeDuplicate( surgeryType);
            if (!IsExist)
            {
                if (ModelState.IsValid)
                {
                    surgeryType.IsRowDeleted = false;
                    context.Add(surgeryType);
                    await context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(surgeryType);
            }
            else
            {
                ModelState.AddModelError("TypeName", "Duplicate Found!");
                return BadRequest("Duplicate Found!");
            }

        }

        // GET: SurgeryTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var surgeryType = await context.SurgeryTypes.FindAsync(id);
            if (surgeryType == null)
            {
                return NotFound();
            }
            return View(surgeryType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SurgeryTypeID,TypeName,FacilityCode,DateCreated,CreatedBy,DateModified,ModifiedBy,IsRowDeleted")] SurgeryType surgeryType)
        {
            var IsExist = IsSurgeryTypeDuplicate(surgeryType);
            if (!IsExist)
            {
                if (id != surgeryType.SurgeryTypeID)
                {
                    return NotFound();
                }
                if (ModelState.IsValid)
                {
                    try
                    {
                        surgeryType.IsRowDeleted = false;
                        context.Update(surgeryType);
                        await context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!SurgeryTypeExists(surgeryType.SurgeryTypeID))
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
                return View(surgeryType);
            }
            else
            {
                ModelState.AddModelError("TypeName", "Duplicate Found!");
                return View();
            }
        }

        // GET: SurgeryTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var surgeryType = await context.SurgeryTypes
                .FirstOrDefaultAsync(m => m.SurgeryTypeID == id);
            if (surgeryType == null)
            {
                return NotFound();
            }

            return View(surgeryType);
        }

        // POST: SurgeryTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var surgeryType = await context.SurgeryTypes.FindAsync(id);
            context.SurgeryTypes.Remove(surgeryType);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SurgeryTypeExists(int id)
        {
            return context.SurgeryTypes.Any(e => e.SurgeryTypeID == id);
        }

        /// <summary>
        /// Logic for duplicate check
        /// </summary>
        /// <param name="surgeryType"></param>
        /// <returns></returns>
        public bool IsSurgeryTypeDuplicate(SurgeryType surgeryType)
        {
            try
            {
                var SurInDb = context.SurgeryTypes.FirstOrDefault(c => c.TypeName.ToLower().Replace(" ", "-") == surgeryType.TypeName.ToLower().Replace(" ", "-"));

                if (SurInDb != null)
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
