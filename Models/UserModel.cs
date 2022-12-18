namespace Api.Models
{
    public class UserModel
    {
        internal string Username { get; set; }
        internal string Password { get; set; }
        internal string Roles { get; set; }
        public Int64 UserID { get; set; }
        public string? RefreshToken {get; set;}
    }
}
