using Domain.Interfaces;

namespace Domain.Events;

public record RoleAddedToUserDomainEvent(int Id, string RoleName) : IDomainEvent
{
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}
