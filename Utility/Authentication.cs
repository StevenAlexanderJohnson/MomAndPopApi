using Api.DataServices.Interfaces;
using Api.Models;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace Api.Utility
{
    public static class Authentication
    {
        /// <summary>
        /// Validates user credentials using <paramref name="userLogin"/>.
        /// </summary>
        /// <param name="userLogin">Login credentials for the user</param>
        /// <param name="userDataService">Service that talks to the Social database</param>
        /// <param name="authDataService">Service that talks to the Auth database</param>
        /// <returns>UserModel that contains the user information and refresh token.</returns>
        public static async Task<UserModel> Authenticate(UserLogin userLogin, IUserDataService userDataService, IAuthDataService authDataService)
        {
            bool valid = await authDataService.ValidateUserCredentialsAsync(userLogin);
            if (!valid)
            {
                return null;
            }
            User userData = (await userDataService.GetUsersByUsernameAsync(userLogin.UserName)).FirstOrDefault()!;
            if (userData == null)
            {
                return null;
            }
            // NEED TO DETERMINE HOW TO REFRESH THE AUTH TOKEN WITHOUT THE REFRESH TOKEN
            var newRefreshToken = GenerateRefreshToken();
            await authDataService.UpdateRefreshToken(userData.Username, newRefreshToken);
            return new UserModel() { Username = userLogin.UserName, Password = userLogin.Password, Roles = "validated", UserID = userData.Id, RefreshToken = newRefreshToken };
        }

        /// <summary>
        /// Refreshes the user's Auth Token using <paramref name="refreshToken"/>.
        /// </summary>
        /// <param name="refreshToken">The current refresh token passed by the requesting user.</param>
        /// <param name="userDataService">Service that talks to the Social database</param>
        /// <param name="authDataService">Service that talks to the Auth database</param>
        /// <returns>UserModel that contains the username and id of the user to generate a new JWT.</returns>
        /// <exception cref="Exception"></exception>
        public static async Task<UserModel> RefreshAuthToken(string refreshToken, IUserDataService userDataService, IAuthDataService authDataService)
        {
            string username = await authDataService.ValidateRefreshTokenAsync(refreshToken);
            if (string.IsNullOrEmpty(username))
            {
                throw new SecurityTokenException("Invalid refresh token");
            }

            User userData = (await userDataService.GetUsersByUsernameAsync(username)).FirstOrDefault()!;
            if(userData == null)
            {
                throw new Exception("Error retreiving user information");
            }
            string newRefreshToken = GenerateRefreshToken();
            await authDataService.UpdateRefreshToken(username, newRefreshToken);
            return new UserModel() { Username = username, UserID = userData.Id, RefreshToken = newRefreshToken };
        }

        public static string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(46));
        }
    }
}
