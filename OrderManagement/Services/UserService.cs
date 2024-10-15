using OrderManagement.Domain;
using OrderManagement.Domain.Events;
using OrderManagement.Infrastructure;
using OrderManagement.Infrastructure.User;

namespace OrderManagement.Services;

public class UserService
{
    private readonly UserRepository _userRepository;

    public UserService(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> AddUser(User user)
    {
        var result = await _userRepository.AddUserAsync(user);

        if (!result)
            return false;

        // BUS NAAR EVENT

        return true;
    }

    public async Task<bool> UpdateUser(User user)
    {
        var result = await _userRepository.UpdateUserAsync(user);

        if (!result)
            return false;

        // BUS NAAR EVENT

        return true;
    }
}
