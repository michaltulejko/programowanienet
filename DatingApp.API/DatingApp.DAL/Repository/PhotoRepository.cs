using DatingApp.DAL.Repository.Interfaces;
using DatingApp.Models.Database.DataModel;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.DAL.Repository;

public class PhotoRepository : BaseRepository<Photo>, IPhotoRepository
{
    public PhotoRepository(DataContext context) : base(context)
    {
    }

    public override async Task<Photo?> GetByIdAsync(int id)
    {
        var user = await Context.Photos.FirstOrDefaultAsync(x => x.Id == id);

        return user;
    }
}