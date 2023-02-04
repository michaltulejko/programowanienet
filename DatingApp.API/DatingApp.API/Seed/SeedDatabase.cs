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
            var userData = System.IO.File.ReadAllText("Seed/UserData.json");
            var userList = JsonConvert.DeserializeObject<List<User>>(userData);

            foreach(var user in userList)
            {
                PasswordHelper.CreatePasswordHash("password", out var passwordHash, out var passwordSalt);
                user.PasswordSalt = passwordSalt;
                user.PasswordHash = passwordHash;
                user.Username = user.Username.ToLower();
            }

            context.AddRange(userList);
            context.SaveChanges();
        }
    }
}