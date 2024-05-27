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

    public async Task<User> GetUserByIdAsync(string userName)
    {
        return await _context.Users.FindAsync(userName);
    }
    public async Task<User> UpdateUserAsync(User user)
    {
        var oldUser = await _context.Users.FindAsync(user.UserName);
        if (oldUser == null)
        {
            throw new ArgumentException("User not found with id: " + oldUser.UserName);
        }
        _context.Entry(oldUser).CurrentValues.SetValues(user);
        await _context.SaveChangesAsync();
        return user;
    }

    
}