using DatingApp.API.Extensions;
using DatingApp.API.Helpers;
using DatingApp.BLL.Services.Interfaces;
using DatingApp.Models.Dtos.User;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers;

[ServiceFilter(typeof(LogUserActivityFilter))]
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("getusers")]
    public async Task<IActionResult> GetUsers()
    {
        return Ok(await _userService.GetAllAsync());
    }

    [ProducesResponseType(typeof(UserForDetailsDto), StatusCodes.Status200OK)]
    [HttpGet("{id:int}", Name = "GetUser")]
    public async Task<IActionResult> GetUser(int id)
    {
        return Ok(await _userService.GetUserAsync(id));
    }

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateUser(int id, UserForUpdateDto user)
    {
        if (!User.IsSameAsRequestor(id)) return Unauthorized();

        await _userService.UpdateUserAsync(id, user);

        return NoContent();
    }
}