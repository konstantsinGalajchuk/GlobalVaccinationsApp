using MillionTimesVaccinationsApp.Models;

namespace MillionTimesVaccinationsApp.ViewModels
{
    public class VaccineDoseViewModel
    {
        public PageViewModel PageViewModel { get; set; }

        public IEnumerable<VaccineDose> VaccineDoses { get; set; }
    }
}
