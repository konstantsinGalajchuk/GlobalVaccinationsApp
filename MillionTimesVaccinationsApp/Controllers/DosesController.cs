using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MillionTimesVaccinationsApp.Data;
using MillionTimesVaccinationsApp.Models;
using MillionTimesVaccinationsApp.ViewModels;

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
        public async Task<IActionResult> Index(int page = 1)
        {
            IQueryable<Dose> filtredDoses = _context.Doses;

            int pageSize = 20;
            var count = await filtredDoses.CountAsync();
            var items = await filtredDoses.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            DoseViewModel viewModel = new DoseViewModel
            {
                PageViewModel = pageViewModel,
                Doses = items
            };

            return View(viewModel);
        }

        // GET: Doses/Details/5
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
