using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MillionTimesVaccinationsApp.Models;

[Table("MessagesAfterVaccination")]
public partial class MessagesAfterVaccination
{
    [Key]
    public int MessageId { get; set; }

    public string Description { get; set; } = null!;

    public DateTime Date { get; set; }

    public string? Recommendations { get; set; }

    public string Doctor { get; set; } = null!;
}
