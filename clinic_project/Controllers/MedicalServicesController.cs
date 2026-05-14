using clinic_project.Data;
using clinic_project.Models;
using clinic_project.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace clinic_project.Controllers
{
    [Route("api/Services")]
    [ApiController]
    public class MedicalServicesController : ControllerBase
    {
        public MedicalServicesController(AppDbContext db) { _db = db; }
        private readonly AppDbContext _db;
        [HttpGet("getAllServices")]
        public async Task<IActionResult> GetAllServices() 
        {
            return Ok(await _db.MedicalServices.ToListAsync());
        }
        [HttpPost]
        //[Authorize(Roles ="ClinicOwner")]
        public async Task<IActionResult> AddService(MedicalDto medical)
        {
            if (ModelState.IsValid)
            {
                var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var clinic = await _db.Clinics.FirstOrDefaultAsync(op => op.OwnerId == userid);
                if (clinic == null)
                {
                    return BadRequest("You must create a clinic before adding Medical Services.");
                }
                MedicalService medicalService= new()
                {
                    
                    ServiceName = medical.ServiceName,
                    price = medical.price,
                    description = medical.description,
                    ClinicId = clinic.Id,
                };
                await _db.MedicalServices.AddAsync(medicalService);
                await _db.SaveChangesAsync();
                return Ok(medical);
            }
            return BadRequest(ModelState);
        }
        [HttpGet("GetProductById")]
        public async Task<IActionResult> GetProductsById(int id)
        {
            var medicalService= await _db.MedicalServices.Where(op => op.ClinicId == id).ToListAsync();
            return Ok(medicalService);

        }





    }
}
