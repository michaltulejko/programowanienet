using Microsoft.AspNetCore.Identity;

namespace DatingApp.Models.Database.DataModel;

public class AppUserRole : IdentityUserRole<int>
{
    public AppUser User { get; set; }
    public AppRole Role { get; set; }
}