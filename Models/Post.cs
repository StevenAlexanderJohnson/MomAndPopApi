using Microsoft.AspNetCore.Mvc;

public class Post 
{
    public Int64 Id { get; set; }
    [FromForm(Name = "Image")]
    public List<IFormFile> Image { get; set; } = new List<IFormFile>();
    public string ImageType { get; set; } = "";
    public string Title { get; set; }
    public string Description { get; set; }
    public Int64 UserId { get; set; }
}