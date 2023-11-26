using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MillionTimesVaccinationsApp.Models;

public partial class Dose
{
    [Key]
    public int DoseId { get; set; }

    [Required]
    [RegularExpression(@"^\d+(\.\d+)?$", ErrorMessage = "The 'Value' field must contain only floating numbers separated by the '.' character.")]
    public double Value { get; set; }

    public virtual ICollection<VaccineDose> VaccineDoses { get; set; } = new List<VaccineDose>();
}
