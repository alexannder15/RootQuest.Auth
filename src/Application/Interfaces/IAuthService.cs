using Domain.Dtos;

namespace Application.Interfaces;

public interface IAuthService
{
    Task<string> RegisterAsync(CreateUserDto userDto);
    Task<string> LoginAsync(LoginUserDto loginUser);
}
