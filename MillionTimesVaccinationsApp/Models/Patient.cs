using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MillionTimesVaccinationsApp.Models;

public partial class Patient
{
    [Key]
    public int PatientId { get; set; }

    [Required]
    [StringLength(50, ErrorMessage = "The 'FullName' field must contain no more than 50 characters.")]
    public string? FullName { get; set; }

    [Required]
    [RegularExpression("^(male|female)$", ErrorMessage = "The 'Sex' field must contain only 'male' or 'female'.")]
    public string Sex { get; set; } = null!;

    [Required]
    public string Passport { get; set; } = null!;

    [Required]
    [StringLength(50, ErrorMessage = "The 'Region' field must contain no more than 50 characters.")]
    [RegularExpression(@"^[^0-9]+$", ErrorMessage = "The 'Region' field must not contain numbers.")]
    public string? Region { get; set; }

    [Required]
    [StringLength(50, ErrorMessage = "The 'City' field must contain no more than 50 characters.")]
    [RegularExpression(@"^[^0-9]+$", ErrorMessage = "The 'City' field must not contain numbers.")]
    public string City { get; set; } = null!;

    [Required]
    [StringLength(50, ErrorMessage = "The 'Street' field must contain no more than 50 characters.")]
    public string Street { get; set; } = null!;

    [Required]
    [RegularExpression(@"^\d{1,3}$", ErrorMessage = "The 'HouseNumber' field must contain numbers only.")]
    public string HouseNumber { get; set; } = null!;

    [RegularExpression(@"^\d{1,3}$", ErrorMessage = "The 'ApartmentNumber' field must contain numbers only.")]
    public string? ApartmentNumber { get; set; }

    public virtual ICollection<Vaccination> Vaccinations { get; set; } = new List<Vaccination>();
}
