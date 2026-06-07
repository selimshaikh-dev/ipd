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
    public class DiagnosisExaminationsController : Controller
    {
        private readonly DataContext context;

        public DiagnosisExaminationsController(DataContext context)
        {
           this.context = context;
        }

        // GET: DiagnosisExaminations
        public async Task<IActionResult> Index()
        {
            return View(await context.DiagonosisExaminations.ToListAsync());
        }
        public async Task<IActionResult> GetAllExamination()
        {
            var data = await context.DiagonosisExaminations.ToListAsync();            
            return Json(data);
        }

        // GET: DiagnosisExaminations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diagnosisExamination = await context.DiagonosisExaminations
                .FirstOrDefaultAsync(m => m.DigonosisExaminationID == id);
            if (diagnosisExamination == null)
            {
                return NotFound();
            }

            return View(diagnosisExamination);
        }

        // GET: DiagnosisExaminations/Create
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DiagnosisExamination diagnosisExamination)
        {
            var IsExist = IsExaminationDuplicate(diagnosisExamination);
            if (!IsExist)
            {
                if (ModelState.IsValid)
                {
                    diagnosisExamination.IsRowDeleted = false;
                    context.Add(diagnosisExamination);
                    await context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(diagnosisExamination);
            }
            else
            {
                ModelState.AddModelError("Name", "Duplicate Found!");
                return View();
            }
        }

        // GET: DiagnosisExaminations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diagnosisExamination = await context.DiagonosisExaminations.FindAsync(id);
            if (diagnosisExamination == null)
            {
                return NotFound();
            }
            return View(diagnosisExamination);
        }
   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DiagnosisExamination diagnosisExamination)
        {
            var IsExist = IsExaminationDuplicate(diagnosisExamination);
            if (!IsExist)
            {
                if (id != diagnosisExamination.DigonosisExaminationID)
                {
                    return NotFound();
                }
                if (ModelState.IsValid)
                {
                    try
                    {
                        diagnosisExamination.IsRowDeleted = false;
                        context.Update(diagnosisExamination);
                        await context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!DiagnosisExaminationExists(diagnosisExamination.DigonosisExaminationID))
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
                return View(diagnosisExamination);
            }
            else
            {
                ModelState.AddModelError("Name", "Duplicate Found!");
                return View();
            }
        }

        // GET: DiagnosisExaminations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diagnosisExamination = await context.DiagonosisExaminations
                .FirstOrDefaultAsync(m => m.DigonosisExaminationID == id);
            if (diagnosisExamination == null)
            {
                return NotFound();
            }

            return View(diagnosisExamination);
        }

        // POST: DiagnosisExaminations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var diagnosisExamination = await context.DiagonosisExaminations.FindAsync(id);
            context.DiagonosisExaminations.Remove(diagnosisExamination);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiagnosisExaminationExists(int id)
        {
            return context.DiagonosisExaminations.Any(e => e.DigonosisExaminationID == id);
        }
        /// <summary>
        /// Logic for duplicate check
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public bool IsExaminationDuplicate(DiagnosisExamination diagnosisExamination)
        {
            try
            {
                var regInDb = context.DiagonosisExaminations.FirstOrDefault(c => c.DigonosisExaminationsName.ToLower().Replace(" ", "-") == diagnosisExamination.DigonosisExaminationsName.ToLower().Replace(" ", "-"));

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
