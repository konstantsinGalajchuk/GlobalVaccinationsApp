using MillionTimesVaccinationsApp.Models;

namespace MillionTimesVaccinationsApp.ViewModels
{
    public class DoseViewModel
    {
        public PageViewModel PageViewModel { get; set; }

        public IEnumerable<Dose> Doses { get; set; }
    }
}
