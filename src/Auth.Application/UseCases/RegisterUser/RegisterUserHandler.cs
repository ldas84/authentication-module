using MediatR;
using Microsoft.Extensions.Logging;
using Auth.Domain.Entities;
using Auth.Domain.Interfaces;
using Auth.Application.DTOs;
using Auth.Application.Interfaces;

namespace Auth.Application.UseCases.RegisterUser;

public class RegisterUserHandler : IRequestHandler<RegisterUserRequest, RegisterUserResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ILogger<RegisterUserHandler> _logger;

    public RegisterUserHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        ILogger<RegisterUserHandler> logger)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _logger = logger;
    }

    public async Task<RegisterUserResponse> Handle(RegisterUserRequest request, CancellationToken cancellationToken)
    {
        // 1. Duplicate validation
        var existingUser = await _userRepository.GetByEmailAsync(request.Email);
        if (existingUser != null)
        {
            _logger.LogWarning("User already exists with email: {Email}", request.Email);
            throw new Exception("User already exists.");
        }

        // 2. Hashing password
        var hashedPassword = _passwordHasher.Hash(request.Password);

        // 3. Entity creation
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            PasswordHash = hashedPassword,
            IsActive = true
        };

        // 4. Save to Database
        await _userRepository.AddAsync(user);

        // 5. Answer
        return new RegisterUserResponse
        {
            UserId = user.Id,
            Email = user.Email
        };
    }
}