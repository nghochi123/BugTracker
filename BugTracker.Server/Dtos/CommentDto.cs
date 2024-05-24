namespace Microsoft.BugTracker.Dtos;
using System;
public class CommentDto(
    string content,
    DateTime createdAt,
    DateTime updatedAt
    )
{
    public string Content { get; set; } = content;
    public DateTime CreatedAt { get; set; } = createdAt;
    public DateTime UpdatedAt { get; set; } = updatedAt;
}