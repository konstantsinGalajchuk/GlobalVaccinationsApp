using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MillionTimesVaccinationsApp.Models;

public partial class Dose
{
    [Key]
    public int DoseId { get; set; }

    public double Value { get; set; }

    public virtual ICollection<VaccineDose> VaccineDoses { get; set; } = new List<VaccineDose>();
}
