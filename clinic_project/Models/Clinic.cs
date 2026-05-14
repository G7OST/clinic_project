using clinic_project.Models.Dto;
using System.ComponentModel.DataAnnotations;

namespace clinic_project.Models
{
    public class Clinic
    {
        [Key]
        
        public int Id { get; set; }
        [Required]
        public string ClinicName { get; set; }
        [Required]
        public string ClinicAddress { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string OwnerId { get; set; }
        public ICollection<Product>? Products { get; set; } =  new  List<Product>();
        public  ICollection<MedicalService>? medicalService { get; set; }= new List<MedicalService>();

    }
}
