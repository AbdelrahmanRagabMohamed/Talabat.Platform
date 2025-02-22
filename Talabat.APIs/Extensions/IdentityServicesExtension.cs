using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Talabat.Core.Entites.Identity;
using Talabat.Core.Services;
using Talabat.Repository.Identity;
using Talabat.Service;

namespace Talabat.APIs.Extensions;

public static class IdentityServicesExtension
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection Services, IConfiguration configuration)
    {

        Services.AddScoped<ITokenService, TokenService>();

        // Add Interfaces , Classes that Implemnt this Interfaces 
        Services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>();

        // Allow DI For : SignInManeger / UserManger / RoleManger
        Services.AddAuthentication(Options =>
        {
            // To Write [Authorize] Instead of [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]    // فيها مشكلة
            Options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            Options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

        })
                 .AddJwtBearer(Options =>
                 {
                     Options.TokenValidationParameters = new TokenValidationParameters()
                     {
                         ValidateIssuer = true,
                         ValidIssuer = configuration["JWT:ValidIssuer"],
                         ValidateAudience = true,
                         ValidAudience = configuration["JWT:ValidAudience"],
                         ValidateLifetime = true,
                         ValidateIssuerSigningKey = true,
                         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]))

                     };
                 });

        return Services;
    }
}
