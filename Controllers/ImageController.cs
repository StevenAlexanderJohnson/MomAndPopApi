using Api.DataServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("/api/[controller]")]
    public class ImageController : Controller
    {
        private readonly IPostDataService _postDataService;
        public ImageController(IPostDataService postDataService)
        {
            _postDataService = postDataService;
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

    }
}
