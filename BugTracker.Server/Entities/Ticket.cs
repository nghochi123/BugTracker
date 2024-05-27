using System.ComponentModel.DataAnnotations.Schema;

namespace Microsoft.BugTracker.Entities;

public enum Progress {
    Closed,
    Open,
    InProgress,
}

public class Ticket(
    string title,
    string description,
    int priority,
    Progress status,
    string[] assignedUserNames,
    DateTime createdAt,
    DateTime updatedAt,
    string[] tags,
    string projectId
    
    ) : BaseEntity
{
    public string Title { get; private set; } = title;
    public string Description { get; private set; } = description;
    public int Priority { get; private set; } = priority;
    public Progress Status { get; private set; } = status;
    public string[] AssignedUserNames { get; private set; } = assignedUserNames;
    public DateTime CreatedAt { get; private set; } = createdAt;
    public DateTime UpdatedAt { get; private set; } = updatedAt;
    public string[] Tags { get; private set; } = tags;
    [ForeignKey("Project")]
    public string ProjectId { get; private set; } = projectId;

    public void SetUpdatedAt(DateTime updatedAt){
        UpdatedAt = updatedAt;
    }

}   