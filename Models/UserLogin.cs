namespace Api.Models
{
    public class UserLogin
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string RedirectUrl { get; set; } = "/";
    }
}
