namespace Microsoft.BugTracker.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

public class UpdateCommentDto(
    string ticketId,
    string userName,
    string content
    ) : BaseEntity
{
    public string Content { get; set; } = content;
    [ForeignKey("Ticket")]
    public string TicketId { get; set; } = ticketId;
    [ForeignKey("User")]
    public string UserName { get; set; } = userName;
}