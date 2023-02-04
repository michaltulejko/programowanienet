using DatingApp.DAL.Repository.Interfaces;
using DatingApp.Models.Database.DataModel;

namespace DatingApp.DAL.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DataContext context) : base(context)
        {
        }


    }
}
