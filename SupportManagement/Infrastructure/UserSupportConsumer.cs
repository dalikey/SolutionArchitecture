using MassTransit;
using RabbitMQ.domain;
using SupportManagement.Domain;

namespace SupportManagement.Infrastructure;

public class UserSupportConsumer : IConsumer<ISupportTicketCreatedEvent>
{
    private readonly SupportRepository _supportRepository;
    public UserSupportConsumer(SupportRepository supportRepository)
    {
        _supportRepository = supportRepository;
    }

    public async Task Consume(ConsumeContext<ISupportTicketCreatedEvent> context)
    {
        Console.WriteLine("Support received on SupportManagement: " + context.Message.ToString());

        ISupportTicketCreatedEvent @event = context.Message;
        Support supportTicket = new Support
        {
            SupportTicketNumber = @event.SupportTicketNumber,
            UserEmail = @event.UserEmail,
            IssueDate = @event.IssueDate,
            Status = @event.Status,
            Description = @event.Description,
            SupportId = @event.SupportId,
            UserId = @event.UserId,
        };

        await _supportRepository.AddSupportTicketAsync(supportTicket);
    }
}
