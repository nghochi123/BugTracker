using Microsoft.BugTracker.Entities;
using Microsoft.BugTracker.Dtos;

namespace Microsoft.BugTracker.Interfaces;
public interface IUserService
{
    Task<List<UserDto>> GetAllUsersAsync();
    Task<UserDto> GetUserByIdAsync(string id);
    Task RegisterUserAsync(RegisterDto reg);
    Task<bool> AuthenticateUserAsync(LoginDto login);
    Task UpdateUserAsync(RegisterDto userDetails);
}