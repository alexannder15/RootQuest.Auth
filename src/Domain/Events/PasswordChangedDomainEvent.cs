using Domain.Interfaces;

namespace Domain.Events;

public record PasswordChangedDomainEvent(int Id) : IDomainEvent
{
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}
