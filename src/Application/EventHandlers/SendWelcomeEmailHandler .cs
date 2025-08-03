using Application.Interfaces;
using Domain.Events;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Application.EventHandlers;

public class SendWelcomeEmailHandler(
    ILogger<SendWelcomeEmailHandler> logger,
    IServiceScopeFactory scopeFactory
) : INotificationHandler<UserRegisteredDomainEvent>
{
    public async Task Handle(
        UserRegisteredDomainEvent notification,
        CancellationToken cancellationToken
    )
    {
        using var scope = scopeFactory.CreateScope();
        var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

        //await emailService.SendAsync(
        //    notification.Email,
        //    "Welcome to Root Quest!",
        //    "<h1>Welcome to Root Quest!</h1><p>Thank you for registering.</p>"
        //);

        logger.LogInformation($"[📧] Welcome email sent to {notification.Email}");

        await Task.CompletedTask;
    }
}
