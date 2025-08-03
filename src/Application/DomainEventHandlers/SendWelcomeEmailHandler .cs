using Domain.Events;
using MediatR;

namespace Application.DomainEventHandlers;

public class SendWelcomeEmailHandler : INotificationHandler<UserRegisteredDomainEvent>
{
    public Task Handle(UserRegisteredDomainEvent notification, CancellationToken cancellationToken)
    {
        // Lógica para enviar el correo de bienvenida
        Console.WriteLine($"[📧] Welcome email sent to {notification.Email}");
        return Task.CompletedTask;
    }
}
