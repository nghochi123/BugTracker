namespace Microsoft.BugTracker.Dtos;

public class UserDto(
    string userName,
    string email
    )
{
    public string UserName { get; set; } = userName;
    public string Email { get; set; } = email;
}