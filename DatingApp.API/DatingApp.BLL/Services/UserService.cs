using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DatingApp.BLL.Services.Interfaces;
using DatingApp.DAL.Repository.Interfaces;
using DatingApp.Models.Configuration;
using DatingApp.Models.Database.DataModel;
using DatingApp.Models.Dtos.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.BLL.Services;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly IOptions<AppSettings> _settings;
    private readonly IUserRepository _userRepository;

    public UserService(
        IUserRepository userRepository,
        IMapper mapper,
        IOptions<AppSettings> settings)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _settings = settings;
    }

    public async Task<List<UserForDetailsDto>> GetAllAsync()
    {
        var users = await _userRepository.GetAll().ProjectTo<UserForDetailsDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return users;
    }

    public async Task<UserForDetailsDto> GetUserAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        var result = _mapper.Map<UserForDetailsDto>(user);

        return result;
    }

    public async Task UpdateUserAsync(int id, UserForUpdateDto userForUpdate)
    {
        var userToUpdate = await _userRepository.GetByIdAsync(id);
        _mapper.Map(userForUpdate, userToUpdate);

        try
        {
            await _userRepository.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Updating user failed on save", ex);
        }
    }

    public async Task<UserForDetailsDto> RegisterAsync(UserForRegisterDto user)
    {
        return _mapper.Map<UserForDetailsDto>(await _userRepository.Register(_mapper.Map<User>(user),
            user.Password));
    }

    public async Task<bool> UserExistsAsync(string username)
    {
        return await _userRepository.UserExistsAsync(username);
    }

    public async Task<AuthenticatedUserDto?> LoginAsync(UserForLoginDto user)
    {
        var loggedInUser = await _userRepository.Login(user.Username, user.Password);

        if (loggedInUser == null) return null;

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, loggedInUser.Id.ToString()),
            new Claim(ClaimTypes.Name, loggedInUser.Username)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Value.Token!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Today.AddDays(1),
            SigningCredentials = credentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return new AuthenticatedUserDto
        {
            Token = tokenHandler.WriteToken(token),
            User = _mapper.Map<UserForListDto>(loggedInUser)
        };
    }

    public async Task UpdateUserActivityAsync(int userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        user!.LastActive = DateTime.Now;

        await _userRepository.SaveChangesAsync();
    }
}