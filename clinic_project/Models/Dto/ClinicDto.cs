using System.ComponentModel.DataAnnotations;

namespace clinic_project.Models.Dto
{
    public class ClinicDto
    {
        [Required]
        public string ClinicName { get; set; }
        [Required]
        public string ClinicAddress { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        

    }
}
