using Microsoft.AspNetCore.Identity;
using Talabat.Core.Entites.Identity;

namespace Talabat.Core.Services;
public interface ITokenService
{
    Task<string> CreateTokenAsync(AppUser User, UserManager<AppUser> userManager);
}
