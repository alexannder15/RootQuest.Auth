using Domain.Interfaces;

namespace Domain.Events;

public record UserDeactivatedDomainEvent(int Id) : IDomainEvent
{
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}
