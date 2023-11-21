using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MillionTimesVaccinationsApp.ViewModels
{
    public class UserViewModel
    {
        public string? Id { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        public string? UserName { get; set; }

        [Required]
        public string? Role { get; set; }

        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
