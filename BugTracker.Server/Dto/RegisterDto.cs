namespace Microsoft.BugTracker.Dtos;
public class RegisterDto(
    string userName,
    string password,
    string email
    )
{
    public string UserName { get; set; } = userName;
    public string Password { get; set; } = password;
    public string Email { get; set; } = email;
}