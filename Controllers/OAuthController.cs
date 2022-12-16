using Api.DataServices;
using Api.Models;
using Api.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OAuth : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly AuthDataService _authDataService;
        private readonly UserDataService _userDataService;
        public OAuth(IConfiguration config, AuthDataService authDataService, UserDataService userDataService)
        {
            _config = config;
            _authDataService = authDataService;
            _userDataService = userDataService;
        }

        /// <summary>
        /// Checks the user's credentials and returns their auth token.
        /// </summary>
        /// <param name="userLogin">Username, Password, ReturnUrl</param>
        /// <returns>JWT Auth token that should be passed back to the server as a Bearer token.</returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public ActionResult Login([FromBody] UserLogin userLogin)
        {
            UserModel user = Authentication.Authenticate(userLogin, _userDataService, _authDataService).Result;
            if (user != null)
            {
                var token = Jwt.GenerateJwt(_config, user);
                return Ok(new { token = token});
            }

            return BadRequest("User not found.");
        }

        /// <summary>
        /// Register a user with the Auth database and the Social database.
        /// </summary>
        /// <param name="FirstName">First name of the new user</param>
        /// <param name="LastName">Last name of the new user</param>
        /// <param name="Address1">Addres line 1</param>
        /// <param name="Address2">Addres line 2</param>
        /// <param name="City">City for address</param>
        /// <param name="State">State for address</param>
        /// <param name="Zip">Zip for address</param>
        /// <param name="Password">Password used to register user</param>
        /// <returns>
        /// Status Code 201 if the creation was a success.
        /// Bad Request if there was an issue in inserting.
        /// Status Code 500 if there was an issue on the server.
        /// </returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> Register([FromBody] User newUser)
        {
            try
            {
                await _authDataService.CreateUserCredentials(newUser);
                await _userDataService.CreateUserAsync(newUser);
                return StatusCode(201);
            }
            catch (DbException ex)
            {
                if (ex.SqlState == "45000")
                {
                    return BadRequest(ex.Message);
                }
                else if (ex.SqlState == "23000")
                {
                    await _authDataService.DeleteUserCredentials(newUser);
                    return BadRequest("User credentials already exists.");
                }
                return StatusCode(500);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
