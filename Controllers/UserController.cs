using Api.DataServices;
using Api.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly UserDataService _userDataService;
        private readonly AuthDataService _authDataService;
        public UserController(UserDataService userDataService, AuthDataService authDataService)
        {
            _userDataService = userDataService;
            _authDataService = authDataService;
        }

        /// <summary>
        /// Search for users with username is equal to <paramref name="username"/>.
        /// </summary>
        /// <param name="username">Username to be looked up.</param>
        /// <returns>List of Users</returns>
        [HttpGet]
        [Route("username/{username}")]
        public async Task<IActionResult> Get(string username)
        {
            try
            {
                List<User> output = await _userDataService.GetUsersByUsernameAsync(username);
                return Ok(output);
            }
            catch(DbException)
            {
                return BadRequest("User does not exists");
            }
            catch(Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Search of user who's ID is equal to <paramref name="userId"/>.
        /// </summary>
        /// <param name="userId">ID of the user to look up</param>
        /// <returns>List of users containing the User</returns>
        [HttpGet]
        [Route("{userId}")]
        public async Task<IActionResult> Get(Int64 userId)
        {
            try
            {
                var output = await _userDataService.GetUserByIdAsync(userId);
                return Ok(output);
            }
            catch(DbException)
            {
                return StatusCode(400);
            }
            catch(Exception)
            {
                return StatusCode(500);
            }
        }
    }
}