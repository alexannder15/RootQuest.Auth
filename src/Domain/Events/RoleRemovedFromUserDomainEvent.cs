using Domain.Interfaces;
using MediatR;

namespace Domain.Events;

public record RoleRemovedFromUserDomainEvent(int Id, string RoleName) : IDomainEvent, INotification
{
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}
