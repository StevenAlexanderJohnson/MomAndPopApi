using Api.Models;
using Api.Utility;
using MySqlConnector;
using System.Data;

namespace Api.DataServices
{
    public class AuthDataService
    {
        private readonly MySqlConnection _sqlConnection;
        public AuthDataService(MySqlConnection sqlConection)
        {
            _sqlConnection = sqlConection;
        }

        /// <summary>
        /// Creates a record in the Auth database using the username and hashed password.
        /// </summary>
        /// <param name="newUser">Object containing the username and password for the user.</param>
        /// <returns>Nothing</returns>
        public async Task CreateUserCredentials(User newUser)
        {
            await _sqlConnection.OpenAsync();
            try
            {
                // Add user credentials to the database
                await using MySqlCommand credentials_command = new MySqlCommand("sp_Add_User_Credentials", _sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                credentials_command.Parameters.AddWithValue("usernameInput", newUser.Username);
                credentials_command.Parameters.AddWithValue("passwordInput", PasswordHasher.HashPassword(newUser.Password));

                await credentials_command.ExecuteNonQueryAsync();
            }
            catch(Exception)
            {
                throw;
            }
            finally
            {
                await _sqlConnection.CloseAsync();
            }
        }

        /// <summary>
        /// Delete the user credentials from the Auth database
        /// </summary>
        /// <param name="user">Object containing the username and password of the user.</param>
        /// <returns>Nothing</returns>
        public async Task DeleteUserCredentials(User user)
        {
            await _sqlConnection.OpenAsync();
            try
            {
                await using var command = new MySqlCommand("sp_Delete_User_Credentials", _sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("usernameInput", user.Username);
                command.Parameters.AddWithValue("passwordInput", PasswordHasher.HashPassword(user.Password));
                await command.ExecuteNonQueryAsync();
            }
            catch(Exception)
            {
                throw;
            }
            finally
            {
                await _sqlConnection.CloseAsync();
            }
        }

        /// <summary>
        /// Validate user credentials.
        /// </summary>
        /// <param name="user">Object containing the username and password of the user.</param>
        /// <returns>True if the credentials were correct and false if not.</returns>
        public async Task<bool> ValidateUserCredentialsAsync(UserLogin user)
        {
            await _sqlConnection.OpenAsync();
            try
            {
                await using MySqlCommand command = new MySqlCommand("sp_Authorize_User", _sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("usernameInput", user.UserName);
                command.Parameters.AddWithValue("passwordHashInput", PasswordHasher.HashPassword(user.Password));

                await using MySqlDataReader reader = await command.ExecuteReaderAsync();
                reader.Read();
                bool output = Convert.ToBoolean(reader["result"]);
                return output;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                await _sqlConnection.CloseAsync();
            }
        }
    }
}
