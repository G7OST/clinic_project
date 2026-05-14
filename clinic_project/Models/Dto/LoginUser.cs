using System.ComponentModel.DataAnnotations;

namespace clinic_project.Models.Dto
{
    public class LoginUser
    {
        [Required]
        [EmailAddress]
        public String Email { get; set; }
        public string Password { get; set; }
    }
}
