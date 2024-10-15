using MassTransit;
using OrderManagement.Infrastructure.Order;
using RabbitMQ.domain.UserEvents;

namespace OrderManagement.Infrastructure.User;

public class UserConsumer : IConsumer<IUserUpdatedEvent>
{
    private readonly OrderRepository _orderRepository;
    
    public UserConsumer(OrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task Consume(ConsumeContext<IUserUpdatedEvent> context)
    {
        Console.WriteLine("User received on Order management: " + context.Message.ToString());
        
        IUserUpdatedEvent @event = context.Message;
        Domain.User user = new Domain.User
        {
            UserId = @event.UserId,
            Email = @event.Email,
            FirstName = @event.FirstName,
            LastName = @event.LastName
        };

        await _orderRepository.UpdateUserAsync(user);
    }
}
