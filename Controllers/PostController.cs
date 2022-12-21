using Api.DataServices.Interfaces;
using Api.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;

namespace Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/Post")]
    public class PostController : ControllerBase
    {
        private readonly IPostDataService _postDataService;
        public PostController(IPostDataService postDataService)
        {
            _postDataService = postDataService;
        }

        /// <summary>
        /// Create a Post
        /// </summary>
        /// <param name="newPost">Object containing post information.</param>
        /// <returns>Status Code 201 if success, 400 for database issue, and 500 for server error</returns>
        [HttpPost]
        public async Task<ActionResult> Post([FromForm] Post newPost)
        {
            try
            {
                newPost.UserId = Convert.ToInt64(User.Claims
                                    .Where(x => x.Type == "userID")
                                    .Select(x => x.Value).SingleOrDefault());

                await _postDataService.CreatePostAsync(newPost);
                return StatusCode(201);
            }
            catch (FormatException)
            {
                return BadRequest("Invalid user");
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Get a post from the database by it's ID.
        /// </summary>
        /// <param name="postId">Id of the post to be retrieved</param>
        /// <returns>A list of Post objects that contains the Post information.</returns>
        [HttpGet]
        [Route("{postId}")]
        public async Task<ActionResult> GetPostById(Int64 postId)
        {
            try
            {
                List<PostResponse> output = await _postDataService.GetPostByIdAsync(postId);
                return Ok(output);
            }
            catch (DbException)
            {
                return BadRequest("Post could not be found.");
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Get a list of posts with an offset from the first post.
        /// </summary>
        /// <param name="offset">Number of post from the first post</param>
        /// <returns>List of PostResonse objects</returns>
        [HttpGet]
        [Route("offset/{offset}")]
        public async Task<ActionResult> GetPostWindow(Int64 offset)
        {
            try
            {
                List<PostResponse> output = await _postDataService.GetPostWindowAsync(offset);
                return Ok(output);
            }
            catch(DbException)
            {
                return BadRequest();
            }
            catch(Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
