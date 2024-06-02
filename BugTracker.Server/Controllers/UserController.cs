using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.BugTracker.Dtos;
using Microsoft.BugTracker.Entities;
using Microsoft.BugTracker.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;


namespace Microsoft.BugTracker.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var userDtos = await _userService.GetAllUsersAsync();
            return Ok(userDtos);
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(
            [FromBody] RegisterDto reg
        )
        {
            await _userService.RegisterUserAsync(reg);
            return Ok("User Registered");
        }

        [HttpPost("login")]
        public async Task<IActionResult> AuthenticateUser(
            [FromBody] LoginDto login
        )
        {
            var username = login.UserName;
            var result = await _userService.AuthenticateUserAsync(login);
            if (result){
                var claims = new List<Claim>
                {
                    new(ClaimTypes.Name, username)
                };

                var claimsIdentity = new ClaimsIdentity(claims, "CookieAuth");
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync("CookieAuth", claimsPrincipal);
                return Ok("User Authenticated");
            }
            return Unauthorized("Invalid username or password");
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("CookieAuth");

            return Ok("User logged out");
        }

        [HttpGet("{userName}")]
        public async Task<IActionResult> GetUser(string userName)
        {
            var userDto = await _userService.GetUserByIdAsync(userName);
            if (userDto == null)
            {
                return NotFound(); // Returns a 404 Not Found response if the user is not found
            }

            return Ok(userDto); // Returns a 200 OK response with the user data
        }

        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser(
            [FromBody] RegisterDto userDetails
        )
        {
            var loggedInUsername = User.Identity.Name;
            var usernameToEdit = userDetails.UserName;
            if (!string.Equals(loggedInUsername, usernameToEdit))
            {
                return Unauthorized("You are not allowed to access this resource.");
            }
            await _userService.UpdateUserAsync(userDetails);
            return Ok("User Updated.");
        }
        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var loggedInUsername = User.Identity.Name;
            var userDto = await _userService.GetUserByIdAsync(loggedInUsername);
            return Ok(userDto);
        }

        

    }
}
