namespace Microsoft.BugTracker.Entities;

public class User(
    string userName,
    string passwordHash,
    string email
    ) : BaseEntity
{
    public string UserName { get; private set; } = userName;
    public string PasswordHash { get; private set; } = passwordHash;
    public string Email { get; private set; } = email;
}