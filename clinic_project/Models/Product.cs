using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clinic_project.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        [Required]
        [Column(TypeName ="decimal(5,2)")]    
        public decimal Price { get; set; }
        [Required]
        public int QuantityStock { get; set; }
        [Required]
        public int ClinicId { get; set; }
        [ForeignKey("ClinicId")]
        public Clinic? clinic { get; set; }


    }
}
