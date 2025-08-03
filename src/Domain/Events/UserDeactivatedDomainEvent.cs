using Domain.Interfaces;
using MediatR;

namespace Domain.Events;

public record UserDeactivatedDomainEvent(int Id) : IDomainEvent, INotification
{
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}
