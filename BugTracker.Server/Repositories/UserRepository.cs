using Microsoft.BugTracker.Entities;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.BugTracker.Repositories;
public class UserRepository(ApplicationDbContext context)
{
    private readonly ApplicationDbContext _context = context;

    public async Task<List<User>> GetAllUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task AddUserAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    // Additional methods to update, delete, etc.
}