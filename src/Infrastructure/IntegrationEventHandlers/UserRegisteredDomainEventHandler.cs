using Application.IntegrationEvents;
using Domain.Events;
using Infrastructure.Interfaces;
using MediatR;

namespace Infrastructure.IntegrationEventHandlers;

public class UserRegisteredDomainEventHandler(IRabbitMqPublisher rabbitMqPublisher)
    : INotificationHandler<UserRegisteredDomainEvent>
{
    public async Task Handle(
        UserRegisteredDomainEvent notification,
        CancellationToken cancellationToken
    )
    {
        var integrationEvent = new UserRegisteredIntegrationEvent(
            notification.UserId,
            notification.Email
        );

        await rabbitMqPublisher.PublishAsync("user.registered", integrationEvent);

        // Log the event handling
        Console.WriteLine($"[📧] User registered: {notification.Email}");
    }
}
