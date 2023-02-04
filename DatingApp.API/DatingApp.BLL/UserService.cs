using DatingApp.BLL.Services.Interfaces;
using DatingApp.Models.Dtos.User;

namespace DatingApp.BLL;

public class UserService : IUserService
{
    public Task<List<UserForDetailsDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<UserForDetailsDto> GetUserAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateUserAsync(int id, UserForUpdateDto userForUpdate)
    {
        throw new NotImplementedException();
    }

    public Task<UserForDetailsDto> RegisterAsync(UserForRegisterDto user)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UserExistsAsync(string username)
    {
        throw new NotImplementedException();
    }

    public Task<AuthenticatedUserDto> LoginAsync(UserForLoginDto user)
    {
        throw new NotImplementedException();
    }

    public Task UpdateUserActivityAsync(int userId)
    {
        throw new NotImplementedException();
    }
}