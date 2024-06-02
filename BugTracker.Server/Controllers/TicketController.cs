using Microsoft.AspNetCore.Mvc;
using Microsoft.BugTracker.Dtos;
using Microsoft.BugTracker.Entities;
using Microsoft.BugTracker.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Microsoft.BugTracker.Controllers
{
    [ApiController]
    [Route("api/projects/{projectId}/tickets")]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        private readonly IProjectService _projectService;

        public TicketController(ITicketService ticketService, IProjectService projectService)
        {
            _ticketService = ticketService;
            _projectService = projectService;

        }

        [HttpGet]
        public async Task<IActionResult> GetAllProjectTickets(string projectId){
            var projectTickets = await _ticketService.GetAllTicketsAsync(projectId);
            return Ok(projectTickets);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddTicket(
            string projectId,
            [FromBody] CreateTicketDto ticketInformation
        )
        {
            var loggedInUsername = User.Identity.Name;
            var userIsPartOfProject = await _projectService.CheckIfUserIsPartOfProjectAsync(projectId, loggedInUsername);
            if (userIsPartOfProject)
            {
                Ticket ticket = new Ticket(
                    ticketInformation.Title,
                    ticketInformation.Description,
                    ticketInformation.Priority,
                    ticketInformation.Status,
                    ticketInformation.AssignedUserNames,
                    DateTime.Now.ToUniversalTime(),
                    DateTime.Now.ToUniversalTime(),
                    ticketInformation.Tags,
                    projectId
                );

                await _ticketService.AddTicketAsync(ticket);
                return Ok("Ticket Created");
            }
            return Unauthorized("You are not allowed to access this resource.");

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTicketById(
            string id
        )
        {
            var ticketDto = await _ticketService.GetProjectTicketById(id);
            if (ticketDto == null)
            {
                return NotFound();
            }
            return Ok(ticketDto);

        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateProjectTicket(
            [FromBody] UpdateTicketDto updateTicketInformation,
            string projectId
        )
        {
            var loggedInUsername = User.Identity.Name;
            Console.WriteLine($"Username {loggedInUsername}");
            var userIsPartOfProject = await _projectService.CheckIfUserIsPartOfProjectAsync(projectId, loggedInUsername);
            if (userIsPartOfProject)
            {
                var ticketDto = await _ticketService.GetProjectTicketById(updateTicketInformation.Id);
                Ticket ticket = new(
                    updateTicketInformation.Title,
                    updateTicketInformation.Description,
                    updateTicketInformation.Priority,
                    updateTicketInformation.Status,
                    updateTicketInformation.AssignedUserNames,
                    ticketDto.CreatedAt,
                    DateTime.Now.ToUniversalTime(),
                    updateTicketInformation.Tags,
                    projectId
                ){
                    Id=ticketDto.Id
                };
                await _ticketService.UpdateProjectTicketAsync(ticket);
                return Ok("Ticket Updated.");
            }
            return Unauthorized("You are not allowed to access this resource.");

        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjectTicket(
            string id,
            string projectId
        )
        {
            var loggedInUsername = User.Identity.Name;
            var userIsPartOfProject = await _projectService.CheckIfUserIsPartOfProjectAsync(projectId, loggedInUsername);
            if (userIsPartOfProject)
            {
                await _ticketService.DeleteProjectTicketById(id);
                return Ok("Ticket deleted.");
            }
            return Unauthorized("You are not allowed to access this resource.");
        }
    }
}
