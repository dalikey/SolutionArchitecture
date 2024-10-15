using MassTransit;
using RabbitMQ.domain;
using RabbitMQ.domain.UserEvents;
using UserManagement.Domain;
using UserManagement.Domain.Events;
using UserManagement.Infrastructure;

namespace UserManagement.Services;

public class UserService
{
    private readonly UserRepository _userRepository;
    private readonly IBus _bus;

    public UserService(UserRepository userRepository, IBus bus)
    {
        _userRepository = userRepository;
        _bus = bus;
    }

    public async Task RegisterUserAsync(User user)
    {
        var result = await _userRepository.AddUserAsync(user);
        if (!result)
            return;

        IUserRegisteredEvent @event = new UserRegisteredEvent
        {
            UserId = user.UserId,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber,
            Address = user.Address,
        };
        await _bus.Publish(@event);
    }

    public async Task UpdateUserAsync(User user)
    {
        await _userRepository.UpdateUserAsync(user);

        IUserUpdatedEvent @event = new UserUpdatedEvent
        {
            UserId = user.UserId,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber,
            Address = user.Address,
        };
        await _bus.Publish(@event);
    }

    public async Task RequestUserSupport(Support support, int userId)
    {
        var supportWithId = await _userRepository.AddTicketOfUserAsync(support, userId);

        ISupportTicketCreatedEvent @event = new SupportTicketCreatedEvent
        {
            SupportTicketNumber = support.SupportTicketNumber,
            UserEmail = support.UserEmail,
            IssueDate = support.IssueDate,
            Status = support.Status,
            Description = support.Description,
            SupportId = supportWithId.SupportId,
            UserId = userId,
        };
        await _bus.Publish(@event);
    }
}
