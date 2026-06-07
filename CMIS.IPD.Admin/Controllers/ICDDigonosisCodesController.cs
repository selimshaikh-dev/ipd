#nullable disable
using IPD.Domain.Entities;
using IPD.Infrastructure.Sql;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace IPD.Admin.Controllers
{
    public class ICDDigonosisCodesController : Controller
    {
        private readonly DataContext context;

        public ICDDigonosisCodesController(DataContext context)
        {
            this.context = context;
        }

        // GET: ICDDigonosisCodes
        public async Task<IActionResult> Index()
        {
            return View(await context.ICDDigonosisCodes.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> GetAllICDDigonosisCode()
        {
            return Json(await context.ICDDigonosisCodes.ToListAsync());
        }

        // GET: ICDDigonosisCodes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var iCDDigonosisCode = await context.ICDDigonosisCodes
                .FirstOrDefaultAsync(m => m.DiseaseID == id);
            if (iCDDigonosisCode == null)
            {
                return NotFound();
            }

            return View(iCDDigonosisCode);
        }

        // GET: ICDDigonosisCodes/Create
        public IActionResult Create()
        {
            ViewBag.Categories = GetAllDisease();
            return View();
        }

        private IEnumerable<SelectListItem> GetAllDisease()
        {
            var categories = context.ICDDigonosisCodes
                .Select(c => new SelectListItem
                {
                    Value = c.DiseaseID.ToString(),
                    Text = c.Description
                });

            return categories;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DiseaseID,ICDCode,Description,ParentsID,FacilityCode,DateCreated,CreatedBy,DateModified,ModifiedBy,IsRowDeleted")] ICDDigonosisCode iCDDigonosisCode)
        {
            var IsExist = IsICDDigonosisCodeDuplicate(iCDDigonosisCode);
            if (!IsExist)
            {
                if (ModelState.IsValid)
                {
                    iCDDigonosisCode.IsRowDeleted = false;
                    context.Add(iCDDigonosisCode);
                    await context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(iCDDigonosisCode);
            }
            else
            {
                ModelState.AddModelError("Description", "Duplicate Found!");
                return BadRequest("Duplicate Found!");
            }
        }

        // GET: ICDDigonosisCodes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var iCDDigonosisCode = await context.ICDDigonosisCodes.FindAsync(id);
            if (iCDDigonosisCode == null)
            {
                return NotFound();
            }
            ViewBag.Categories = GetAllDisease();
            return View(iCDDigonosisCode);
        }
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DiseaseID,ICDCode,Description,ParentsID,FacilityCode,DateCreated,CreatedBy,DateModified,ModifiedBy,IsRowDeleted")] ICDDigonosisCode iCDDigonosisCode)
        {
            var IsExist = IsICDDigonosisCodeDuplicate(iCDDigonosisCode);
            if (!IsExist)
            {
                if (id != iCDDigonosisCode.DiseaseID)
                {
                    return NotFound();
                }
                if (ModelState.IsValid)
                {
                    try
                    {
                        iCDDigonosisCode.IsRowDeleted = false;
                        context.Update(iCDDigonosisCode);
                        await context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ICDDigonosisCodeExists(iCDDigonosisCode.DiseaseID))
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
                return View(iCDDigonosisCode);
            }
            else
            {
                ModelState.AddModelError("Description", "Duplicate Found!");
                return View();
            }
        }

        // GET: ICDDigonosisCodes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var iCDDigonosisCode = await context.ICDDigonosisCodes
                .FirstOrDefaultAsync(m => m.DiseaseID == id);
            if (iCDDigonosisCode == null)
            {
                return NotFound();
            }

            return View(iCDDigonosisCode);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var iCDDigonosisCode = await context.ICDDigonosisCodes.FindAsync(id);
            context.ICDDigonosisCodes.Remove(iCDDigonosisCode);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ICDDigonosisCodeExists(int id)
        {
            return context.ICDDigonosisCodes.Any(e => e.DiseaseID == id);
        }

        /// <summary>
        /// Logic for duplicate check
        /// </summary>
        /// <param name="iCDDigonosisCode"></param>
        /// <returns></returns>
        public bool IsICDDigonosisCodeDuplicate(ICDDigonosisCode iCDDigonosisCode)
        {
            try
            {
                var iCDInDb = context.ICDDigonosisCodes.FirstOrDefault(c => c.Description.ToLower().Replace(" ", "-") == iCDDigonosisCode.Description.ToLower().Replace(" ", "-"));

                if (iCDInDb != null)
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