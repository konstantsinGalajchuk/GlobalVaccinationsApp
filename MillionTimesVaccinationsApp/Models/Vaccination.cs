using System.ComponentModel.DataAnnotations;

namespace MillionTimesVaccinationsApp.Models;

public partial class Vaccination
{
    [Key]
    public int VaccinationId { get; set; }

    [Required]
    public int? VaccineId { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required]
    [RegularExpression(@"^\d{1,10}$", ErrorMessage = "The 'DoseNumber' field must contain no more than 10 digits.")]
    public int DoseNumber { get; set; }

    [Required]
    public int? PatientId { get; set; }

    [Required]
    public int? MedicalInstitutionId { get; set; }

    public virtual MedicalInstitution? MedicalInstitution { get; set; }

    public virtual Patient? Patient { get; set; }

    public virtual Vaccine? Vaccine { get; set; }
}
