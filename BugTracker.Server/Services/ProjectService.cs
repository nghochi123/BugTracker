using Microsoft.BugTracker.Repositories;
using Microsoft.BugTracker.Dtos;
using Microsoft.BugTracker.Entities;
using Microsoft.BugTracker.Interfaces;

namespace Microsoft.BugTracker.Services;

public class ProjectService(ProjectRepository projectRepository) : IProjectService
{
    private readonly ProjectRepository _projectRepository = projectRepository;

    public async Task<List<ProjectDto>> GetAllProjectsAsync()
    {
        var projects = await _projectRepository.GetAllProjectsAsync();
        var projectDtos = new List<ProjectDto>();
        foreach (var project in projects)
        {
            projectDtos.Add(new ProjectDto(
                project.Id, 
                project.Name, 
                project.Description,
                project.CreatedAt,
                project.UpdatedAt
            ));
        }
        return projectDtos;
    }
    public async Task<ProjectDto> GetProjectByIdAsync(string id)
    {
        var project = await _projectRepository.GetProjectByIdAsync(id);
        if (project == null)
        {
            return null;
        }

        return new ProjectDto(
                project.Id, 
                project.Name, 
                project.Description,
                project.CreatedAt,
                project.UpdatedAt
            );
    }

    public async Task<string> CreateProjectAsync(CreateProjectDto createProject)
    {
        Project project = new(
            createProject.Name,
            createProject.Description,
            DateTime.Now.ToUniversalTime(),
            DateTime.Now.ToUniversalTime()
        );
        await _projectRepository.AddProjectAsync(project);
        return project.Id;
    }

    public async Task UpdateProjectAsync(Project project)
    {
        await _projectRepository.UpdateProjectAsync(project);
    }
    

    public async Task DeleteProjectAsync(string id){
        await _projectRepository.DeleteProjectByIdAsync(id);
    }

    // Assigning Users to Projects

    public async Task<List<ProjectUser>> GetAllUserProjectsAsync(string username){
        return await _projectRepository.GetAllUserProjectsAsync(username);
    }

    public async Task<List<ProjectUser>> GetAllProjectUsersAsync(string id){
        return await _projectRepository.GetAllProjectUsersAsync(id);
    }

    public async Task AddProjectUserAsync(ProjectUser projectUser){
        await _projectRepository.AddProjectUserAsync(projectUser);
    }

    public async Task RemoveProjectUserAsync(string projectId, string userId){
        await _projectRepository.RemoveProjectUserAsync(projectId, userId);
    }
    public async Task<bool> CheckIfUserIsPartOfProjectAsync(string projectId, string userName){
        Console.Write(projectId, userName);
        return await _projectRepository.CheckIfUserIsPartOfProjectAsync(projectId, userName);
    }
}
