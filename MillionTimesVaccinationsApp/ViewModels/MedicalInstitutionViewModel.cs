using MillionTimesVaccinationsApp.Models;

namespace MillionTimesVaccinationsApp.ViewModels
{
    public class MedicalInstitutionViewModel
    {
        public PageViewModel PageViewModel { get; set; }

        public IEnumerable<MedicalInstitution> MedicalInstitutions { get; set; }
    }
}
