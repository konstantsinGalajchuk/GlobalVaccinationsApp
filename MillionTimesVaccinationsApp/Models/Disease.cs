using System.ComponentModel.DataAnnotations;

namespace MillionTimesVaccinationsApp.Models;

public partial class Disease
{
    [Key]
    public int DiseaseId { get; set; }

    [Required]
    [RegularExpression(@"^\d{1,10}$", ErrorMessage = "The 'Code' field must contain no more than 10 digits.")]
    public int Code { get; set; }

    [Required]
    [StringLength(50, ErrorMessage = "The 'Name' field must contain no more than 50 characters.")]
    public string Name { get; set; } = null!;

    public virtual ICollection<Vaccine> Vaccines { get; set; } = new List<Vaccine>();
}
