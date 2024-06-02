using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.BugTracker.Dtos;
using Microsoft.BugTracker.Entities;
using Microsoft.BugTracker.Services;
using Microsoft.BugTracker.Controllers;
using Microsoft.BugTracker.Interfaces;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

public class UserControllerTests
{
    private readonly Mock<IUserService> _mockService;
    private readonly UserController _controller;
    private readonly Mock<HttpContext> _mockHttpContext;
    private readonly Mock<IAuthenticationService> _mockAuthService;

    public UserControllerTests()
    {
        _mockService = new Mock<IUserService>();
        _controller = new UserController(_mockService.Object);

        _mockHttpContext = new Mock<HttpContext>();
        _mockAuthService = new Mock<IAuthenticationService>();


        _mockHttpContext.Setup(_ => _.RequestServices.GetService(typeof(IAuthenticationService)))
                        .Returns(_mockAuthService.Object);
        _controller.ControllerContext = new ControllerContext()
        {
            HttpContext = _mockHttpContext.Object
        };
    }

    [Fact]
    public async Task GetAllUsers_ReturnsOkResult_WithListOfUsers()
    {
        var userDtos = new List<UserDto>() { new UserDto("user1", "email1"), new UserDto("user2", "email2") };
        _mockService.Setup(s => s.GetAllUsersAsync()).ReturnsAsync(userDtos);

        var result = await _controller.GetAllUsers();

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedUsers = Assert.IsType<List<UserDto>>(okResult.Value);
        Assert.Equal(2, returnedUsers.Count);
    }

    [Fact]
    public async Task RegisterUser_ReturnsOk_WhenRegistrationIsSuccessful()
    {
        var regDto = new RegisterDto("newuser", "password123", "email@example.com");
        _mockService.Setup(s => s.RegisterUserAsync(regDto)).Returns(Task.CompletedTask);

        var result = await _controller.RegisterUser(regDto);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task AuthenticateUser_ReturnsOk_WhenCredentialsAreValid()
    {
        var loginDto = new LoginDto("test", "test");
        _mockService.Setup(s => s.AuthenticateUserAsync(loginDto)).ReturnsAsync(true);
        _mockService.Setup(s => s.AuthenticateUserAsync(loginDto)).ReturnsAsync(true);

        _mockAuthService.Setup(x => 
            x.SignInAsync(It.IsAny<HttpContext>(), It.IsAny<string>(), It.IsAny<ClaimsPrincipal>(), It.IsAny<AuthenticationProperties>()))
            .Returns(Task.CompletedTask);

        var result = await _controller.AuthenticateUser(loginDto);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal("User Authenticated", okResult.Value);
    }

    [Fact]
    public async Task AuthenticateUser_ReturnsUnauthorized_WhenCredentialsAreInvalid()
    {
        var loginDto = new LoginDto("user", "badpassword");
        _mockService.Setup(s => s.AuthenticateUserAsync(loginDto)).ReturnsAsync(false);

        var result = await _controller.AuthenticateUser(loginDto);

        Assert.IsType<UnauthorizedObjectResult>(result);
    }
}
