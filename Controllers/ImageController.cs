using Api.DataServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;

namespace Api.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("/api/[controller]")]
    public class ImageController : Controller
    {
        private readonly IPostDataService _postDataService;
        private readonly IUserDataService _userDataService;
        public ImageController(IPostDataService postDataService, IUserDataService userDataSeries)
        {
            _postDataService = postDataService;
            _userDataService = userDataSeries;
        }

        /// <summary>
        /// Get the image for the post with passed ID
        /// </summary>
        /// <param name="postId">Id of the post you are getting the image for.</param>
        /// <returns>Image file for the post.</returns>
        [HttpGet]
        [Route("{postId}")]
        public async Task<ActionResult> GetPostImageById(Int64 postId)
        {
            try
            {
                Tuple<byte[], string> fileData = await _postDataService.GetPostImageByIdAsync(postId);
                return File(fileData.Item1, fileData.Item2);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get the image for a user's profile from user's Id.
        /// </summary>
        /// <param name="userId">User's Id</param>
        /// <returns>Image file for the user's profile.</returns>
        [HttpGet]
        [Route("user/{userId}")]
        public async Task<ActionResult> GetUserImageById(Int64 userId)
        {
            try
            {
                Tuple<byte[], string> fileData = await _userDataService.GetUserImageByIdAsync(userId);
                return File(fileData.Item1, fileData.Item2);
            }
            catch(Exception ex)
            {
                if (ex.Message == "No Data")
                {
                    return BadRequest();
                }
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Route("user")]
        public async Task<ActionResult> UploadUserImage([FromForm] UserImage userImage)
        {
            try
            {
                userImage.UserId = Convert.ToInt64(User.Claims
                    .Where(x => x.Type == "userID")
                    .Select(x => x.Value).First());
                await _userDataService.CreateUserImageAsync(userImage);
                return Ok();
            }
            catch(FormatException)
            {
                return BadRequest("Invalid user");
            }
            catch(Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
