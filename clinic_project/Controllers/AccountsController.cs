using clinic_project.Models;
using clinic_project.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Numerics;
using System.Security.Claims;
using System.Text;

namespace clinic_project.Controllers
{
    [Route("api/Users")]
    [ApiController]
    public class AccountsController : ControllerBase
    {

        
        public AccountsController(UserManager<AppUser> userManager,IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;

        [HttpPost("UserRegister")]
        public async Task<IActionResult> RegisterUser(RegisterDto registerDto)
        {
            if (ModelState.IsValid)
            {

                AppUser user = new()
                {
                    UserName = registerDto.Username,
                    Email = registerDto.Email

                };
                
                IdentityResult result = await _userManager.CreateAsync(user, registerDto.Password);
                if (result.Succeeded)
                {
                   
                    await _userManager.AddToRoleAsync(user, registerDto.UserRole);

                    return Ok("User registered and role assigned successfully!");
                }
                foreach (var errors in result.Errors)
                {
                    ModelState.AddModelError("", errors.Description);
                }
                
            }
            return BadRequest(ModelState); 


        }
        [HttpPost("Userlogin")]
        public async Task<IActionResult> Login_User(LoginUser loginUser)
        {

            if (ModelState.IsValid)
            {
                AppUser? user = await _userManager.FindByEmailAsync(loginUser.Email);
               if (user != null)
                {
                    if (await _userManager.CheckPasswordAsync(user, loginUser.Password))
                    {
                        var claims = new List<Claim>();
                        claims.Add(new Claim(ClaimTypes.Name,user.UserName));
                        claims.Add(new Claim(ClaimTypes.NameIdentifier,user.Id));
                        claims.Add(new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()));
                        
                        var roles = await _userManager.GetRolesAsync(user);
                        foreach(var role in roles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, role.ToString()));

                        }
                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
                        var sc = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var token = new JwtSecurityToken(
                            claims: claims,
                            issuer: _configuration["JWT:Issuer"],
                            audience: _configuration["JWT:Audience"],
                            expires:DateTime.Now.AddHours(1),
                            signingCredentials: sc
                        );
                        var _token = new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration=token.ValidTo
                        };

                        return Ok(_token);
                    }
                }
                else { return Unauthorized(); }

            }
            return BadRequest(ModelState);
        }
    } 
}
                            





                        
                        



