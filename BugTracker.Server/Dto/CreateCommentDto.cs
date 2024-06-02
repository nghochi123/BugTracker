namespace Microsoft.BugTracker.Dtos;
using System;
public class CreateCommentDto(
    string ticketId,
    string content
    )
{
    public string Content { get; set; } = content;
    public string TicketId { get; set; } = ticketId;
}