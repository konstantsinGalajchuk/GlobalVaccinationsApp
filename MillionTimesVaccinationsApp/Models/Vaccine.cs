using System.ComponentModel.DataAnnotations;

namespace MillionTimesVaccinationsApp.Models;

public partial class Vaccine
{
    [Key]
    public int VaccineId { get; set; }

    [Required]
    public int? DiseaseId { get; set; }

    [Required]
    [StringLength(250, ErrorMessage = "The 'Description' field must contain no more than 250 characters.")]
    public string? Description { get; set; }

    [Required]
    [StringLength(50, ErrorMessage = "The 'Manufacturer' field must contain no more than 50 characters.")]
    public string Manufacturer { get; set; } = null!;

    public virtual Disease? Disease { get; set; }

    public virtual ICollection<Vaccination> Vaccinations { get; set; } = new List<Vaccination>();

    public virtual ICollection<VaccineDose> VaccineDoses { get; set; } = new List<VaccineDose>();
}
