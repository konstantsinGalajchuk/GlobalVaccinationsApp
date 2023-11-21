using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MillionTimesVaccinationsApp.Models;

public partial class Patient
{
    [Key]
    public int PatientId { get; set; }

    public string? FullName { get; set; }

    public string Sex { get; set; } = null!;

    public string Passport { get; set; } = null!;

    public string? Region { get; set; }

    public string City { get; set; } = null!;

    public string Street { get; set; } = null!;

    public string HouseNumber { get; set; } = null!;

    public string? ApartmentNumber { get; set; }

    public virtual ICollection<Vaccination> Vaccinations { get; set; } = new List<Vaccination>();
}
