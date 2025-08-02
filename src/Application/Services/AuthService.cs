using System.Text.Json;
using Application.Exceptions;
using Application.Interfaces;
using Domain.AggregateRoots;
using Domain.Dtos;
using Microsoft.AspNetCore.Identity;

namespace Application.Services;

public class AuthService(UserManager<User> userManager, IJwtService jwtService) : IAuthService
{
    public async Task<string> RegisterAsync(CreateUserDto createUser)
    {
        var userByEmail = await userManager.FindByEmailAsync(createUser.Email);

        if (userByEmail != null)
            throw new EmailAlreadyExistException("Email already exist");

        var user = new User(createUser.Email, createUser.Email);

        var isCreated = await userManager.CreateAsync(user, createUser.Password);

        if (isCreated.Succeeded)
            return jwtService.GenerateJwtToken(user);

        throw new UnhandledException(
            $"Something was wrong with Register!: {JsonSerializer.Serialize(isCreated.Errors)}"
        );
    }

    public async Task<string> LoginAsync(LoginUserDto loginUser)
    {
        User? user =
            await userManager.FindByEmailAsync(loginUser.Email)
            ?? throw new EmailNotFoundException("User doesn't exist");

        bool isCorrect = await userManager.CheckPasswordAsync(user, loginUser.Password);

        if (!isCorrect)
            throw new InvalidCredentialsException("Invalid credentials");

        return jwtService.GenerateJwtToken(user);
    }
}
