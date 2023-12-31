﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MillionTimesVaccinationsApp.Data;
using MillionTimesVaccinationsApp.Models;
using MillionTimesVaccinationsApp.ViewModels;

namespace MillionTimesVaccinationsApp.Controllers
{
    public class PatientsController : Controller
    {
        private readonly GlobalVaccinationsDbContext _context;

        public PatientsController(GlobalVaccinationsDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string? sex, string? city, string? region, string? fullName, int page = 1)
        {
            IQueryable<Patient> filtredPatients = _context.Patients;

            if (!string.IsNullOrEmpty(sex))
            {
                filtredPatients = filtredPatients.Where(p => p.Sex == sex);
                HttpContext.Session.SetString("PatientsSex", sex);
                ViewData["PatientsSex"] = sex;
            }
            else
            {
                ViewData["PatientsSex"] = HttpContext.Session.GetString("PatientsSex");
            }

            if (!string.IsNullOrEmpty(city))
            {
                filtredPatients = filtredPatients.Where(p => p.City == city);
                HttpContext.Session.SetString("PatientsCity", city);
                ViewData["PatientsCity"] = city;
            }
            else
            {
                ViewData["PatientsCity"] = HttpContext.Session.GetString("PatientsCity");
            }

            if (!string.IsNullOrEmpty(region))
            {
                filtredPatients = filtredPatients.Where(p => p.Region == region);
                HttpContext.Session.SetString("PatientsRegion", region);
                ViewData["PatientsRegion"] = region;
            }
            else
            {
                ViewData["PatientsRegion"] = HttpContext.Session.GetString("PatientsRegion");
            }

            if (!string.IsNullOrEmpty(fullName))
            {
                filtredPatients = filtredPatients.Where(p => p.FullName == fullName);
                HttpContext.Session.SetString("PatientsFullName", fullName);
                ViewData["PatientsFullName"] = fullName;
            }
            else
            {
                ViewData["PatientsFullName"] = HttpContext.Session.GetString("PatientsFullName");
            }

            int pageSize = 20;
            var count = await filtredPatients.CountAsync();
            var items = await filtredPatients.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            PatientViewModel viewModel = new PatientViewModel
            {
                PageViewModel = pageViewModel,
                Patients = items
            };

            return View(viewModel);
        }

        public IActionResult ClearFilters()
        {
            HttpContext.Session.Clear();
            ViewData["PatientsSex"] = string.Empty;
            ViewData["PatientsFullName"] = string.Empty;
            ViewData["PatientsCity"] = string.Empty;
            ViewData["PatientsRegion"] = string.Empty;

            return RedirectToAction("Index");
        }

        // GET: Patients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Patients == null)
            {
                return NotFound();
            }

            var patient = await _context.Patients
                .FirstOrDefaultAsync(m => m.PatientId == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // GET: Patients/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Patients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PatientId,FullName,Sex,Passport,Region,City,Street,HouseNumber,ApartmentNumber")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                _context.Add(patient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(patient);
        }

        // GET: Patients/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Patients == null)
            {
                return NotFound();
            }

            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PatientId,FullName,Sex,Passport,Region,City,Street,HouseNumber,ApartmentNumber")] Patient patient)
        {
            if (id != patient.PatientId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientExists(patient.PatientId))
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
            return View(patient);
        }

        // GET: Patients/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Patients == null)
            {
                return NotFound();
            }

            var patient = await _context.Patients
                .FirstOrDefaultAsync(m => m.PatientId == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Patients == null)
            {
                return Problem("Entity set 'GlobalVaccinationsDbContext.Patients'  is null.");
            }
            var patient = await _context.Patients.FindAsync(id);
            if (patient != null)
            {
                _context.Patients.Remove(patient);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PatientExists(int id)
        {
          return (_context.Patients?.Any(e => e.PatientId == id)).GetValueOrDefault();
        }
    }
}
