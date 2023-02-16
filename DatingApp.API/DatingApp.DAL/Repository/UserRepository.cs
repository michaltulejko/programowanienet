using DatingApp.DAL.Helpers;
using DatingApp.DAL.Repository.Interfaces;
using DatingApp.Models.Database.DataModel;
using DatingApp.Models.Dtos.User;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.DAL.Repository;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(DataContext context) : base(context)
    {
    }

    public async Task<User> Register(User user, string password)
    {
        PasswordHelper.CreatePasswordHash(password, out var passwordHash, out var passwordSalt);

        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;

        await Context.Users.AddAsync(user);
        await Context.SaveChangesAsync();

        return user;
    }

    public async Task<User?> Login(string? username, string password)
    {
        var user = await Context.Users.Include(u => u.Photos)
            .FirstOrDefaultAsync(x => x.Username == username);

        return user == null ? null :
            !PasswordHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt) ? null : user;
    }

    public async Task<bool> UserExistsAsync(string username)
    {
        return await Context.Users.AnyAsync(x => x.Username == username);
    }

    public async Task<PagedList<User>> GetPaged(UserParams @params)
    {
        var users = Context.Users.Include(u => u.Photos);

        return await PagedList<User>.CreateAsync(users, @params.PageNumber, @params.PageSize);
    }

    public override async Task<User?> GetByIdAsync(int id)
    {
        var user = await Context.Users.Include(x => x.Photos).FirstOrDefaultAsync(x => x.Id == id);

        return user;
    }

    public override IQueryable<User> GetAll()
    {
        return Context.Users.Include(x => x.Photos);
    }
}