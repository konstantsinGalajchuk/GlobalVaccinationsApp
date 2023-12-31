﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MillionTimesVaccinationsApp.Data;
using MillionTimesVaccinationsApp.Models;
using MillionTimesVaccinationsApp.ViewModels;

namespace MillionTimesVaccinationsApp.Controllers
{
    public class DiseasesController : Controller
    {
        private readonly GlobalVaccinationsDbContext _context;

        public DiseasesController(GlobalVaccinationsDbContext context)
        {
            _context = context;
        }

        // GET: Diseases
        public async Task<IActionResult> Index(int? code, string? name, int page = 1)
        {
            IQueryable<Disease> filtredDiseases = _context.Diseases;

            if (!string.IsNullOrEmpty(name))
            {
                filtredDiseases = filtredDiseases.Where(d => d.Name == name);
                HttpContext.Session.SetString("DiseasesName", name);
                ViewData["DiseasesName"] = name;
            }
            else
            {
                ViewData["DiseasesName"] = HttpContext.Session.GetString("DiseasesName");
            }

            if (code != null)
            {
                filtredDiseases = filtredDiseases.Where(d => d.Code == code);
                HttpContext.Session.SetString("DiseasesCode", code.ToString());
                ViewData["DiseasesCode"] = code;
            }
            else
            {
                ViewData["DiseasesCode"] = HttpContext.Session.GetString("DiseasesCode");
            }

            int pageSize = 20;
            var count = await filtredDiseases.CountAsync();
            var items = await filtredDiseases.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            DiseaseViewModel viewModel = new DiseaseViewModel
            {
                PageViewModel = pageViewModel,
                Diseases = items
            };

            return View(viewModel);
        }

        public IActionResult ClearFilters()
        {
            HttpContext.Session.Clear();
            ViewData["DiseasesName"] = string.Empty;
            ViewData["DiseasesCode"] = string.Empty;

            return RedirectToAction("Index");
        }

        // GET: Diseases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Diseases == null)
            {
                return NotFound();
            }

            var disease = await _context.Diseases
                .FirstOrDefaultAsync(m => m.DiseaseId == id);
            if (disease == null)
            {
                return NotFound();
            }

            return View(disease);
        }

        // GET: Diseases/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Diseases/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DiseaseId,Code,Name")] Disease disease)
        {
            if (ModelState.IsValid)
            {
                _context.Add(disease);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(disease);
        }

        // GET: Diseases/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Diseases == null)
            {
                return NotFound();
            }

            var disease = await _context.Diseases.FindAsync(id);
            if (disease == null)
            {
                return NotFound();
            }
            return View(disease);
        }

        // POST: Diseases/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DiseaseId,Code,Name")] Disease disease)
        {
            if (id != disease.DiseaseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(disease);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiseaseExists(disease.DiseaseId))
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
            return View(disease);
        }

        // GET: Diseases/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Diseases == null)
            {
                return NotFound();
            }

            var disease = await _context.Diseases
                .FirstOrDefaultAsync(m => m.DiseaseId == id);
            if (disease == null)
            {
                return NotFound();
            }

            return View(disease);
        }

        // POST: Diseases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Diseases == null)
            {
                return Problem("Entity set 'GlobalVaccinationsDbContext.Diseases'  is null.");
            }
            var disease = await _context.Diseases.FindAsync(id);
            if (disease != null)
            {
                _context.Diseases.Remove(disease);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiseaseExists(int id)
        {
          return (_context.Diseases?.Any(e => e.DiseaseId == id)).GetValueOrDefault();
        }
    }
}
