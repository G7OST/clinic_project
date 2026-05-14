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
    [Route("api/Product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public ProductController(AppDbContext db) { _db = db; }
        private readonly AppDbContext _db;
       
        [HttpPost]
        [Authorize(Roles = "ClinicOwner")]
        public async Task<IActionResult> AddProduct(ProductDto productDto) 
        {
            if (ModelState.IsValid)
            {
                var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var clinic = await _db.Clinics.FirstOrDefaultAsync(op => op.OwnerId == userid);
                if (clinic == null)
                {
                    return BadRequest("You must create a clinic before adding products.");
                }
                Product product1 = new()
                {
                    Name = productDto.Name,
                    Description = productDto.Description,
                    Price = productDto.Price,
                    QuantityStock = productDto.QuantityStock,
                    ClinicId = clinic.Id,
                };
                await _db.Product.AddAsync(product1);
               await _db.SaveChangesAsync();
                return Ok(productDto);
            }
            return BadRequest(ModelState);
        }
        [HttpGet("GetProductById")]
        public async Task<IActionResult> GetProductsById(int id) { 
            var Products=await _db.Product.Where(op=> op.ClinicId==id).ToListAsync();
            return Ok(Products);
        
        }
        [HttpGet("getAllProducts")]
        public async Task<IActionResult> GetAllServices()
        {
            return Ok(await _db.Product.ToListAsync());
        }
    }
}
            
