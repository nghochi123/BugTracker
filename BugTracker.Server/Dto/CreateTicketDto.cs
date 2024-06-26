using Microsoft.BugTracker.Entities;
namespace Microsoft.BugTracker.Dtos;
public class CreateTicketDto(

    string title,
    string description,
    int priority,
    Progress status,
    string[] tags,
    string[] assignedUserNames
    )
{
    public string Title { get; set; } = title;
    public string Description { get; set; } = description;
    public int Priority { get; set; } = priority;
    public Progress Status { get; set; } = status;    
    public string[] Tags { get; set; } = tags;

    public string[] AssignedUserNames { get; set; } = assignedUserNames;
}