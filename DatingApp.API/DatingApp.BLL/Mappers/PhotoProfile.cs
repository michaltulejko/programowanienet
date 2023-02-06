using AutoMapper;
using DatingApp.Models.Database.DataModel;
using DatingApp.Models.Dtos.Photo;

namespace DatingApp.BLL.Mappers;

public class PhotoProfile : Profile
{
    public PhotoProfile()
    {
        CreateMap<Photo, PhotoForDetailsDto>(MemberList.None);
        CreateMap<PhotoForCreationDto, Photo>(MemberList.None);
        CreateMap<Photo, PhotoForReturnDto>(MemberList.None);
    }
}