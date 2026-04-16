using Auth.Application.DTOs;
using Auth.Application.Interfaces;

namespace Auth.Application.UseCases.RegisterUser;

public class RegisterUserHandler
{    
    private readonly IAuthService _authService;

    public RegisterUserHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task HandleAsync(RegisterUserDto dto)
    {
        await _authService.RegisterAsync(dto);
    }
}