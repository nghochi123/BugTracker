using Microsoft.BugTracker.Entities;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.BugTracker.Repositories;
public class CommentRepository(ApplicationDbContext context)
{
    private readonly ApplicationDbContext _context = context;

    public async Task<List<Comment>> GetAllCommentsAsync(string ticketId)
    {
        var allComments = await _context.Comments.ToListAsync();
        var ticketComments = allComments.Where(t => t.TicketId == ticketId).ToList();
        return ticketComments;
    }

    public async Task AddCommentAsync(Comment comment)
    {
        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();
    }

    public async Task<Comment> GetCommentById(string commentId)
    {
        return await _context.Comments.FindAsync(commentId);
    }


    public async Task<Comment> UpdateCommentAsync(Comment comment)
    {
        var oldComment = await _context.Comments.FindAsync(comment.Id);
        if (oldComment == null)
        {
            throw new ArgumentException("Comment not found with id: " + oldComment.Id);
        }
        comment.SetUpdatedAt(DateTime.Now.ToUniversalTime());
        _context.Entry(oldComment).CurrentValues.SetValues(comment);
        await _context.SaveChangesAsync();
        return comment;
    }

    public async Task DeleteCommentByIdAsync(string commentId)
    {
        var comment = await _context.Comments.FindAsync(commentId);
        if (comment != null)
        {
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new ArgumentNullException("Project not found");
        }
    }

    // Additional methods to update, delete, etc.
}