using Microsoft.AspNetCore.Http;

namespace DatingApp.Models.Dtos.Photo;

public class PhotoForCreationDto
{
    public string? Url { get; set; }
    public IFormFile? File { get; set; }
    public string? Description { get; set; }
    public DateTime DateAdded => DateTime.Now;
    public string? PublicId { get; set; }
}