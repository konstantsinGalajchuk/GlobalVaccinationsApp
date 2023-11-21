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

namespace MillionTimesVaccinationsApp.Controllers
{
    public class MedicalInstitutionsController : Controller
    {
        private readonly GlobalVaccinationsDbContext _context;

        public MedicalInstitutionsController(GlobalVaccinationsDbContext context)
        {
            _context = context;
        }

        // GET: MedicalInstitutions
        public async Task<IActionResult> Index()
        {
              return _context.MedicalInstitutions != null ? 
                          View(await _context.MedicalInstitutions.ToListAsync()) :
                          Problem("Entity set 'GlobalVaccinationsDbContext.MedicalInstitutions'  is null.");
        }

        // GET: MedicalInstitutions/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MedicalInstitutions == null)
            {
                return NotFound();
            }

            var medicalInstitution = await _context.MedicalInstitutions
                .FirstOrDefaultAsync(m => m.MedicalInstitutionId == id);
            if (medicalInstitution == null)
            {
                return NotFound();
            }

            return View(medicalInstitution);
        }

        // GET: MedicalInstitutions/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: MedicalInstitutions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MedicalInstitutionId,Name,Region,City,Street,HouseNumber,ApartmentNumber")] MedicalInstitution medicalInstitution)
        {
            if (ModelState.IsValid)
            {
                _context.Add(medicalInstitution);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(medicalInstitution);
        }

        // GET: MedicalInstitutions/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MedicalInstitutions == null)
            {
                return NotFound();
            }

            var medicalInstitution = await _context.MedicalInstitutions.FindAsync(id);
            if (medicalInstitution == null)
            {
                return NotFound();
            }
            return View(medicalInstitution);
        }

        // POST: MedicalInstitutions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MedicalInstitutionId,Name,Region,City,Street,HouseNumber,ApartmentNumber")] MedicalInstitution medicalInstitution)
        {
            if (id != medicalInstitution.MedicalInstitutionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medicalInstitution);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicalInstitutionExists(medicalInstitution.MedicalInstitutionId))
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
            return View(medicalInstitution);
        }

        // GET: MedicalInstitutions/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MedicalInstitutions == null)
            {
                return NotFound();
            }

            var medicalInstitution = await _context.MedicalInstitutions
                .FirstOrDefaultAsync(m => m.MedicalInstitutionId == id);
            if (medicalInstitution == null)
            {
                return NotFound();
            }

            return View(medicalInstitution);
        }

        // POST: MedicalInstitutions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MedicalInstitutions == null)
            {
                return Problem("Entity set 'GlobalVaccinationsDbContext.MedicalInstitutions'  is null.");
            }
            var medicalInstitution = await _context.MedicalInstitutions.FindAsync(id);
            if (medicalInstitution != null)
            {
                _context.MedicalInstitutions.Remove(medicalInstitution);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedicalInstitutionExists(int id)
        {
          return (_context.MedicalInstitutions?.Any(e => e.MedicalInstitutionId == id)).GetValueOrDefault();
        }
    }
}
