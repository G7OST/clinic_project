using clinic_project.Data;
using clinic_project.Models;
using clinic_project.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Security.Claims;

namespace clinic_project.Controllers
{
    [Route("api/Clinic")]
    [ApiController]
    public class ClinicController : ControllerBase
    {
        public ClinicController(AppDbContext db)
        {
            _db = db;
        }
        private readonly AppDbContext _db;
      
       // [Authorize(Roles ="ClinicOwner")]
        [HttpPost("Create_Clinic")]

        public async Task<IActionResult>CreateClinic(ClinicDto clinicDto)
        {
            if (ModelState.IsValid)
            {
                var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
                Clinic ExistClinic = await _db.Clinics.FirstOrDefaultAsync(op => op.OwnerId == user);
                if (ExistClinic != null)
                {
                    return BadRequest($"Clinic{ExistClinic}Already exists ");
                }
                else
                {
                    Clinic clinic1 = new()
                    {
                        ClinicName = clinicDto.ClinicName,
                        ClinicAddress = clinicDto.ClinicAddress,
                        PhoneNumber = clinicDto.PhoneNumber,
                        OwnerId = user
                        

                    };


                    await _db.Clinics.AddAsync(clinic1);  
                    await _db.SaveChangesAsync();
                    return Ok(clinic1);
                }
            }
            return BadRequest(ModelState);
            
            

        }
        [HttpGet("GetAllClinics")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public async Task<IActionResult> Getall_Clinics()
        {
            return Ok(await _db.Clinics.ToListAsync());

        }
        [HttpGet("GetClinicById")]
        public async Task<IActionResult>GetClinicById(int id) 
        {
         var clinic=await _db.Clinics.FirstOrDefaultAsync(op=>op.Id == id);
            return Ok(clinic);
        }

    }
}
            

               



