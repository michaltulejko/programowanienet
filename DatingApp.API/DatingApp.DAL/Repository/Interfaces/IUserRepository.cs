using DatingApp.DAL.Helpers;
using DatingApp.Models.Database.DataModel;
using DatingApp.Models.Dtos.User;

namespace DatingApp.DAL.Repository.Interfaces;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User> Register(User user, string password);
    Task<User?> Login(string? username, string password);
    Task<bool> UserExistsAsync(string username);
    Task<PagedList<User>> GetPaged(UserParams @params);
}