using Microsoft.AspNetCore.Identity;

namespace DatingApp.Models.Database.DataModel;

public class AppRole : IdentityRole<int>
{
    public ICollection<AppUserRole> UserRoles { get; set; }
}