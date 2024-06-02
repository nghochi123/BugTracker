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

public class TicketControllerTests
{
    private readonly Mock<ITicketService> _mockTicketService;
    private readonly Mock<IProjectService> _mockProjectService;
    private readonly TicketController _controller;
    private readonly ClaimsPrincipal _user;

    public TicketControllerTests()
    {
        _mockTicketService = new Mock<ITicketService>();
        _mockProjectService = new Mock<IProjectService>();
        _controller = new TicketController(_mockTicketService.Object, _mockProjectService.Object);

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
    public async Task GetAllTickets_ReturnsOkObjectResult_WithAListOfTickets()
    {
        var projectId = "1";
        var mockTickets = new List<TicketDto> { new TicketDto(
            "1",
            "T1",
            "D1",
            1,
            Progress.InProgress,
            ["testuser"],
            DateTime.Now.ToUniversalTime(),
            DateTime.Now.ToUniversalTime(),
            ["Bug", "UI"]
        ), new TicketDto(
            "2",
            "T2",
            "D2",
            2,
            Progress.InProgress,
            ["testuser"],
            DateTime.Now.ToUniversalTime(),
            DateTime.Now.ToUniversalTime(),
            ["Bug", "UI"]
        ) };
        _mockTicketService.Setup(s => s.GetAllTicketsAsync(projectId)).ReturnsAsync(mockTickets);

        var result = await _controller.GetAllProjectTickets(projectId);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<TicketDto>>(okResult.Value);
        Assert.Equal(2, returnValue.Count);
    }

    [Fact]
    public async Task CreateTicket_ReturnsOkObjectResult()
    {
        var projectId = "1";
        var ticketDto = new CreateTicketDto(
            "title",
            "description",
            1,
            Progress.InProgress,
            ["Bug", "UI"],
            ["testuser"]
        );

        var loggedInUsername = _user.Identity.Name;
        _mockProjectService.Setup(s => s.CheckIfUserIsPartOfProjectAsync(projectId, loggedInUsername)).ReturnsAsync(true);
        var result = await _controller.AddTicket(projectId, ticketDto);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal("Ticket Created", okResult.Value);
    }

    [Fact]
    public async Task GetTicket_ReturnsNotFoundResult_WhenTicketDoesNotExist()
    {
        _mockTicketService.Setup(s => s.GetProjectTicketById("invalidId")).ReturnsAsync((TicketDto)null);

        var result = await _controller.GetTicketById("invalidId");

        Assert.IsType<NotFoundResult>(result);
    }
}