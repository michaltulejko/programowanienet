namespace DatingApp.Models.Dtos.User;

public class AuthenticatedUserDto
{
    public string? Token { get; set; }
    public UserForListDto? User { get; set; }
}