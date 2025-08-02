using Domain.AggregateRoots;

namespace Application.Interfaces;

public interface IUserContextService
{
    Task<User>? GetCurrentUserAsync();
}
