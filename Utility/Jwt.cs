using Api.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.Utility
{
    public static class Jwt
    {
        /// <summary>
        /// Generates the JWT token used to authorize the user.
        /// </summary>
        /// <param name="config">Configuration to get JWT settings from appsettings.</param>
        /// <param name="user">User model generate claims in the JWT</param>
        /// <returns>JWT as a string.</returns>
        public static string GenerateJwt(IConfiguration config, UserModel user)
        {
            var settings = config.GetSection("Jwt").Get<JwtSettings>();
            var key = Encoding.ASCII.GetBytes(settings.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("validated", "true"),
                    new Claim("name", user.Username),
                    new Claim("userID", user.UserID.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(3),
                Issuer = settings.Issuer,
                Audience = settings.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;
        }
    }
}
