using Microsoft.BugTracker.Entities;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.BugTracker.Repositories;
public class CommentRepository(ApplicationDbContext context)
{
    private readonly ApplicationDbContext _context = context;

    public async Task<List<Comment>> GetAllCommentsAsync()
    {
        return await _context.Comments.ToListAsync();
    }

    public async Task AddCommentAsync(Comment comment)
    {
        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();
    }

    // Additional methods to update, delete, etc.
}