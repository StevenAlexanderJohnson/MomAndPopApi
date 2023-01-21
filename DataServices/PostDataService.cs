using Api.DataServices.Interfaces;
using Api.Dependencies;
using Api.Models.Response;
using MySqlConnector;
using System.Data;

namespace Api.DataServices
{
    public class PostDataService : IPostDataService
    {
        private readonly MySqlConnectionFactory _connectionFactory;
        public PostDataService(MySqlConnectionFactory connectionFactory) 
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<List<PostResponse>> GetPostByIdAsync(long postId)
        {
            using (var connection = await _connectionFactory.CreateConnectionAsync())
            {
                await using MySqlCommand command = new MySqlCommand("sp_Get_Post_By_Id", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("postId", postId);

                await using MySqlDataReader sqlReader = await command.ExecuteReaderAsync();
                List<PostResponse> output = new List<PostResponse>();
                while (sqlReader.Read())
                {
                    PostResponse addValue = new PostResponse();
                    addValue.Id = postId;
                    addValue.Title = sqlReader.GetString("title");
                    addValue.Description = sqlReader.GetString("description");
                    addValue.Attachment = sqlReader.GetBoolean("attachment");
                    if(addValue.Attachment)
                    {
                        addValue.ImageUrl = $"https://localhost:7282/api/image/{addValue.Id}";
                    }
                    output.Add(addValue);
                }
                return output;
            }
        }

        public async Task<Tuple<byte[], string>> GetPostImageByIdAsync(Int64 postId)
        {
            using (var connection = await _connectionFactory.CreateConnectionAsync())
            {
                await using MySqlCommand command = new MySqlCommand("sp_Get_Post_Image", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("postId", postId);

                await using MySqlDataReader reader = await command.ExecuteReaderAsync();
                await reader.ReadAsync();
                Tuple<byte[], string> output = new Tuple<byte[], string>((byte[])reader["image"], (string)reader["image_type"]);
                return output;
            }
        }

        public async Task<List<PostResponse>> GetPostWindowAsync(Int64 offset)
        {
            using (var connection = await _connectionFactory.CreateConnectionAsync())
            {
                await using MySqlCommand command = new MySqlCommand("sp_Get_Post_Window", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("page_offset", offset * 10);

                await using MySqlDataReader sqlReader = await command.ExecuteReaderAsync();
                List<PostResponse> output = new List<PostResponse>();
                while(sqlReader.Read())
                {
                    PostResponse addValue = new PostResponse();
                    addValue.Id = sqlReader.GetInt64("id");
                    addValue.Title = sqlReader.GetString("title");
                    addValue.Description = sqlReader.GetString("description");
                    addValue.Attachment = sqlReader.GetBoolean("attachment");
                    addValue.CreateDate = sqlReader.GetDateTime("create_date");
                    addValue.UserName = sqlReader.GetString("username");
                    if(addValue.Attachment)
                    {
                        addValue.ImageUrl = $"https://localhost:7282/api/image/{addValue.Id}";
                    }
                    output.Add(addValue);
                }

                return output;
            }
        }

        public async Task DeletePostByIdAsync(Post post)
        {
            using (var connection = await _connectionFactory.CreateConnectionAsync())
            {
                await using MySqlCommand command = new MySqlCommand("sp_Delete_Post_By_Id", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task CreatePostAsync(Post newPost)
        {
            using (var connection = await _connectionFactory.CreateConnectionAsync())
            {
                await using MySqlCommand command = new MySqlCommand("sp_Create_Post", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                if (newPost.Image.Count() > 0)
                {
                    using MemoryStream ms = new MemoryStream();
                    await newPost.Image[0].CopyToAsync(ms);
                    command.Parameters.AddWithValue("Image", ms.ToArray());
                    command.Parameters.AddWithValue("Image_Type", newPost.Image[0].ContentType);
                }
                else
                {
                    command.Parameters.AddWithValue("Image", null);
                    command.Parameters.AddWithValue("Image_Type", null);
                }
                command.Parameters.AddWithValue("Title", newPost.Title);
                command.Parameters.AddWithValue("Description", newPost.Description);
                command.Parameters.AddWithValue("UserID", newPost.UserId);

                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
