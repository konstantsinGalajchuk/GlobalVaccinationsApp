using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MillionTimesVaccinationsApp.Models;

public partial class Vaccine
{
    [Key]
    public int VaccineId { get; set; }

    public int? DiseaseId { get; set; }

    public string? Description { get; set; }

    public string Manufacturer { get; set; } = null!;

    public virtual Disease? Disease { get; set; }

    public virtual ICollection<Vaccination> Vaccinations { get; set; } = new List<Vaccination>();

    public virtual ICollection<VaccineDose> VaccineDoses { get; set; } = new List<VaccineDose>();
}
