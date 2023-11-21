using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MillionTimesVaccinationsApp.Models;

public partial class Disease
{
    [Key]
    public int DiseaseId { get; set; }

    public int Code { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Vaccine> Vaccines { get; set; } = new List<Vaccine>();
}
