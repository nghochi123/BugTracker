using Microsoft.AspNetCore.Mvc;
using Microsoft.BugTracker.Entities;

namespace BugTracker.Server.Controllers
{
    [ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly UserService _userService;

    public UsersController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(int id)
    {
        var userDto = await _userService.GetUserByIdAsync(id);
        if (userDto == null)
        {
            return NotFound(); // Returns a 404 Not Found response if the user is not found
        }

        return Ok(userDto); // Returns a 200 OK response with the user data
    }
}
}
