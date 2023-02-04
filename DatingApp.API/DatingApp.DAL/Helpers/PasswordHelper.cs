using System.Security.Cryptography;
using System.Text;

namespace DatingApp.DAL.Helpers;

public static class PasswordHelper
{
    public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512();
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }

    public static bool VerifyPasswordHash(string password, byte[] userPasswordHash, byte[] userPasswordSalt)
    {
        using var hmac = new HMACSHA512(userPasswordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

        return !computedHash.Where((t, i) => t != userPasswordHash[i]).Any();
    }
}