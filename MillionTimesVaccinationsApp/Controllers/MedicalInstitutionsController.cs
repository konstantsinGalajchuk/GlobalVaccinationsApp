using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MillionTimesVaccinationsApp.Data;
using MillionTimesVaccinationsApp.Models;
using MillionTimesVaccinationsApp.ViewModels;

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
        public async Task<IActionResult> Index(string? name, string? region, string? city, int page = 1)
        {
            IQueryable<MedicalInstitution> filtredInstitutions = _context.MedicalInstitutions;

            if (!string.IsNullOrEmpty(name))
            {
                filtredInstitutions = filtredInstitutions.Where(i => i.Name == name);
                HttpContext.Session.SetString("InstitutionsName", name);
                ViewData["InstitutionsName"] = name;
            }
            else
            {
                ViewData["InstitutionsName"] = HttpContext.Session.GetString("InstitutionsName");
            }

            if (!string.IsNullOrEmpty(region))
            {
                filtredInstitutions = filtredInstitutions.Where(i => i.Region == region);
                HttpContext.Session.SetString("InstitutionsRegion", region);
                ViewData["InstitutionsRegion"] = region;
            }
            else
            {
                ViewData["InstitutionsRegion"] = HttpContext.Session.GetString("InstitutionsRegion");
            }

            if (!string.IsNullOrEmpty(city))
            {
                filtredInstitutions = filtredInstitutions.Where(i => i.City == city);
                HttpContext.Session.SetString("InstitutionsCity", city);
                ViewData["InstitutionsCity"] = city;
            }
            else
            {
                ViewData["InstitutionsCity"] = HttpContext.Session.GetString("InstitutionsCity");
            }

            int pageSize = 20;
            var count = await filtredInstitutions.CountAsync();
            var items = await filtredInstitutions.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            MedicalInstitutionViewModel viewModel = new MedicalInstitutionViewModel
            {
                PageViewModel = pageViewModel,
                MedicalInstitutions = items
            };

            return View(viewModel);
        }

        public IActionResult ClearFilters()
        {
            HttpContext.Session.Clear();
            ViewData["InstitutionsName"] = string.Empty;
            ViewData["InstitutionsRegion"] = string.Empty;
            ViewData["InstitutionsCity"] = string.Empty;

            return RedirectToAction("Index");
        }

        // GET: MedicalInstitutions/Details/5
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
