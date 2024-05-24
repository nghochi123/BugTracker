namespace Microsoft.BugTracker.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

public class Comment(
    string ticketId,
    string userId,
    string content,
    DateTime createdAt,
    DateTime updatedAt
    ) : BaseEntity
{
    public string Content { get; private set; } = content;
    public DateTime CreatedAt { get; private set; } = createdAt;
    public DateTime UpdatedAt { get; private set; } = updatedAt;
    [ForeignKey("Ticket")]
    public string TicketId { get; private set; } = ticketId;
    [ForeignKey("User")]
    public string UserId { get; private set; } = userId;
}