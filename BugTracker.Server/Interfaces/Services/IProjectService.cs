using Microsoft.BugTracker.Entities;
using Microsoft.BugTracker.Dtos;

namespace Microsoft.BugTracker.Interfaces;
public interface IProjectService
{
    Task<List<ProjectDto>> GetAllProjectsAsync();
    Task<ProjectDto> GetProjectByIdAsync(string id);
    Task<string> CreateProjectAsync(CreateProjectDto createProject);
    Task UpdateProjectAsync(Project project);
    Task DeleteProjectAsync(string id);
    Task<List<ProjectUser>> GetAllProjectUsersAsync(string id);
    Task<List<ProjectUser>> GetAllUserProjectsAsync(string id);
    Task AddProjectUserAsync(ProjectUser projectUser);
    Task RemoveProjectUserAsync(string projectId, string userName);
    Task<bool> CheckIfUserIsPartOfProjectAsync(string projectId, string userName);
}