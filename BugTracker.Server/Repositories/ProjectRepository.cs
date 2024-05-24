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

    // Additional methods to update, delete, etc.
}