namespace Api.Models 
{
    public class UpdateCredentials {
        public string? NewUsername { get; set; }
        public string? OldUsername { get; set; }
        public string OldPassword { get; set; }
        public string? NewPassword { get; set; }
    }
}