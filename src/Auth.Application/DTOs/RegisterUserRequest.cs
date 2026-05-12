using MediatR;

namespace Auth.Application.DTOs;

public class RegisterUserRequest : IRequest<RegisterUserResponse>
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}