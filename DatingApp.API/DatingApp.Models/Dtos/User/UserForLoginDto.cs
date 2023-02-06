using System.ComponentModel.DataAnnotations;

namespace DatingApp.Models.Dtos.User;

public class UserForLoginDto
{
    private string _userName;

    [Required]
    public string Username
    {
        get => _userName.ToLower();
        set => _userName = value;
    }

    [Required] public string Password { get; set; }
}