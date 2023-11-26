using MillionTimesVaccinationsApp.Models;

namespace MillionTimesVaccinationsApp.ViewModels
{
    public class DiseaseViewModel
    {
        public PageViewModel PageViewModel { get; set; }

        public IEnumerable<Disease> Diseases { get; set; }
    }
}
