using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Numerics;

public class Post 
{
    public Int64 Id { get; set; }
    [FromForm(Name = "Image")]
    public List<IFormFile> Image { get; set; }
    public string ImageType { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Int64 UserId { get; set; }
}