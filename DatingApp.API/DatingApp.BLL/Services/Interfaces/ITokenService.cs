using DatingApp.Models.Database.DataModel;

namespace DatingApp.BLL.Services.Interfaces;

public interface ITokenService
{
    Task<string> CreateToken(AppUser user);
}