﻿using System;
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
    public class VaccinationsController : Controller
    {
        private readonly GlobalVaccinationsDbContext _context;

        public VaccinationsController(GlobalVaccinationsDbContext context)
        {
            _context = context;
        }

        // GET: Vaccinations
        public async Task<IActionResult> Index()
        {
            var globalVaccinationsDbContext = _context.Vaccinations.Include(v => v.MedicalInstitution).Include(v => v.Patient).Include(v => v.Vaccine);
            return View(await globalVaccinationsDbContext.ToListAsync());
        }

        // GET: Vaccinations/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Vaccinations == null)
            {
                return NotFound();
            }

            var vaccination = await _context.Vaccinations
                .Include(v => v.MedicalInstitution)
                .Include(v => v.Patient)
                .Include(v => v.Vaccine)
                .FirstOrDefaultAsync(m => m.VaccinationId == id);
            if (vaccination == null)
            {
                return NotFound();
            }

            return View(vaccination);
        }

        // GET: Vaccinations/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["MedicalInstitutionId"] = new SelectList(_context.MedicalInstitutions, "MedicalInstitutionId", "MedicalInstitutionId");
            ViewData["PatientId"] = new SelectList(_context.Patients, "PatientId", "PatientId");
            ViewData["VaccineId"] = new SelectList(_context.Vaccines, "VaccineId", "VaccineId");
            return View();
        }

        // POST: Vaccinations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VaccinationId,VaccineId,Date,DoseNumber,PatientId,MedicalInstitutionId")] Vaccination vaccination)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vaccination);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MedicalInstitutionId"] = new SelectList(_context.MedicalInstitutions, "MedicalInstitutionId", "MedicalInstitutionId", vaccination.MedicalInstitutionId);
            ViewData["PatientId"] = new SelectList(_context.Patients, "PatientId", "PatientId", vaccination.PatientId);
            ViewData["VaccineId"] = new SelectList(_context.Vaccines, "VaccineId", "VaccineId", vaccination.VaccineId);
            return View(vaccination);
        }

        // GET: Vaccinations/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Vaccinations == null)
            {
                return NotFound();
            }

            var vaccination = await _context.Vaccinations.FindAsync(id);
            if (vaccination == null)
            {
                return NotFound();
            }
            ViewData["MedicalInstitutionId"] = new SelectList(_context.MedicalInstitutions, "MedicalInstitutionId", "MedicalInstitutionId", vaccination.MedicalInstitutionId);
            ViewData["PatientId"] = new SelectList(_context.Patients, "PatientId", "PatientId", vaccination.PatientId);
            ViewData["VaccineId"] = new SelectList(_context.Vaccines, "VaccineId", "VaccineId", vaccination.VaccineId);
            return View(vaccination);
        }

        // POST: Vaccinations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VaccinationId,VaccineId,Date,DoseNumber,PatientId,MedicalInstitutionId")] Vaccination vaccination)
        {
            if (id != vaccination.VaccinationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vaccination);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VaccinationExists(vaccination.VaccinationId))
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
            ViewData["MedicalInstitutionId"] = new SelectList(_context.MedicalInstitutions, "MedicalInstitutionId", "MedicalInstitutionId", vaccination.MedicalInstitutionId);
            ViewData["PatientId"] = new SelectList(_context.Patients, "PatientId", "PatientId", vaccination.PatientId);
            ViewData["VaccineId"] = new SelectList(_context.Vaccines, "VaccineId", "VaccineId", vaccination.VaccineId);
            return View(vaccination);
        }

        // GET: Vaccinations/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Vaccinations == null)
            {
                return NotFound();
            }

            var vaccination = await _context.Vaccinations
                .Include(v => v.MedicalInstitution)
                .Include(v => v.Patient)
                .Include(v => v.Vaccine)
                .FirstOrDefaultAsync(m => m.VaccinationId == id);
            if (vaccination == null)
            {
                return NotFound();
            }

            return View(vaccination);
        }

        // POST: Vaccinations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Vaccinations == null)
            {
                return Problem("Entity set 'GlobalVaccinationsDbContext.Vaccinations'  is null.");
            }
            var vaccination = await _context.Vaccinations.FindAsync(id);
            if (vaccination != null)
            {
                _context.Vaccinations.Remove(vaccination);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VaccinationExists(int id)
        {
          return (_context.Vaccinations?.Any(e => e.VaccinationId == id)).GetValueOrDefault();
        }
    }
}
