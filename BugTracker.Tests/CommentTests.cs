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

public class CommentControllerTests
{
    private readonly Mock<ICommentService> _mockCommentService;
    private readonly Mock<IProjectService> _mockProjectService;
    private readonly CommentController _controller;
    private readonly ClaimsPrincipal _user;

    public CommentControllerTests()
    {
        _mockCommentService = new Mock<ICommentService>();
        _mockProjectService = new Mock<IProjectService>();
        _controller = new CommentController(_mockCommentService.Object, _mockProjectService.Object);

        _user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.Name, "testuser")
        }));

        _controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = _user }
        };
    }

    [Fact]
    public async Task GetAllComments_ReturnsOkObjectResult_WithAListOfComments()
    {
        var ticketId = "1";
        var mockComments = new List<Comment> { new Comment(
            "1",
            "U1",
            "C1",
            DateTime.Now.ToUniversalTime(),
            DateTime.Now.ToUniversalTime()
        ), new Comment(
            "2",
            "U2",
            "C2",
            DateTime.Now.ToUniversalTime(),
            DateTime.Now.ToUniversalTime()
        ) };
        _mockCommentService.Setup(s => s.GetAllCommentsAsync(ticketId)).ReturnsAsync(mockComments);

        var result = await _controller.GetAllComments(ticketId);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<Comment>>(okResult.Value);
        Assert.Equal(2, returnValue.Count);
    }

    [Fact]
    public async Task CreateComment_ReturnsOkObjectResult()
    {
        var projectId = "projectId";
        var ticketId = "ticketId";
        var createCommentDto = new CreateCommentDto(
            "content",
            "ticketId"
        );

        var loggedInUsername = _user.Identity.Name;
        _mockProjectService.Setup(s => s.CheckIfUserIsPartOfProjectAsync(projectId, loggedInUsername)).ReturnsAsync(true);
        var result = await _controller.AddComment(projectId, ticketId, createCommentDto);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal("Comment Created", okResult.Value);
    }

    [Fact]
    public async Task GetComment_ReturnsNotFoundResult_WhenCommentDoesNotExist()
    {
        _mockCommentService.Setup(s => s.GetCommentByIdAsync("invalidId")).ReturnsAsync((Comment)null);

        var result = await _controller.GetCommentById("invalidId");

        Assert.IsType<NotFoundResult>(result);
    }
}