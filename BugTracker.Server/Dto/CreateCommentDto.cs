namespace Microsoft.BugTracker.Dtos;
using System;
public class CreateCommentDto(
    string ticketId,
    string userName,
    string content
    )
{
    public string Content { get; private set; } = content;
    public string UserName { get; private set; } = userName;
    public string TicketId { get; private set; } = ticketId;
}