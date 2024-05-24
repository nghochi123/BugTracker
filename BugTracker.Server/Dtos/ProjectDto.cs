namespace Microsoft.BugTracker.Dtos;
using System;
public class ProjectDto(
    string name,
    string description,
    DateTime createdAt,
    DateTime updatedAt
    )
{
    public string Name { get; set; } = name;
    public string Description { get; set; } = description;
    public DateTime CreatedAt { get; set; } = createdAt;
    public DateTime UpdatedAt { get; set; } = updatedAt;
}