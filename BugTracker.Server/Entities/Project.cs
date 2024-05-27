namespace Microsoft.BugTracker.Entities;
using System;

public class Project(
    string name,
    string description,
    DateTime createdAt,
    DateTime updatedAt
    ) : BaseEntity
{
    public string Name { get; private set; } = name;
    public string Description { get; private set; } = description;
    public DateTime CreatedAt { get; private set; } = createdAt;
    public DateTime UpdatedAt { get; private set; } = updatedAt;

    public void SetUpdatedAt(DateTime updatedAt){
        UpdatedAt = updatedAt;
    }
}