using MillionTimesVaccinationsApp.Models;

namespace MillionTimesVaccinationsApp.ViewModels
{
    public class PatientViewModel
    {
        public PageViewModel PageViewModel { get; set; }

        public IEnumerable<Patient> Patients { get; set; }
    }
}
