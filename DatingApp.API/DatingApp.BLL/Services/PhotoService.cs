using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DatingApp.BLL.Services.Interfaces;
using DatingApp.DAL.Repository.Interfaces;
using DatingApp.Models.Configuration;
using DatingApp.Models.Database.DataModel;
using DatingApp.Models.Dtos.Photo;
using Microsoft.Extensions.Options;

namespace DatingApp.BLL.Services;

public class PhotoService : IPhotoService
{
    private readonly Cloudinary _cloudinary;
    private readonly IMapper _mapper;
    private readonly IPhotoRepository _photoRepository;
    private readonly IUserRepository _userRepository;

    public PhotoService(
        IPhotoRepository photoRepository,
        IUserRepository userRepository,
        IMapper mapper,
        IOptions<CloudStorageConfig> cloudStorageOptions)
    {
        _photoRepository = photoRepository;
        _userRepository = userRepository;
        _mapper = mapper;

        var account = new Account(
            cloudStorageOptions.Value.CloudName,
            cloudStorageOptions.Value.ApiKey,
            cloudStorageOptions.Value.ApiSercret);

        _cloudinary = new Cloudinary(account);
    }

    public async Task<PhotoForReturnDto> UploadPhotoAsync(int userId, PhotoForCreationDto photo)
    {
        try
        {
            var user = await _userRepository.GetByIdAsync(userId);
            var uploadResult = new ImageUploadResult();

            if (photo.File is { Length: > 0 })
            {
                await using var stream = photo.File.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(photo.File.Name, stream),
                    Transformation = new Transformation().Width(500).Height(500).Crop("fill").Gravity("face")
                };

                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }

            photo.Url = uploadResult.Url.ToString();
            photo.PublicId = uploadResult.PublicId;
            var photoEntity = _mapper.Map<PhotoForCreationDto, Photo>(photo, options => options.AfterMap((src, dest) =>
            {
                if (user != null && !user.Photos.Any(p => p.IsMain)) dest.IsMain = true;
            }));

            user?.Photos.Add(photoEntity);
            await _photoRepository.SaveChangesAsync();

            return _mapper.Map<PhotoForReturnDto>(photoEntity);
            ;
        }
        catch (Exception ex)
        {
            throw new Exception("Photo could not be upload", ex);
        }
    }

    public async Task<PhotoForReturnDto> GetPhotoAsync(int id)
    {
        var result = await _photoRepository.GetByIdAsync(id);

        return _mapper.Map<PhotoForReturnDto>(result);
    }

    public async Task<PhotoForReturnDto> SetPhotoAsMainAsync(int userId, int photoId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        var photo = user!.Photos.FirstOrDefault(p => p.Id == photoId);

        if (photo == null) throw new Exception("Photo not found");

        if (photo.IsMain) throw new Exception("Photo is already main photo");

        foreach (var userPhoto in user.Photos.Where(p => p.Id != photoId)) userPhoto.IsMain = false;

        photo.IsMain = true;

        await _userRepository.SaveChangesAsync();
        return _mapper.Map<PhotoForReturnDto>(photo);
    }

    public async Task RemoveUserPhotoAsync(int userId, int photoId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        var photoToDelete = user!.Photos.FirstOrDefault(p => p.Id == photoId);

        if (photoToDelete == null) throw new Exception("User's profile does not contain given photo");

        try
        {
            var deletionParams = new DeletionParams(photoToDelete.PublicId);
            var deletionResult = await _cloudinary.DestroyAsync(deletionParams);

            if (deletionResult.Result == "ok")
                _photoRepository.Delete(photoToDelete);
            else
                throw new Exception("Could not remove photo");

            await _photoRepository.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Could not remove photo", ex);
        }
    }
}