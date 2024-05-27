using Microsoft.BugTracker.Repositories;
using Microsoft.BugTracker.Entities;
using Microsoft.BugTracker.Interfaces;
using Microsoft.BugTracker.Dtos;

namespace Microsoft.BugTracker.Services;
public class CommentService(CommentRepository commentRepository) : ICommentService
{
    private readonly CommentRepository _commentRepository = commentRepository;

    public async Task<List<Comment>> GetAllCommentsAsync(string ticketId)
    {
        var comments = await _commentRepository.GetAllCommentsAsync(ticketId);
        return comments;
    }

    public async Task AddCommentAsync(Comment comment)
    {
        await _commentRepository.AddCommentAsync(comment);
    }

    public async Task<Comment> GetCommentByIdAsync(string commentId)
    {
        var comment = await _commentRepository.GetCommentById(commentId);
        return comment;
    }
    
    public async Task<Comment> UpdateCommentAsync(Comment comment)
    {
        return await _commentRepository.UpdateCommentAsync(comment);
    }

    public async Task DeleteCommentByIdAsync(string commentId)
    {
        await _commentRepository.DeleteCommentByIdAsync(commentId);
    }
}