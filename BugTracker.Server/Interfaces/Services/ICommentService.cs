using Microsoft.BugTracker.Entities;
using Microsoft.BugTracker.Dtos;

namespace Microsoft.BugTracker.Interfaces;
public interface ICommentService
{
    Task<List<Comment>> GetAllCommentsAsync(string commentId);
    Task AddCommentAsync(Comment createComment);
    Task<Comment> GetCommentByIdAsync(string commentId);
    Task<Comment> UpdateCommentAsync(Comment comment);
    Task DeleteCommentByIdAsync(string commentId);
}