using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clinic_project.Models
{
    public class MedicalService
    {
        [Key]
        public int id { get; set; }
        public string ServiceName{ get; set; }
        public string? description { get; set; }
        public int price { get; set; }
        [Required]
        public int ClinicId { get; set; }
        [ForeignKey("ClinicId")]
        public Clinic clinic { get; set; }


    }
}
