using System.ComponentModel.DataAnnotations;

namespace DatingApp.Models.Dtos.User
{
    public class UserForRegisterDto
    {

        private string _userName;

        [Required]
        [StringLength(32, MinimumLength = 4, ErrorMessage = "You must specify username between 4 and 32 characters")]
        public string Username
        {
            get => _userName?.ToLower();
            set => _userName = value;
        }

        [Required]
        [StringLength(32, MinimumLength = 5, ErrorMessage = "You must specify password between 5 and 32 characters")]
        public string Password { get; set; }

        [Required] public string Gender { get; set; }

        [Required] public string KnownAs { get; set; }

        [Required] public DateTime DateOfBirth { get; set; }

        [Required] public string City { get; set; }

        [Required] public string Country { get; set; }

        public DateTime Created => DateTime.Now;

        public DateTime LastActive => DateTime.Now;
    }
}
