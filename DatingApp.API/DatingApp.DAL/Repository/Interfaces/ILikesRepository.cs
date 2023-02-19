using DatingApp.DAL.Helpers;
using DatingApp.Models.Database.DataModel;
using DatingApp.Models.Dtos;

namespace DatingApp.DAL.Repository.Interfaces;

public interface ILikesRepository
{
    Task<UserLike> GetUserLike(int sourceUserId, int targetUserId);
    Task<AppUser> GetUserWithLikes(int userId);
    Task<PagedList<LikeDto>> GetUserLikes(LikesParams likesParams);
}