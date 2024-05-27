namespace Microsoft.BugTracker.Dtos;
using System;
public class CommentDto(
    string id,
    string content,
    DateTime createdAt,
    DateTime updatedAt,
    string ticketId,
    string userName
    )
{
    public string Id { get; set; } = id;
    public string Content { get; set; } = content;
    public DateTime CreatedAt { get; set; } = createdAt;
    public DateTime UpdatedAt { get; set; } = updatedAt;
    public string TicketId { get; set; } = ticketId;
    public string UserName { get; set; } = userName;
}