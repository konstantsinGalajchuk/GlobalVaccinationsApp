using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MillionTimesVaccinationsApp.Models;

public partial class VaccineDose
{
    [Key]
    public int VaccineDoseId { get; set; }

    [Required]
    public int? DoseId { get; set; }

    [Required]
    public int? VaccineId { get; set; }

    public virtual Dose? Dose { get; set; }

    public virtual Vaccine? Vaccine { get; set; }
}
