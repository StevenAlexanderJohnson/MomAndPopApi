public class UserImage
{
    public Int64 UserId { get; set; }
    public List<IFormFile> Image { get; set; } = new List<IFormFile>();
    public string ImageType { get; set; } = "";
}
