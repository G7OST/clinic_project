using System.ComponentModel.DataAnnotations;

namespace clinic_project.Models.Dto
{
    public class MedicalDto
    {
        
        [Required]

        public string ServiceName { get; set; }
        

        public string? description { get; set; }
        [Required]

        public int price { get; set; }
    }
}
