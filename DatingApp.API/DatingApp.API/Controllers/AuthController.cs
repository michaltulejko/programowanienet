using DatingApp.BLL.Services.Interfaces;
using DatingApp.Models.Dtos.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [AllowAnonymous]
    [ProducesResponseType(typeof(UserForDetailsDto), StatusCodes.Status200OK)]
    [HttpPost("register")]
    public async Task<IActionResult> Register(UserForRegisterDto user)
    {
        if (await _userService.UserExistsAsync(user.Username)) return BadRequest("Username already taken");

        var registeredUser = await _userService.RegisterAsync(user);

        return CreatedAtRoute("GetUser", new { controller = "User", id = registeredUser.Id }, registeredUser);
    }

    [AllowAnonymous]
    [ProducesResponseType(typeof(AuthenticatedUserDto), StatusCodes.Status200OK)]
    [HttpPost("login")]
    public async Task<IActionResult> Login(UserForLoginDto user)
    {
        var authUser = await _userService.LoginAsync(user);

        if (authUser == null) return Unauthorized();

        return Ok(authUser);
    }
}