using Microsoft.AspNetCore.Identity;

namespace Talabat.Core.Entites.Identity;
public class AppUser : IdentityUser
{
    public string DisplayName { get; set; }

    public Address Address { get; set; }
}
