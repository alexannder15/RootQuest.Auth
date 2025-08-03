namespace Domain.Dtos;

public class LoginUserDto
{
    public required string Email { get; set; } = string.Empty;

    public required string Password { get; set; } = string.Empty;
}
