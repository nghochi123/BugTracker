using System.ComponentModel.DataAnnotations;

namespace Microsoft.BugTracker.Entities;

public abstract class BaseEntity
{
    [Key]
    public string Id { get; set; } = DateTime.Now.ToString("yyMMddHHmmssff");
}