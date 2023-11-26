using MillionTimesVaccinationsApp.Models;

namespace MillionTimesVaccinationsApp.ViewModels
{
    public class VaccineViewModel
    {
        public PageViewModel PageViewModel { get; set; }

        public IEnumerable<Vaccine> Vaccines { get; set; }
    }
}
