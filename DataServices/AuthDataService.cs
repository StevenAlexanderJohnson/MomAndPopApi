using Api.Dependencies;
using Api.Models;
using Api.Utility;
using MySqlConnector;
using System.Data;

namespace Api.DataServices
{
    public class AuthDataService
    {
        private readonly MySqlConnectionFactory _connectionFactory;
        public AuthDataService(MySqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        /// <summary>
        /// Creates a record in the Auth database using the username and hashed password.
        /// </summary>
        /// <param name="newUser">Object containing the username and password for the user.</param>
        /// <returns>Nothing</returns>
        public async Task<string> CreateUserCredentials(User newUser)
        {
            using (var connection = await _connectionFactory.CreateConnectionAsync())
            {
                // Add user credentials to the database
                await using MySqlCommand command = new MySqlCommand("sp_Add_User_Credentials", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("usernameInput", newUser.Username);
                command.Parameters.AddWithValue("passwordInput", PasswordHasher.HashPassword(newUser.Password));
                string refresh_token = Authentication.GenerateRefreshToken();
                command.Parameters.AddWithValue("refreshToken", refresh_token);
                command.Parameters.AddWithValue("expiresIn", DateTime.Now.AddMinutes(7));

                await command.ExecuteNonQueryAsync();
                return refresh_token;
            }
        }

        /// <summary>
        /// Delete the user credentials from the Auth database
        /// </summary>
        /// <param name="user">Object containing the username and password of the user.</param>
        /// <returns>Nothing</returns>
        public async Task DeleteUserCredentials(User user)
        {
            using (var connection = await _connectionFactory.CreateConnectionAsync())
            {
                await using var command = new MySqlCommand("sp_Delete_User_Credentials", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("usernameInput", user.Username);
                command.Parameters.AddWithValue("passwordInput", PasswordHasher.HashPassword(user.Password));
                await command.ExecuteNonQueryAsync();
            }
        }

        /// <summary>
        /// Validate user credentials.
        /// </summary>
        /// <param name="user">Object containing the username and password of the user.</param>
        /// <returns>True if the credentials were correct and false if not.</returns>
        public async Task<bool> ValidateUserCredentialsAsync(UserLogin user)
        {
            using (var connection = await _connectionFactory.CreateConnectionAsync())
            {
                await using MySqlCommand command = new MySqlCommand("sp_Authorize_User", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("usernameInput", user.UserName);
                command.Parameters.AddWithValue("passwordHashInput", PasswordHasher.HashPassword(user.Password));
                command.Parameters.Add("result", DbType.Boolean);
                command.Parameters["result"].Direction = ParameterDirection.Output;

                await command.ExecuteNonQueryAsync();
                return Convert.ToBoolean(command.Parameters["result"].Value);
            }
        }

        public async Task<string> ValidateRefreshTokenAsync(string refreshToken)
        {
            using (var connection = await _connectionFactory.CreateConnectionAsync())
            {
                await using MySqlCommand command = new MySqlCommand("sp_Check_Refresh_Token", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("refreshToken", refreshToken);
                command.Parameters.Add(new MySqlParameter("foundUser", MySqlDbType.VarChar));
                command.Parameters["foundUser"].Direction = ParameterDirection.Output;

                await command.ExecuteNonQueryAsync();
                string output = command.Parameters["foundUser"].Value?.ToString()!;
                return output;
            }
        }

        public async Task UpdateRefreshToken(string username, string newRefreshToken)
        {
            using (var connection = await _connectionFactory.CreateConnectionAsync())
            {
                await using MySqlCommand command = new MySqlCommand("sp_Update_Refresh_Token", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("usernameInput", username);
                command.Parameters.AddWithValue("newRefreshToken", newRefreshToken);

                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
