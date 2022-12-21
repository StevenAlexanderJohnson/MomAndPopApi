using Api.Models.Response;

namespace Api.DataServices.Interfaces
{
    public interface IPostDataService
    {
        /// <summary>
        /// Searches for a post in the database with the same post ID.
        /// </summary>
        /// <param name="postId">ID of desired post.</param>
        /// <returns>List of post containing the Post</returns>
        public Task<List<PostResponse>> GetPostByIdAsync(long postId);

        /// <summary>
        /// Get the bytes and image type from from the database.
        /// </summary>
        /// <param name="postId">Id of the post you are getting the image for.</param>
        /// <returns>Tuple containing the bytes for the image and the image type.</returns>
        public Task<Tuple<byte[], string>> GetPostImageByIdAsync(Int64 postId);

        /// <summary>
        /// Get a 'window' of post starting at <paramref name="offset"/>.
        /// </summary>
        /// <param name="offset">Number of post from the first post.</param>
        /// <returns>List of PostResponse starting at the offset of <paramref name="offset"/></returns>
        public Task<List<PostResponse>> GetPostWindowAsync(Int64 offset);

        /// <summary>
        /// Delete a post from the database by post ID
        /// </summary>
        /// <param name="post">Object containing the post information.</param>
        /// <returns>Nothing</returns>
        public Task DeletePostByIdAsync(Post post);

        /// <summary>
        /// Creates a post in the database.
        /// </summary>
        /// <param name="newPost">Object containing the post information.</param>
        /// <returns>Nothing</returns>
        public Task CreatePostAsync(Post newPost);
    }
}
