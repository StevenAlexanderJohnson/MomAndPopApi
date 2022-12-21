using Api.Models;

namespace Api.DataServices.Interfaces
{
    public interface IAuthDataService
    {
        /// <summary>
        /// Creates a record in the Auth database using the username and hashed password.
        /// </summary>
        /// <param name="newUser">Object containing the username and password for the user.</param>
        /// <returns>Nothing</returns>
        public Task<string> CreateUserCredentials(User newUser);

        /// <summary>
        /// Delete the user credentials from the Auth database
        /// </summary>
        /// <param name="user">Object containing the username and password of the user.</param>
        /// <returns>Nothing</returns>
        public Task DeleteUserCredentials(User user);

        /// <summary>
        /// Validate user credentials.
        /// </summary>
        /// <param name="user">Object containing the username and password of the user.</param>
        /// <returns>True if the credentials were correct and false if not.</returns>
        public Task<bool> ValidateUserCredentialsAsync(UserLogin user);

        /// <summary>
        /// Checks if the refresh token is still valid and not expired
        /// </summary>
        /// <param name="refreshToken">Refresh token sent in the request.</param>
        /// <returns>The username of the user found.</returns>
        public Task<string> ValidateRefreshTokenAsync(string refreshToken);

        /// <summary>
        /// Update the refresh token with the new value.
        /// </summary>
        /// <param name="username">Username of the user to update</param>
        /// <param name="newRefreshToken">Value to set the refresk token to.</param>
        /// <returns>Nothing</returns>
        public Task UpdateRefreshToken(string username, string newRefreshToken);

        /// <summary>
        /// Sets the expire time to now to invalidate refresh token.
        /// </summary>
        /// <param name="refreshToken">Refresh token to invalidate</param>
        /// <returns>Nothing</returns>
        public Task ExpireRefreshTokenAsync(string refreshToken);
    }
}
