using DatingApp.Models.Dtos.User;

namespace DatingApp.BLL.Services.Interfaces;

public interface IUserService
{
    Task<List<UserForDetailsDto>> GetAllAsync();
    Task<UserForDetailsDto> GetUserAsync(int id);
    Task UpdateUserAsync(int id, UserForUpdateDto userForUpdate);
    Task<UserForDetailsDto> RegisterAsync(UserForRegisterDto user);
    Task<bool> UserExistsAsync(string username);
    Task<AuthenticatedUserDto?> LoginAsync(UserForLoginDto user);
    Task UpdateUserActivityAsync(int userId);
}