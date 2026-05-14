using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clinic_project.Models.Dto
{
    public class ProductDto
    {
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        [Required]
        [Column(TypeName = "decimal(5,2)")]
        public decimal Price { get; set; }
        [Required]
        public int QuantityStock { get; set; }
    }
}
