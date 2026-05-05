using Microsoft.Extensions.Logging;
using Auth.Domain.Entities;
using Auth.Domain.Interfaces;
using Auth.Application.DTOs;
using Auth.Application.Interfaces;
using System.Net.Http.Json;
using System.Net.Http;
using MediatR;

namespace Auth.Application.UseCases.Auth;

public class LoginUserHandler : IRequestHandler<LoginUserRequest, LoginUserResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ITokenService _tokenService;
    private readonly ILogger<LoginUserHandler> _logger;

    public LoginUserHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IHttpClientFactory httpClientFactory,
        ITokenService tokenService,
        ILogger<LoginUserHandler> logger)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _httpClientFactory = httpClientFactory;
        _tokenService = tokenService;
        _logger = logger;
    }

    public async Task<LoginUserResponse> Handle(LoginUserRequest request, CancellationToken cancellationToken)
    {
        // 1. Buscar usuario
        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user == null)
        {
            _logger.LogWarning("Login try with not registered mail: {Email}", request.Email);
            throw new Exception("Invalid Credentials.");
        }

        // 2. Validar contraseña
        if (!_passwordHasher.Verify(request.Password, user.PasswordHash))
        {
            _logger.LogWarning("Invalid password for email {Email}", request.Email);
            throw new Exception("Invalid Credentials.");
        }

        // 3. Llamar AuthZ para obtener roles
        var client = _httpClientFactory.CreateClient("AuthZ");

        HttpResponseMessage response;
        try
        {
            response = await client.GetAsync($"/api/roles/user/{user.Id}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while communicate with AuthZ");
            throw new Exception("Internal error validating user roles.");
        }

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogWarning("AuthZ returned error for user {Email}", request.Email);
            throw new Exception("User roles couldn't be gotten.");
        }

        var roles = await response.Content.ReadFromJsonAsync<List<string>>() 
                    ?? new List<string>();

        // 4. Generar token
        var token = _tokenService.GenerateAccessToken(user.Email, roles);

        // 5. Retornar respuesta
        return new LoginUserResponse
        {
            Token = token,
            Roles = roles,
            Email = user.Email
        };
    }
}