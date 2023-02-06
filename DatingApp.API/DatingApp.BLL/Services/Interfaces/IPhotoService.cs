using DatingApp.Models.Dtos.Photo;

namespace DatingApp.BLL.Services.Interfaces;

public interface IPhotoService
{
    Task<PhotoForReturnDto> UploadPhotoAsync(int userId, PhotoForCreationDto photo);
    Task<PhotoForReturnDto> GetPhotoAsync(int id);
    Task<PhotoForReturnDto> SetPhotoAsMainAsync(int userId, int photoId);
    Task RemoveUserPhotoAsync(int userId, int photoId);
}