using System.ComponentModel.DataAnnotations;
namespace Microsoft.BugTracker.Entities;

public class User(
    string userName,
    string passwordHash,
    string email
    )
{
    [Key]
    public string UserName { get; private set; } = userName;
    public string PasswordHash { get; private set; } = passwordHash;
    public string Email { get; private set; } = email;
    public ICollection<ProjectUser> ProjectUsers { get; set; }
}