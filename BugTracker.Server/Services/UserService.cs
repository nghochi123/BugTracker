using Microsoft.BugTracker.Repositories;
using Microsoft.BugTracker.Dtos;
using Microsoft.BugTracker.Entities;
using Microsoft.BugTracker.Interfaces;

namespace Microsoft.BugTracker.Services;
public class UserService(UserRepository userRepository) : IUserService
{
    private readonly UserRepository _userRepository = userRepository;

    public async Task<List<UserDto>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllUsersAsync();
        var userDtos = new List<UserDto>();
        foreach (var user in users)
        {
            userDtos.Add(new UserDto(user.UserName, user.Email));
        }
        return userDtos;
    }
    public async Task<UserDto> GetUserByIdAsync(string userName)
    {
        var user = await _userRepository.GetUserByIdAsync(userName);
        if (user == null)
        {
            return null;
        }

        return new UserDto(user.UserName, user.Email);
    }

    public async Task RegisterUserAsync(RegisterDto reg)
    {
        User user = new(
            reg.UserName,
            BCrypt.Net.BCrypt.HashPassword(reg.Password),
            reg.Email
        );
        await _userRepository.AddUserAsync(user);
    }
    public async Task<bool> AuthenticateUserAsync(LoginDto login)
    {
        var users = await _userRepository.GetAllUsersAsync();
        var username = login.UserName;
        foreach (var user in users)
        {
            if (user.UserName.Equals(username, StringComparison.OrdinalIgnoreCase))
            {
                if(BCrypt.Net.BCrypt.Verify(login.Password, user.PasswordHash)){
                    return true;
                }
            }
        }
        return false;
    }

    public async Task UpdateUserAsync(RegisterDto userDetails)
    {
        User user = new(
            userDetails.UserName,
            BCrypt.Net.BCrypt.HashPassword(userDetails.Password),
            userDetails.Email
        );
        await _userRepository.UpdateUserAsync(user);
    }
    
    
}