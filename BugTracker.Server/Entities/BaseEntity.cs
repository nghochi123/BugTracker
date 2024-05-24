using System.ComponentModel.DataAnnotations;

namespace Microsoft.BugTracker.Entities;

public abstract class BaseEntity
{
    [Key]
    public int Id { get; set; }
}