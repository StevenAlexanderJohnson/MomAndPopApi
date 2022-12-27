using Api.DataServices.Interfaces;
using Api.Models;
using Api.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data.Common;
using System.Net;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OAuth : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IAuthDataService _authDataService;
        private readonly IUserDataService _userDataService;
        public OAuth(IConfiguration config, IAuthDataService authDataService, IUserDataService userDataService)
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
            try
            {
                UserModel user = Authentication.Authenticate(userLogin, _userDataService, _authDataService).Result;
                if (user != null)
                {
                    Response.Cookies.Append("refreshToken", user.RefreshToken!, new CookieOptions
                    {
                        HttpOnly = true,
                        IsEssential = true
                    });

                    var token = Jwt.GenerateJwt(_config, user);
                    return Ok(new { token = token });
                }

                return BadRequest("User not found.");
            }
            catch (DbException)
            {
                return BadRequest();
            }
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
                return Login(new UserLogin() { UserName = newUser.Username, Password = newUser.Password });
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
                    return BadRequest("Username is already in use.");
                }
                return StatusCode(500);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Sets the refresh token to expire now.
        /// </summary>
        /// <returns>StatusCode 200 on success</returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("logout")]
        public async Task<ActionResult> Logout()
        {
            try
            {
                string refreshToken = Request.Cookies["refreshToken"]!;
                if(string.IsNullOrEmpty(refreshToken))
                {
                    return BadRequest();
                }
                await _authDataService.ExpireRefreshTokenAsync(refreshToken);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Refreshes the JWT Auth token using the refresh token.
        /// </summary>
        /// <returns>Returns an object containing a new auth token, and a cookie containing the new refresh token.</returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("refresh")]
        public async Task<ActionResult> RefreshAuthToken()
        {
            try
            {
                string refreshToken = Request.Cookies["refreshToken"]!;
                if (string.IsNullOrEmpty(refreshToken))
                {
                    return StatusCode(403);
                }
                UserModel user = await Authentication.RefreshAuthToken(refreshToken, _userDataService, _authDataService);

                if (user == null)
                {
                    return StatusCode(401);
                }

                Response.Cookies.Append("refreshToken", user.RefreshToken!, new CookieOptions
                {
                    HttpOnly = true,
                    IsEssential = true
                });

                var authToken = Jwt.GenerateJwt(_config, user);
                Console.WriteLine("Token has been refreshed.");
                return Ok(new { token = authToken });
            }
            catch (DbException)
            {
                return StatusCode(401);
            }
            catch (SecurityTokenException)
            {
                return StatusCode(401);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
