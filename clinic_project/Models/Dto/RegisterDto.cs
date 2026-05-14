using System.ComponentModel.DataAnnotations;

namespace clinic_project.Models.Dto
{
    public class RegisterDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [MaxLength(20)]
        public string Password { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string? Phone { get; set; }
        public string? UserRole { get; set; }
    }
}
