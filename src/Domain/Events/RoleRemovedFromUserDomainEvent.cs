using Domain.Interfaces;

namespace Domain.Events;

public record RoleRemovedFromUserDomainEvent(int Id, string RoleName) : IDomainEvent
{
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}
