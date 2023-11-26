using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MillionTimesVaccinationsApp.Data;
using MillionTimesVaccinationsApp.Models;
using MillionTimesVaccinationsApp.ViewModels;

namespace MillionTimesVaccinationsApp.Controllers
{
    public class VaccinesController : Controller
    {
        private readonly GlobalVaccinationsDbContext _context;

        public VaccinesController(GlobalVaccinationsDbContext context)
        {
            _context = context;
        }

        // GET: Vaccines
        public async Task<IActionResult> Index(string? manufacturer, string? diseaseName, int page = 1)
        {
            IQueryable<Vaccine> filtredVaccines = _context.Vaccines
                .Include(v => v.Disease);

            if (!string.IsNullOrEmpty(manufacturer))
            {
                filtredVaccines = filtredVaccines.Where(v => v.Manufacturer == manufacturer);
                HttpContext.Session.SetString("VaccinesManufacturer", manufacturer);
                ViewData["VaccinesManufacturer"] = manufacturer;
            }
            else
            {
                ViewData["VaccinesManufacturer"] = HttpContext.Session.GetString("VaccinesManufacturer");
            }

            if (!string.IsNullOrEmpty(diseaseName))
            {
                filtredVaccines = filtredVaccines.Where(v => v.Disease.Name == diseaseName);
                HttpContext.Session.SetString("VaccinesDiseaseName", diseaseName.ToString());
                ViewData["VaccinesDiseaseName"] = diseaseName;
            }
            else
            {
                ViewData["VaccinesDiseaseName"] = HttpContext.Session.GetString("VaccinesDiseaseName");
            }

            int pageSize = 20;
            var count = await filtredVaccines.CountAsync();
            var items = await filtredVaccines.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            VaccineViewModel viewModel = new VaccineViewModel
            {
                PageViewModel = pageViewModel,
                Vaccines = items
            };

            return View(viewModel);
        }

        public IActionResult ClearFilters()
        {
            HttpContext.Session.Clear();
            ViewData["VaccinesManufacturer"] = string.Empty;
            ViewData["VaccinesDiseaseName"] = string.Empty;

            return RedirectToAction("Index");
        }

        // GET: Vaccines/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Vaccines == null)
            {
                return NotFound();
            }

            var vaccine = await _context.Vaccines
                .Include(v => v.Disease)
                .FirstOrDefaultAsync(m => m.VaccineId == id);
            if (vaccine == null)
            {
                return NotFound();
            }

            return View(vaccine);
        }

        // GET: Vaccines/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["DiseaseId"] = new SelectList(_context.Diseases, "DiseaseId", "DiseaseId");
            return View();
        }

        // POST: Vaccines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VaccineId,DiseaseId,Description,Manufacturer")] Vaccine vaccine)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vaccine);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DiseaseId"] = new SelectList(_context.Diseases, "DiseaseId", "DiseaseId", vaccine.DiseaseId);
            return View(vaccine);
        }

        // GET: Vaccines/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Vaccines == null)
            {
                return NotFound();
            }

            var vaccine = await _context.Vaccines.FindAsync(id);
            if (vaccine == null)
            {
                return NotFound();
            }
            ViewData["DiseaseId"] = new SelectList(_context.Diseases, "DiseaseId", "DiseaseId", vaccine.DiseaseId);
            return View(vaccine);
        }

        // POST: Vaccines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VaccineId,DiseaseId,Description,Manufacturer")] Vaccine vaccine)
        {
            if (id != vaccine.VaccineId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vaccine);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VaccineExists(vaccine.VaccineId))
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
            ViewData["DiseaseId"] = new SelectList(_context.Diseases, "DiseaseId", "DiseaseId", vaccine.DiseaseId);
            return View(vaccine);
        }

        // GET: Vaccines/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Vaccines == null)
            {
                return NotFound();
            }

            var vaccine = await _context.Vaccines
                .Include(v => v.Disease)
                .FirstOrDefaultAsync(m => m.VaccineId == id);
            if (vaccine == null)
            {
                return NotFound();
            }

            return View(vaccine);
        }

        // POST: Vaccines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Vaccines == null)
            {
                return Problem("Entity set 'GlobalVaccinationsDbContext.Vaccines'  is null.");
            }
            var vaccine = await _context.Vaccines.FindAsync(id);
            if (vaccine != null)
            {
                _context.Vaccines.Remove(vaccine);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VaccineExists(int id)
        {
          return (_context.Vaccines?.Any(e => e.VaccineId == id)).GetValueOrDefault();
        }
    }
}
