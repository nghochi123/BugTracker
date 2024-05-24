using Microsoft.BugTracker.Repositories;
using Microsoft.BugTracker.Entities;

public class UserService
{
    private readonly UserRepository _userRepository;

    public UserService(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        if (user == null)
        {
            return null;
        }

        return new User
        {
            UserId = user.UserId,
            FullName = $"{user.FirstName} {user.LastName}",
            Email = user.Email
        };
    }
}