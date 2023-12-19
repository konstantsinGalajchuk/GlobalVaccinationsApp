using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MillionTimesVaccinationsApp.Models;

[Table("MessagesAfterVaccination")]
public partial class MessagesAfterVaccination
{
    [Key]
    public int MessageId { get; set; }

    [Required]
    [StringLength(250, ErrorMessage = "The 'Description' field must contain no more than 250 characters.")]
    public string Description { get; set; } = null!;

    [Required]
    public DateTime Date { get; set; }

    [StringLength(100, ErrorMessage = "The 'Recommendation' field must contain no more than 100 characters.")]
    public string? Recommendations { get; set; }

    [Required]
    [StringLength(50, ErrorMessage = "The 'Doctor' field must contain no more than 50 characters.")]
    public string Doctor { get; set; } = null!;
}
