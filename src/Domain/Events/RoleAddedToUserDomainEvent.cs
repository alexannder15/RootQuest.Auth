using Domain.Interfaces;
using MediatR;

namespace Domain.Events;

public record RoleAddedToUserDomainEvent(int Id, string RoleName) : IDomainEvent, INotification
{
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}
