using System.Security.Claims;
using DatingApp.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DatingApp.API.Helpers;

public class LogUserActivityFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var resultContext = await next();

        var value = resultContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (value != null)
        {
            var userId = int.Parse(value);
            var userService = resultContext.HttpContext.RequestServices.GetService<IUserService>();

            await userService!.UpdateUserActivityAsync(userId);
        }
    }
}