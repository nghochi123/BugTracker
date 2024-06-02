using Microsoft.AspNetCore.Mvc;
using Microsoft.BugTracker.Dtos;
using Microsoft.BugTracker.Entities;
using Microsoft.BugTracker.Interfaces;
using Microsoft.AspNetCore.Authorization;


namespace Microsoft.BugTracker.Controllers
{
    [ApiController]
    [Route("api/projects")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProjects()
        {
            var projectDtos = await _projectService.GetAllProjectsAsync();
            return Ok(projectDtos);
        }

        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> CreateProject(
            [FromBody] CreateProjectDto project
        )
        {
            var loggedInUsername = User.Identity.Name;
            var projectId = await _projectService.CreateProjectAsync(project);
            await _projectService.AddProjectUserAsync(new ProjectUser(
                projectId,
                loggedInUsername
            ));
            return Ok(projectId);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProject(string id)
        {
            var projectDto = await _projectService.GetProjectByIdAsync(id);
            if (projectDto == null)
            {
                return NotFound();
            }

            return Ok(projectDto);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateProject(
            [FromBody] UpdateProjectDto updateProjectDto
        )
        {
            var loggedInUsername = User.Identity.Name;
            var userIsPartOfProject = await _projectService.CheckIfUserIsPartOfProjectAsync(updateProjectDto.Id, loggedInUsername);
            if (userIsPartOfProject)
            {
                var projectDto = await _projectService.GetProjectByIdAsync(updateProjectDto.Id);
                Project project = new(
                    updateProjectDto.Name,
                    updateProjectDto.Description,
                    projectDto.CreatedAt,
                    DateTime.Now.ToUniversalTime()
                )
                {
                    Id = projectDto.Id
                };
                await _projectService.UpdateProjectAsync(project);
                return Ok("Project Updated.");
            }
            return Unauthorized("You are not allowed to access this resource.");
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjectById(string id)
        {
            var loggedInUsername = User.Identity.Name;
            var userIsPartOfProject = await _projectService.CheckIfUserIsPartOfProjectAsync(id, loggedInUsername);
            if (userIsPartOfProject)
            {
                await _projectService.DeleteProjectAsync(id);
                return Ok("Project deleted");
            }
            return Unauthorized("You are not allowed to access this resource.");
        }

        // Assigning Users to projects

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetAllUserProjects()
        {
            var loggedInUsername = User.Identity.Name;
            var projectUsers = await _projectService.GetAllUserProjectsAsync(loggedInUsername);
            return Ok(projectUsers);
        }

        [HttpGet("{projectId}/users")]
        public async Task<IActionResult> GetAllProjectUsers(string projectId)
        {
            var projectUsers = await _projectService.GetAllProjectUsersAsync(projectId);
            return Ok(projectUsers);
        }
        
        [Authorize]
        [HttpPost("{projectId}/users")]
        public async Task<IActionResult> AddProjectUser(
            string projectId,
            [FromBody] ProjectUser projectUser
        )
        {
            await _projectService.AddProjectUserAsync(projectUser);
            return Ok("User assigned to project");
        }

        [Authorize]
        [HttpDelete("{projectId}/users/{userId}")]
        public async Task<IActionResult> DeleteProjectUser(string projectId, string userId)
        {

            var loggedInUsername = User.Identity.Name;
            var userIsPartOfProject = await _projectService.CheckIfUserIsPartOfProjectAsync(projectId, loggedInUsername);
            if (userIsPartOfProject)
            {
                await _projectService.RemoveProjectUserAsync(projectId, userId);
                return Ok("User removed from project.");
            }
            return Unauthorized("You are not allowed to access this resource.");
        }
    }
}
