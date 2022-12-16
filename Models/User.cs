namespace Api.Models
{
    public class User
    {
        public Int64 Id { get; set; }
        public string FirstName { get; set; }
        public string Username { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public bool Verified { get; set; }
        public string Password { get; set; } = "";
    }
}