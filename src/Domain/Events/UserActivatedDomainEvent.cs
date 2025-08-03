using Domain.Interfaces;
using MediatR;

namespace Domain.Events;

public record UserActivatedDomainEvent(int Id) : IDomainEvent, INotification
{
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}
