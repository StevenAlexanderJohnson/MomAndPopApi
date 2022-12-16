using MySqlConnector;
using System.Data;
using System.Text;

namespace Api.DataServices
{
    public class PostDataService
    {
        private readonly MySqlConnection _sqlConnection;
        public PostDataService(MySqlConnection sqlConnection) 
        {
            _sqlConnection = sqlConnection;
        }

        /// <summary>
        /// Searches for a post in the database with the same post ID.
        /// </summary>
        /// <param name="postId">ID of desired post.</param>
        /// <returns>List of post containing the Post</returns>
        internal async Task<List<Post>> GetPostByIdAsync(long postId)
        {
            await _sqlConnection.OpenAsync();
            try
            {
                await using MySqlCommand command = new MySqlCommand("sp_Get_Post_By_Id", _sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("postId", postId);

                await using MySqlDataReader sqlReader = await command.ExecuteReaderAsync();
                List<Post> output = new List<Post>();
                while (sqlReader.Read())
                {
                    Post addValue = new Post();
                    addValue.Id = postId;
                    addValue.Image = Encoding.Default.GetString((byte[])sqlReader["image"]);
                    addValue.Title = sqlReader.GetString("title");
                    addValue.Description = sqlReader.GetString("description");
                    addValue.UserId = sqlReader.GetInt64("user_id");
                    output.Add(addValue);
                }

                return output;
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
        /// Delete a post from the database by post ID
        /// </summary>
        /// <param name="post">Object containing the post information.</param>
        /// <returns>Nothing</returns>
        public async Task DeletePostByIdAsync(Post post)
        {
            await _sqlConnection.OpenAsync();
            try
            {
                await using MySqlCommand command = new MySqlCommand("sp_Delete_Post_By_Id", _sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

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
        /// Creates a post in the database.
        /// </summary>
        /// <param name="newPost">Object containing the post information.</param>
        /// <returns>Nothing</returns>
        public async Task CreatePostAsync(Post newPost)
        {
            await _sqlConnection.OpenAsync();
            try
            {
                await using MySqlCommand command = new MySqlCommand("sp_Create_Post", _sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("Image", newPost.Image);
                command.Parameters.AddWithValue("Title", newPost.Title);
                command.Parameters.AddWithValue("Description", newPost.Description);
                command.Parameters.AddWithValue("UserID", newPost.UserId);

                await command.ExecuteNonQueryAsync();
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
