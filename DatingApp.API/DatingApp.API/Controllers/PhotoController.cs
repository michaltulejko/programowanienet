using DatingApp.API.Extensions;
using DatingApp.BLL.Services.Interfaces;
using DatingApp.Models.Dtos.Photo;
using DatingApp.Models.Dtos.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers;

[Authorize]
[Route("api/users/{userId}/photos")]
[ApiController]
public class PhotoController : ControllerBase
{
    private readonly IPhotoService _photoService;

    public PhotoController(IPhotoService photoService)
    {
        _photoService = photoService;
    }

    [ProducesResponseType(typeof(PhotoForReturnDto), StatusCodes.Status200OK)]
    [HttpGet("{id:int}", Name = "GetPhoto")]
    public async Task<IActionResult> GetPhoto(int id)
    {
        var photo = await _photoService.GetPhotoAsync(id);

        return Ok(photo);
    }

    [ProducesResponseType(typeof(PhotoForReturnDto), StatusCodes.Status200OK)]
    [HttpPost]
    public async Task<IActionResult> AddPhotoForUser(int userId, [FromForm] PhotoForCreationDto photo)
    {
        if (!User.IsSameAsRequestor(userId)) return Unauthorized();

        var uploadedPhoto = await _photoService.UploadPhotoAsync(userId, photo);
        return CreatedAtRoute("GetPhoto", new { userId, id = uploadedPhoto.Id }, uploadedPhoto);
    }

    [ProducesResponseType(typeof(PhotoForReturnDto), StatusCodes.Status200OK)]
    [HttpPut("{photoId:int}/setMain")]
    public async Task<IActionResult> SetPhotoAsMain(int userId, int photoId)
    {
        if (!User.IsSameAsRequestor(userId)) return Unauthorized();

        var photo = await _photoService.SetPhotoAsMainAsync(userId, photoId);
        return CreatedAtRoute("GetPhoto", new { userId, id = photo.Id }, photo);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpDelete("{photoId:int}")]
    public async Task<IActionResult> DeletePhoto(int userId, int photoId)
    {
        if (!User.IsSameAsRequestor(userId)) return Unauthorized();

        await _photoService.RemoveUserPhotoAsync(userId, photoId);

        return Ok();
    }
}