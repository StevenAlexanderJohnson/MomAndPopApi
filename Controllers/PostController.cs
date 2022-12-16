﻿using Api.DataServices;
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
        private readonly PostDataService _postDataService;
        public PostController(PostDataService postDataService)
        {
            _postDataService = postDataService;
        }

        /// <summary>
        /// Create a Post
        /// </summary>
        /// <param name="newPost">Object containing post information.</param>
        /// <returns>Status Code 201 if success, 400 for database issue, and 500 for server error</returns>
        [HttpPost]
        public async Task<ActionResult> Post(Post newPost)
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

        [HttpGet]
        [Route("{postId}")]
        public async Task<ActionResult> GetPostById(Int64 postId)
        {
            try
            {
                List<Post> output = await _postDataService.GetPostByIdAsync(postId);
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
    }
}