using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MillionTimesVaccinationsApp.Data;
using MillionTimesVaccinationsApp.Models;
using MillionTimesVaccinationsApp.ViewModels;

namespace MillionTimesVaccinationsApp.Controllers
{
    public class ExtendedSearchController : Controller
    {
        private readonly GlobalVaccinationsDbContext _context;

        public ExtendedSearchController(GlobalVaccinationsDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> VaccinationsAccPatient(string? patientName, int page = 1)
        {
            IQueryable<Vaccination> filtredVaccinations = _context.Vaccinations
                .Include(v => v.MedicalInstitution)
                .Include(v => v.Patient)
                .Include(v => v.Vaccine);

            if (!string.IsNullOrEmpty(patientName))
            {
                filtredVaccinations = filtredVaccinations.Where(v => v.Patient.FullName == patientName);
                HttpContext.Session.SetString("Search1Name", patientName);
                ViewData["Search1Name"] = patientName;
            }
            else
            {
                ViewData["Search1Name"] = HttpContext.Session.GetString("Search1Name");
            }

            int pageSize = 20;
            var count = await filtredVaccinations.CountAsync();
            var items = await filtredVaccinations.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            VaccinationViewModel viewModel = new VaccinationViewModel
            {
                PageViewModel = pageViewModel,
                Vaccinations = items
            };

            return View(viewModel);
        }

        public IActionResult ClearFiltersSearch1()
        {
            HttpContext.Session.Clear();
            ViewData["Search1Name"] = string.Empty;

            return RedirectToAction("VaccinationsAccPatient");
        }

        public async Task<IActionResult> VaccinationsAccRegionDisease(string? region, string? city, string disease)
        {
            IQueryable<Vaccination> filtredVaccinations = _context.Vaccinations;

            if (!string.IsNullOrEmpty(region))
            {
                filtredVaccinations = filtredVaccinations.Where(v => v.MedicalInstitution.Region == region);
                HttpContext.Session.SetString("Search2Region", region);
                ViewData["Search2Region"] = region;
            }
            else
            {
                ViewData["Search2Region"] = HttpContext.Session.GetString("Search2Region");
            }

            if (!string.IsNullOrEmpty(city))
            {
                filtredVaccinations = filtredVaccinations.Where(v => v.MedicalInstitution.City == city);
                HttpContext.Session.SetString("Search2City", city);
                ViewData["Search2City"] = city;
            }
            else
            {
                ViewData["Search2City"] = HttpContext.Session.GetString("Search2City");
            }            

            if (!string.IsNullOrEmpty(disease))
            {
                filtredVaccinations = filtredVaccinations.Where(v => v.Vaccine.Disease.Name == disease);
                HttpContext.Session.SetString("Search2Disease", disease);
                ViewData["Search2Disease"] = disease;
            }
            else
            {
                ViewData["Search2Disease"] = HttpContext.Session.GetString("Search2Disease");
            }

            ViewData["Search2"] = filtredVaccinations.Count();

            return View();
        }

        public IActionResult ClearFiltersSearch2()
        {
            HttpContext.Session.Clear();
            ViewData["Search2Region"] = string.Empty;
            ViewData["Search2City"] = string.Empty;
            ViewData["Search2Disease"] = string.Empty;

            return RedirectToAction("VaccinationsAccRegionDisease");
        }

        public async Task<IActionResult> MessagesAccYear(int? year)
        {
            IQueryable<MessagesAfterVaccination> filtredMessages = _context.MessagesAfterVaccinations;

            if (year != null)
            {
                filtredMessages = filtredMessages.Where(m => m.Date.Year == year);
                HttpContext.Session.SetString("Search3Year", year.ToString());
                ViewData["Search3Year"] = year;
            }
            else
            {
                ViewData["Search3Year"] = HttpContext.Session.GetString("Search3Year");
            }

            ViewData["Search3month1"] = filtredMessages.Where(m => m.Date.Month == 1).Count();
            ViewData["Search3month2"] = filtredMessages.Where(m => m.Date.Month == 2).Count();
            ViewData["Search3month3"] = filtredMessages.Where(m => m.Date.Month == 3).Count();
            ViewData["Search3month4"] = filtredMessages.Where(m => m.Date.Month == 4).Count();
            ViewData["Search3month5"] = filtredMessages.Where(m => m.Date.Month == 5).Count();
            ViewData["Search3month6"] = filtredMessages.Where(m => m.Date.Month == 6).Count();
            ViewData["Search3month7"] = filtredMessages.Where(m => m.Date.Month == 7).Count();
            ViewData["Search3month8"] = filtredMessages.Where(m => m.Date.Month == 8).Count();
            ViewData["Search3month9"] = filtredMessages.Where(m => m.Date.Month == 9).Count();
            ViewData["Search3month10"] = filtredMessages.Where(m => m.Date.Month == 10).Count();
            ViewData["Search3month11"] = filtredMessages.Where(m => m.Date.Month == 11).Count();
            ViewData["Search3month12"] = filtredMessages.Where(m => m.Date.Month == 12).Count();

            return View();
        }

        public IActionResult ClearFiltersSearch3()
        {
            HttpContext.Session.Clear();
            ViewData["Search3Year"] = string.Empty;

            return RedirectToAction("MessagesAccYear");
        }
    }
}
