using Microsoft.BugTracker.Entities;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.BugTracker.Repositories;
public class ProjectRepository(ApplicationDbContext context)
{
    private readonly ApplicationDbContext _context = context;

    public async Task<List<Project>> GetAllProjectsAsync()
    {
        return await _context.Projects.ToListAsync();
    }

    public async Task AddProjectAsync(Project project)
    {
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();
    }

    public async Task<Project> GetProjectByIdAsync(string id)
    {
        return await _context.Projects.FindAsync(id);
    }

    public async Task<Project> UpdateProjectAsync(Project project)
    {
        var oldProject = await _context.Projects.FindAsync(project.Id);
        if (oldProject == null)
        {
            throw new ArgumentException("Project not found with id: " + oldProject.Id);
        }
        project.SetUpdatedAt(DateTime.Now.ToUniversalTime());
        _context.Entry(oldProject).CurrentValues.SetValues(project);
        await _context.SaveChangesAsync();
        return project;
    }

    public async Task DeleteProjectByIdAsync(string projectId)
    {
        var project = await _context.Projects.FindAsync(projectId);
        if (project != null)
        {
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new ArgumentNullException("Project not found");
        }
    }

    // Assigning Users to projects
    public async Task<List<ProjectUser>> GetAllUserProjectsAsync(string username)
    { 
        var projectUsers = await _context.ProjectUsers
                                     .Include(pu => pu.Project)
                                     .Where(pu => pu.User.UserName == username)
                                     .ToListAsync();
        return projectUsers;
        
    }
    public async Task<List<ProjectUser>> GetAllProjectUsersAsync(string id)
    { 

         var projectUsers = await _context.ProjectUsers.ToListAsync();
        projectUsers = projectUsers.Where(x => x.ProjectId == id).ToList();
        return projectUsers;
        
    }

    public async Task AddProjectUserAsync(ProjectUser projectUser){
        _context.ProjectUsers.Add(projectUser);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveProjectUserAsync(string projectId, string userName)
    {
        var pUser = await _context.ProjectUsers.FirstOrDefaultAsync(
            p => p.UserName == userName && p.ProjectId == projectId
        );
        if (pUser != null)
        {
            _context.ProjectUsers.Remove(pUser);
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new ArgumentNullException("Project-User combination not found");
        }
    }

    public async Task<bool> CheckIfUserIsPartOfProjectAsync(string projectId, string userName)
    {
        var pUser = await _context.ProjectUsers.FirstOrDefaultAsync(
            p => p.UserName == userName && p.ProjectId == projectId
        );
        if (pUser == null)
        {
            return false;
        }
        return true;
    }
}