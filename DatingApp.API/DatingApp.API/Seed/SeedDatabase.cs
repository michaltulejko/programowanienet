using DatingApp.DAL.Helpers;
using DatingApp.Models.Database.DataModel;
using Newtonsoft.Json;

namespace DatingApp.API.Seed;

public static class SeedDatabase
{
    public static void SeedUsers(DataContext context)
    {
        if (!context.Users.Any())
        {
            var userData = File.ReadAllText("Seed/UserData.json");
            var userList = JsonConvert.DeserializeObject<List<User>>(userData);
            if (userList != null)
            {
                foreach (var user in userList)
                {
                    PasswordHelper.CreatePasswordHash("password", out var passwordHash, out var passwordSalt);
                    user.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordSalt;
                    user.Username = user.Username.ToLower();
                }

                context.Users.AddRange(userList);
            }

            context.SaveChanges();
        }

        if (context.Photos.Count() > 1000) return;

        var photoData = File.ReadAllText("Seed/PhotoData.json");
        var photoList = JsonConvert.DeserializeObject<List<Photo>>(photoData);

        if (photoList != null) context.Photos.AddRange(photoList);

        context.SaveChanges();
    }
}