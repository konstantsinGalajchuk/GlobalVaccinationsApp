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
    public class DosesController : Controller
    {
        private readonly GlobalVaccinationsDbContext _context;

        public DosesController(GlobalVaccinationsDbContext context)
        {
            _context = context;
        }

        // GET: Doses
        public async Task<IActionResult> Index()
        {
              return _context.Doses != null ? 
                          View(await _context.Doses.ToListAsync()) :
                          Problem("Entity set 'GlobalVaccinationsDbContext.Doses'  is null.");
        }

        // GET: Doses/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Doses == null)
            {
                return NotFound();
            }

            var dose = await _context.Doses
                .FirstOrDefaultAsync(m => m.DoseId == id);
            if (dose == null)
            {
                return NotFound();
            }

            return View(dose);
        }

        // GET: Doses/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Doses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DoseId,Value")] Dose dose)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dose);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dose);
        }

        // GET: Doses/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Doses == null)
            {
                return NotFound();
            }

            var dose = await _context.Doses.FindAsync(id);
            if (dose == null)
            {
                return NotFound();
            }
            return View(dose);
        }

        // POST: Doses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DoseId,Value")] Dose dose)
        {
            if (id != dose.DoseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dose);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoseExists(dose.DoseId))
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
            return View(dose);
        }

        // GET: Doses/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Doses == null)
            {
                return NotFound();
            }

            var dose = await _context.Doses
                .FirstOrDefaultAsync(m => m.DoseId == id);
            if (dose == null)
            {
                return NotFound();
            }

            return View(dose);
        }

        // POST: Doses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Doses == null)
            {
                return Problem("Entity set 'GlobalVaccinationsDbContext.Doses'  is null.");
            }
            var dose = await _context.Doses.FindAsync(id);
            if (dose != null)
            {
                _context.Doses.Remove(dose);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DoseExists(int id)
        {
          return (_context.Doses?.Any(e => e.DoseId == id)).GetValueOrDefault();
        }
    }
}
