using Microsoft.AspNetCore.Authentication.JwtBearer;

using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Text;


namespace clinic_project.Extentions
{
    public  static class CustomJWTauth 
    {
        public static void AddJWTextintions(this IServiceCollection service, ConfigurationManager configuration)
        {
            service.AddAuthentication(
                opt =>
                {
                    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

                    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;


                }).AddJwtBearer(opt =>
                {
                    opt.RequireHttpsMetadata = false;
                    opt.SaveToken=true;
                    opt.TokenValidationParameters = new TokenValidationParameters 
                    { 
                        ValidateIssuer = true,
                        ValidIssuer = configuration["JWT:Issuer"],
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]))
                    };


                        

                });


        }
        public static void AddSwaggerGenJwtAuth(this IServiceCollection services)
        {
            services.AddSwaggerGen(o =>
            {
                //o.SwaggerDoc("v1", new OpenApiInfo()
                //{
                //    Version = "v1",
                //    Title = "test api",
                //    Description = "adasdsad",
                //    Contact = new OpenApiContact()
                //    {
                //        Name = "al Mohamady",
                //        Email = "ahmed@gmail.com",
                //        Url = new Uri("https://mydomain.com")
                //    }
                //});

                o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter the JWT Key"

                });
                o.AddSecurityRequirement(doc => new OpenApiSecurityRequirement

                {
                    {
                        new OpenApiSecuritySchemeReference("Bearer"),
                        new List<string>()
                    }
                });

            });

            }
    }
}


