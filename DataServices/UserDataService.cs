using Api.Models;
using MySqlConnector;
using System.Data;

namespace Api.DataServices
{
    public class UserDataService
    {
        private readonly MySqlConnection _sqlConnection;
        public UserDataService(MySqlConnection sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }

        /// <summary>
        /// Gets a list of users who's username are equal to parameter
        /// </summary>
        /// <param name="userId">The Id of the user you want to find</param>
        /// <returns>List containing one user who's id matches the parameter.</returns>
        public async Task<List<User>> GetUserByIdAsync(Int64 userId)
        {
            await _sqlConnection.OpenAsync();

            try
            {
                await using MySqlCommand sqlCommand = new MySqlCommand("sp_Select_User_By_Id", _sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("userId", userId);

                await using MySqlDataReader sqlReader = await sqlCommand.ExecuteReaderAsync();

                List<User> output = new List<User>();
                while (sqlReader.Read())
                {
                    User add_value = new User();
                    add_value.Id = Convert.ToInt64(sqlReader["id"]);
                    add_value.FirstName = Convert.ToString(sqlReader["first_name"])!;
                    add_value.Username = Convert.ToString(sqlReader["username"])!;
                    add_value.LastName = Convert.ToString(sqlReader["last_name"])!;
                    add_value.Address1 = Convert.ToString(sqlReader["address1"])!;
                    add_value.Address2 = Convert.ToString(sqlReader["address2"]) ?? "";
                    add_value.City = Convert.ToString(sqlReader["city"])!;
                    add_value.State = Convert.ToString(sqlReader["state"])!;
                    add_value.Zip = Convert.ToString(sqlReader["zip"])!;
                    add_value.Verified = Convert.ToBoolean(sqlReader["verified"]);
                    output.Add(add_value);
                }
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

        /// <summary>
        /// Get a list of users who's username match parameter.
        /// </summary>
        /// <param name="username">Username to search for</param>
        /// <returns>List of Users who's username match search parameter.</returns>
        public async Task<List<User>> GetUsersByUsernameAsync(string username)
        {
            await _sqlConnection.OpenAsync();
            try
            {
                await using MySqlCommand sqlCommand = new MySqlCommand("sp_Select_User_By_Username", _sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("username", username);

                await using MySqlDataReader sqlReader = await sqlCommand.ExecuteReaderAsync();

                List<User> output = new List<User>();
                while (sqlReader.Read())
                {
                    User add_value = new User();
                    add_value.Id = Convert.ToInt64(sqlReader["id"]);
                    add_value.FirstName = Convert.ToString(sqlReader["first_name"])!;
                    add_value.Username = Convert.ToString(sqlReader["username"])!;
                    add_value.LastName = Convert.ToString(sqlReader["last_name"])!;
                    add_value.Address1 = Convert.ToString(sqlReader["address1"])!;
                    add_value.Address2 = Convert.ToString(sqlReader["address2"]) ?? "";
                    add_value.City = Convert.ToString(sqlReader["city"])!;
                    add_value.State = Convert.ToString(sqlReader["state"])!;
                    add_value.Zip = Convert.ToString(sqlReader["zip"])!;
                    add_value.Verified = Convert.ToBoolean(sqlReader["verified"]);
                    output.Add(add_value);
                }
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

        /// <summary>
        /// Creates a user in the database.
        /// </summary>
        /// <param name="newUser">User object to be inserted into the database.</param>
        /// <returns>Nothing</returns>
        public async Task CreateUserAsync(User newUser)
        {
            if (newUser.Password == "")
            {
                throw new NullReferenceException("Password is null");
            }

            await _sqlConnection.OpenAsync();
            try
            {
                // Add the user information to the database
                await using MySqlCommand command = new MySqlCommand("sp_Create_User", _sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@ID", newUser.Id);
                command.Parameters.AddWithValue("@Username", newUser.Username);
                command.Parameters.AddWithValue("@FirstName", newUser.FirstName);
                command.Parameters.AddWithValue("@LastName", newUser.LastName);
                command.Parameters.AddWithValue("@Address1", newUser.Address1);
                command.Parameters.AddWithValue("@Address2", newUser.Address2);
                command.Parameters.AddWithValue("@City", newUser.City);
                command.Parameters.AddWithValue("@State", newUser.State);
                command.Parameters.AddWithValue("@Zip", newUser.Zip);
                command.Parameters.AddWithValue("@Verified", newUser.Verified);

                await command.ExecuteNonQueryAsync();
            }
            catch (Exception e)
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
