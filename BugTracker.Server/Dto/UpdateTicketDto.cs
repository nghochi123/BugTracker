using Microsoft.BugTracker.Entities;
namespace Microsoft.BugTracker.Dtos;
using System;
public class UpdateTicketDto(
    string id,
    string title,
    string description,
    int priority,
    Progress status,
    string[] assignedUserNames,
    string[] tags
    )
{
    public string Id { get; set; } = id;
    public string Title { get; set; } = title;
    public string Description { get; set; } = description;
    public int Priority { get; set; } = priority;
    public Progress Status { get; set; } = status;
    public string[] AssignedUserNames { get; private set; } = assignedUserNames;
    public string[] Tags { get; set; } = tags;
}