using clinic_project.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace clinic_project.Data
{
    public class AppDbContext :IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options) { }
        public DbSet<Product> Product { get; set; }
        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<MedicalService> MedicalServices {  get; set; }
    }
}
