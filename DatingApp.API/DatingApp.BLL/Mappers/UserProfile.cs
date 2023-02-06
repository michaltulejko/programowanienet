using AutoMapper;
using DatingApp.BLL.Extensions;
using DatingApp.Models.Database.DataModel;
using DatingApp.Models.Dtos.User;

namespace DatingApp.BLL.Mappers;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserForRegisterDto>(MemberList.None);
        CreateMap<UserForRegisterDto, User>(MemberList.None);
        CreateMap<User, UserForListDto>(MemberList.None)
            .ForMember(dest => dest.Age,
                option => option.MapFrom(src => src.DateOfBirth.GetYears()))
            .ForMember(dest => dest.PhotoUrl,
                options => options.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain)!.Url));
        CreateMap<User, UserForDetailsDto>(MemberList.None)
            .ForMember(dest => dest.Age,
                option => option.MapFrom(src => src.DateOfBirth.GetYears()))
            .ForMember(dest => dest.PhotoUrl,
                options => options.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain)!.Url));
        CreateMap<UserForUpdateDto, User>(MemberList.None);
    }
}