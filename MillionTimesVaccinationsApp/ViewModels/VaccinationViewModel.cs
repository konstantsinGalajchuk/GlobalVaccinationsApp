using MillionTimesVaccinationsApp.Models;

namespace MillionTimesVaccinationsApp.ViewModels
{
    public class VaccinationViewModel
    {
        public PageViewModel PageViewModel { get; set; }

        public IEnumerable<Vaccination> Vaccinations { get; set; }
    }
}
