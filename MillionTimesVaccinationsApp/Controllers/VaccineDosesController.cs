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
    public class VaccineDosesController : Controller
    {
        private readonly GlobalVaccinationsDbContext _context;

        public VaccineDosesController(GlobalVaccinationsDbContext context)
        {
            _context = context;
        }

        // GET: VaccineDoses
        public async Task<IActionResult> Index()
        {
            var globalVaccinationsDbContext = _context.VaccineDoses.Include(v => v.Dose).Include(v => v.Vaccine);
            return View(await globalVaccinationsDbContext.ToListAsync());
        }

        // GET: VaccineDoses/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.VaccineDoses == null)
            {
                return NotFound();
            }

            var vaccineDose = await _context.VaccineDoses
                .Include(v => v.Dose)
                .Include(v => v.Vaccine)
                .FirstOrDefaultAsync(m => m.VaccineDoseId == id);
            if (vaccineDose == null)
            {
                return NotFound();
            }

            return View(vaccineDose);
        }

        // GET: VaccineDoses/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["DoseId"] = new SelectList(_context.Doses, "DoseId", "DoseId");
            ViewData["VaccineId"] = new SelectList(_context.Vaccines, "VaccineId", "VaccineId");
            return View();
        }

        // POST: VaccineDoses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VaccineDoseId,DoseId,VaccineId")] VaccineDose vaccineDose)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vaccineDose);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DoseId"] = new SelectList(_context.Doses, "DoseId", "DoseId", vaccineDose.DoseId);
            ViewData["VaccineId"] = new SelectList(_context.Vaccines, "VaccineId", "VaccineId", vaccineDose.VaccineId);
            return View(vaccineDose);
        }

        // GET: VaccineDoses/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.VaccineDoses == null)
            {
                return NotFound();
            }

            var vaccineDose = await _context.VaccineDoses.FindAsync(id);
            if (vaccineDose == null)
            {
                return NotFound();
            }
            ViewData["DoseId"] = new SelectList(_context.Doses, "DoseId", "DoseId", vaccineDose.DoseId);
            ViewData["VaccineId"] = new SelectList(_context.Vaccines, "VaccineId", "VaccineId", vaccineDose.VaccineId);
            return View(vaccineDose);
        }

        // POST: VaccineDoses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VaccineDoseId,DoseId,VaccineId")] VaccineDose vaccineDose)
        {
            if (id != vaccineDose.VaccineDoseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vaccineDose);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VaccineDoseExists(vaccineDose.VaccineDoseId))
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
            ViewData["DoseId"] = new SelectList(_context.Doses, "DoseId", "DoseId", vaccineDose.DoseId);
            ViewData["VaccineId"] = new SelectList(_context.Vaccines, "VaccineId", "VaccineId", vaccineDose.VaccineId);
            return View(vaccineDose);
        }

        // GET: VaccineDoses/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.VaccineDoses == null)
            {
                return NotFound();
            }

            var vaccineDose = await _context.VaccineDoses
                .Include(v => v.Dose)
                .Include(v => v.Vaccine)
                .FirstOrDefaultAsync(m => m.VaccineDoseId == id);
            if (vaccineDose == null)
            {
                return NotFound();
            }

            return View(vaccineDose);
        }

        // POST: VaccineDoses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.VaccineDoses == null)
            {
                return Problem("Entity set 'GlobalVaccinationsDbContext.VaccineDoses'  is null.");
            }
            var vaccineDose = await _context.VaccineDoses.FindAsync(id);
            if (vaccineDose != null)
            {
                _context.VaccineDoses.Remove(vaccineDose);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VaccineDoseExists(int id)
        {
          return (_context.VaccineDoses?.Any(e => e.VaccineDoseId == id)).GetValueOrDefault();
        }
    }
}
