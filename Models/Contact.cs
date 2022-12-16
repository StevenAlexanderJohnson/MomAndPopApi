public class Contact
{
    public Contact(Int64 userId, string email, string phone, bool emailVerified, bool phoneVerified)
    {
        UserId = userId;
        Email = email;
        Phone = phone;
        EmailVerified = emailVerified;
        PhoneVerified = phoneVerified;
    }

    public Int64 UserId { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public bool EmailVerified { get; set; }
    public bool PhoneVerified { get; set; }
}