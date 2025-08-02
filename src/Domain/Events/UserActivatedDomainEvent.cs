using Domain.Interfaces;

namespace Domain.Events;

public record UserActivatedDomainEvent(int Id) : IDomainEvent
{
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}
