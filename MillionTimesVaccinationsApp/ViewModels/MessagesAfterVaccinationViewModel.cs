using MillionTimesVaccinationsApp.Models;

namespace MillionTimesVaccinationsApp.ViewModels
{
    public class MessagesAfterVaccinationViewModel
    {
        public PageViewModel PageViewModel { get; set; }

        public IEnumerable<MessagesAfterVaccination> MessagesAfterVaccinations { get; set; }
    }
}