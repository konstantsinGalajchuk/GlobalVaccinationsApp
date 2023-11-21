using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MillionTimesVaccinationsApp.Models;

namespace MillionTimesVaccinationsApp.Data;

public partial class GlobalVaccinationsDbContext : DbContext
{
    public GlobalVaccinationsDbContext()
    {
    }

    public GlobalVaccinationsDbContext(DbContextOptions<GlobalVaccinationsDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Disease> Diseases { get; set; }

    public virtual DbSet<Dose> Doses { get; set; }

    public virtual DbSet<MedicalInstitution> MedicalInstitutions { get; set; }

    public virtual DbSet<MessagesAfterVaccination> MessagesAfterVaccinations { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<Vaccination> Vaccinations { get; set; }

    public virtual DbSet<Vaccine> Vaccines { get; set; }

    public virtual DbSet<VaccineDose> VaccineDoses { get; set; }
}
