namespace Microsoft.BugTracker.Dtos;
public class LoginDto(
    string userName,
    string password
    )
{
    public string UserName { get; set; } = userName;
    public string Password { get; set; } = password;
}