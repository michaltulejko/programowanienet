namespace DatingApp.BLL.Services.Interfaces;

public class AuthenticatedUserDto
{
    public string Token { get; set; }
    public UserForListDto User { get; set; }
}