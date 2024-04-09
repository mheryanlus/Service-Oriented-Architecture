using ServiceOrientedArchitecture.Common.Models;

namespace ServiceOrientedArchitecture.Services;

public interface IUserService
{
    Task<UserDto> CreateAsync(string firstNamae, string lastName);

    Task DeleteAsync(int userId);

    Task<IEnumerable<UserEntityDto>> GetAllUsersAsync();
}
