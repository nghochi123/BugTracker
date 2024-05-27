namespace Microsoft.BugTracker.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

public class ProjectUser(
    string projectId,
    string userName
    ) : BaseEntity
{
    [ForeignKey("Project")]
    public string ProjectId { get; private set; } = projectId;
    [ForeignKey("User")]
    public string UserName { get; private set; } = userName;
}