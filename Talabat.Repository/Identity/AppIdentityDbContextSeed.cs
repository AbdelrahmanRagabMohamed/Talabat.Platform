using Microsoft.AspNetCore.Identity;
using Talabat.Core.Entites.Identity;

namespace Talabat.Repository.Identity;
public static class AppIdentityDbContextSeed
{

    public static async Task SeedUserAsync(UserManager<AppUser> userManager)
    {
        if (!userManager.Users.Any())
        {
            var User = new AppUser()
            {
                DisplayName = "Abdel-Rahman Ragab",
                Email = "aboayta8@gmail.com",
                UserName = "abdelrahmanragab",
                PhoneNumber = "01277892600"
            };

            // Add User in Db
            await userManager.CreateAsync(User, "Pa$$w0rd");
        }
    }
}
