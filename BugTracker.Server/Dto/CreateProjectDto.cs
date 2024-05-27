namespace Microsoft.BugTracker.Dtos;
using System;
public class CreateProjectDto(
    string name,
    string description
    )
{
    public string Name { get; set; } = name;
    public string Description { get; set; } = description;
}