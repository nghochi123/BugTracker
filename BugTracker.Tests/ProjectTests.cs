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

public class ProjectControllerTests
{
    private readonly Mock<IProjectService> _mockProjectService;
    private readonly ProjectController _controller;
    private readonly ClaimsPrincipal _user;

    public ProjectControllerTests()
    {
        _mockProjectService = new Mock<IProjectService>();
        _controller = new ProjectController(_mockProjectService.Object);

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
    public async Task GetAllProjects_ReturnsOkObjectResult_WithAListOfProjects()
    {
        var mockProjects = new List<ProjectDto> { new ProjectDto(
            "1",
            "P1",
            "D1",
            DateTime.Now.ToUniversalTime(),
            DateTime.Now.ToUniversalTime()
        ), new ProjectDto(
            "2",
            "P2",
            "D2",
            DateTime.Now.ToUniversalTime(),
            DateTime.Now.ToUniversalTime()
        ) };
        _mockProjectService.Setup(s => s.GetAllProjectsAsync()).ReturnsAsync(mockProjects);

        var result = await _controller.GetAllProjects();

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<ProjectDto>>(okResult.Value);
        Assert.Equal(2, returnValue.Count);
    }

    [Fact]
    public async Task CreateProject_ReturnsOkObjectResult_WithProjectId()
    {
        var projectDto = new CreateProjectDto("New Project", "Project Description");
        _mockProjectService.Setup(s => s.CreateProjectAsync(It.IsAny<CreateProjectDto>())).ReturnsAsync("projectId");
        _mockProjectService.Setup(s => s.AddProjectUserAsync(It.IsAny<ProjectUser>())).Returns(Task.CompletedTask);

        var result = await _controller.CreateProject(projectDto);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal("projectId", okResult.Value);
    }

    [Fact]
    public async Task GetProject_ReturnsNotFoundResult_WhenProjectDoesNotExist()
    {
        _mockProjectService.Setup(s => s.GetProjectByIdAsync("invalidId")).ReturnsAsync((ProjectDto)null);

        var result = await _controller.GetProject("invalidId");

        Assert.IsType<NotFoundResult>(result);
    }
}