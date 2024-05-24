using Microsoft.BugTracker.Entities;
namespace Microsoft.BugTracker.Dtos;
using System;
public class TicketDto(
    string title,
    string description,
    int priority,
    Progress status,
    DateTime createdAt,
    DateTime updatedAt,
    string[] tags
    )
{
    public string Title { get; set; } = title;
    public string Description { get; set; } = description;
    public int Priority { get; set; } = priority;
    public Progress Status { get; set; } = status;
    public DateTime CreatedAt { get; set; } = createdAt;
    public DateTime UpdatedAt { get; set; } = updatedAt;
    public string[] Tags { get; set; } = tags;
}