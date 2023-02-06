using System.Security.Claims;

namespace DatingApp.API.Extensions;

public static class ControllerExtension
{
    /// <summary>
    ///     Check if requestor id is same as passed in parameter
    /// </summary>
    /// <param name="principal">HttpContext.User</param>
    /// <param name="id">Passed user id</param>
    /// <returns>Boolean</returns>
    public static bool IsSameAsRequestor(this ClaimsPrincipal principal, int id)
    {
        var value = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return value != null && id == int.Parse(value);
    }
}