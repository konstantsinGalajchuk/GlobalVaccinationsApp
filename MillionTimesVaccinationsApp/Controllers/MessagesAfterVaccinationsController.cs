using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MillionTimesVaccinationsApp.Data;
using MillionTimesVaccinationsApp.Models;
using MillionTimesVaccinationsApp.ViewModels;

namespace MillionTimesVaccinationsApp.Controllers
{
    public class MessagesAfterVaccinationsController : Controller
    {
        private readonly GlobalVaccinationsDbContext _context;

        public MessagesAfterVaccinationsController(GlobalVaccinationsDbContext context)
        {
            _context = context;
        }

        // GET: MessagesAfterVaccinations
        public async Task<IActionResult> Index(DateTime? date, string doctor, int page = 1)
        {
            IQueryable<MessagesAfterVaccination> filtredMessages = _context.MessagesAfterVaccinations;

            if (date != null)
            {
                filtredMessages = filtredMessages.Where(m => m.Date == date);
                HttpContext.Session.SetString("MessagesDate", date.ToString());
                DateTime dateValue = (DateTime)date;
                ViewData["MessagesDate"] = dateValue.ToString("yyyy-MM-dd");
            }
            else
            {
                DateTime dateValue;
                if (DateTime.TryParse(HttpContext.Session.GetString("MessagesDate"), out dateValue))
                {
                    ViewData["MessagesDate"] = dateValue.ToString("yyyy-MM-dd");
                }
            }

            if (!string.IsNullOrEmpty(doctor))
            {
                filtredMessages = filtredMessages.Where(m => m.Doctor == doctor);
                HttpContext.Session.SetString("MessagesDoctor", doctor);
                ViewData["MessagesDoctor"] = doctor;
            }
            else
            {
                ViewData["MessagesDoctor"] = HttpContext.Session.GetString("MessagesDoctor");
            }

            int pageSize = 20;
            var count = await filtredMessages.CountAsync();
            var items = await filtredMessages.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            MessagesAfterVaccinationViewModel viewModel = new MessagesAfterVaccinationViewModel
            {
                PageViewModel = pageViewModel,
                MessagesAfterVaccinations = items
            };

            return View(viewModel);
        }

        public IActionResult ClearFilters()
        {
            HttpContext.Session.Clear();
            ViewData["MessagesDate"] = string.Empty;
            ViewData["MessagesDoctor"] = string.Empty;

            return RedirectToAction("Index");
        }

        // GET: MessagesAfterVaccinations/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MessagesAfterVaccinations == null)
            {
                return NotFound();
            }

            var messagesAfterVaccination = await _context.MessagesAfterVaccinations
                .FirstOrDefaultAsync(m => m.MessageId == id);
            if (messagesAfterVaccination == null)
            {
                return NotFound();
            }

            return View(messagesAfterVaccination);
        }

        // GET: MessagesAfterVaccinations/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: MessagesAfterVaccinations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MessageId,Description,Date,Recommendations,Doctor")] MessagesAfterVaccination messagesAfterVaccination)
        {
            if (ModelState.IsValid)
            {
                _context.Add(messagesAfterVaccination);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(messagesAfterVaccination);
        }

        // GET: MessagesAfterVaccinations/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MessagesAfterVaccinations == null)
            {
                return NotFound();
            }

            var messagesAfterVaccination = await _context.MessagesAfterVaccinations.FindAsync(id);
            if (messagesAfterVaccination == null)
            {
                return NotFound();
            }
            return View(messagesAfterVaccination);
        }

        // POST: MessagesAfterVaccinations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MessageId,Description,Date,Recommendations,Doctor")] MessagesAfterVaccination messagesAfterVaccination)
        {
            if (id != messagesAfterVaccination.MessageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(messagesAfterVaccination);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MessagesAfterVaccinationExists(messagesAfterVaccination.MessageId))
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
            return View(messagesAfterVaccination);
        }

        // GET: MessagesAfterVaccinations/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MessagesAfterVaccinations == null)
            {
                return NotFound();
            }

            var messagesAfterVaccination = await _context.MessagesAfterVaccinations
                .FirstOrDefaultAsync(m => m.MessageId == id);
            if (messagesAfterVaccination == null)
            {
                return NotFound();
            }

            return View(messagesAfterVaccination);
        }

        // POST: MessagesAfterVaccinations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MessagesAfterVaccinations == null)
            {
                return Problem("Entity set 'GlobalVaccinationsDbContext.MessagesAfterVaccinations'  is null.");
            }
            var messagesAfterVaccination = await _context.MessagesAfterVaccinations.FindAsync(id);
            if (messagesAfterVaccination != null)
            {
                _context.MessagesAfterVaccinations.Remove(messagesAfterVaccination);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MessagesAfterVaccinationExists(int id)
        {
          return (_context.MessagesAfterVaccinations?.Any(e => e.MessageId == id)).GetValueOrDefault();
        }
    }
}
