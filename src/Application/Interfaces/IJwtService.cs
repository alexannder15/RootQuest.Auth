using Domain.AggregateRoots;

namespace Application.Interfaces;

public interface IJwtService
{
    string GenerateJwtToken(User user);
}
