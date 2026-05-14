using Microsoft.AspNetCore.Identity;

namespace clinic_project.Extentions
{
    public  static class DbInitializer
    {
        
            public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
            {
                string[] roles = { "User", "ClinicOwner" };

                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }
            }
        }
    }

