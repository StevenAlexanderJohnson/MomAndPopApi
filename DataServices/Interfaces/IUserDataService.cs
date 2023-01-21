using Api.Models;

namespace Api.DataServices.Interfaces
{
    public interface IUserDataService
    {
        /// <summary>
        /// Gets a list of users who's username are equal to parameter
        /// </summary>
        /// <param name="userId">The Id of the user you want to find</param>
        /// <returns>List containing one user who's id matches the parameter.</returns>
        public Task<List<User>> GetUserByIdAsync(Int64 userId);

        /// <summary>
        /// Get a list of users who's username match parameter.
        /// </summary>
        /// <param name="username">Username to search for</param>
        /// <returns>List of Users who's username match search parameter.</returns>
        public Task<List<User>> GetUsersByUsernameAsync(string username);

        /// <summary>
        /// Creates a user in the database.
        /// </summary>
        /// <param name="newUser">User object to be inserted into the database.</param>
        /// <returns>Nothing</returns>
        public Task CreateUserAsync(User newUser);

        /// <summary>
        /// Gets the user's profile image from the database.
        /// </summary>
        /// <param name="userId">User's Id</param>
        /// <returns>Tuple containing the byte data and the file type.</returns>
        public Task<Tuple<byte[], string>> GetUserImageByIdAsync(Int64 userId);

        /// <summary>
        /// Adds user's new profile picture to the database.
        /// </summary>
        /// <param name="userImage">Model containing image information.</param>
        /// <returns>Nothing</returns>
        public Task CreateUserImageAsync(UserImage userImage);
    }
}
