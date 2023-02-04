using DatingApp.DAL.Repository.Interfaces;
using DatingApp.Models.Database.DataModel;

namespace DatingApp.DAL.Repository;

public class PhotoRepository : BaseRepository<Photo>, IPhotoRepository
{
    public PhotoRepository(DataContext context) : base(context)
    {
    }
}