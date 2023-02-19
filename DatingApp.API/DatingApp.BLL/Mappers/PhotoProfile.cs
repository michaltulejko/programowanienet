using AutoMapper;
using DatingApp.Models.Database.DataModel;
using DatingApp.Models.Dtos;

namespace DatingApp.BLL.Mappers;

public class PhotoProfile : Profile
{
    public PhotoProfile()
    {
        CreateMap<Photo, PhotoDto>();
    }
}