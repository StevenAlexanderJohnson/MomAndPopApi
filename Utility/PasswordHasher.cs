using System.Security.Cryptography;
using System.Text;

namespace Api.Utility
{
    public static class PasswordHasher
    {
        public static string HashPassword(string password)
        {
            var password_hash = "";
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach(byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                password_hash = builder.ToString();
            }
            return password_hash;
        }
    }
}
