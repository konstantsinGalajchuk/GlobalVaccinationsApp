using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MillionTimesVaccinationsApp.Models;

public partial class Vaccination
{
    [Key]
    public int VaccinationId { get; set; }

    public int? VaccineId { get; set; }

    public DateTime Date { get; set; }

    public int DoseNumber { get; set; }

    public int? PatientId { get; set; }

    public int? MedicalInstitutionId { get; set; }

    public virtual MedicalInstitution? MedicalInstitution { get; set; }

    public virtual Patient? Patient { get; set; }

    public virtual Vaccine? Vaccine { get; set; }
}
