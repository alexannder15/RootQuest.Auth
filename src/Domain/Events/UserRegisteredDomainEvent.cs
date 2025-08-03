using Domain.Interfaces;
using MediatR;

namespace Domain.Events;

public record UserRegisteredDomainEvent(int UserId, string Email) : IDomainEvent, INotification
{
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}
