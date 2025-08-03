using Application.Interfaces;
using Domain.Dtos;
using Domain.Dtos.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(CreateUserDto userDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        string token = await authService.RegisterAsync(userDto);

        var response = new Response<string>
        {
            Code = "200.02.001",
            Mesage = "User created",
            Data = token,
        };

        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserDto loginUser)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        string token = await authService.LoginAsync(loginUser);

        var response = new Response<string>
        {
            Code = "200.02.002",
            Mesage = "User logged in successfully",
            Data = token,
        };

        return Ok(response);
    }
}
