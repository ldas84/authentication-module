using MediatR;

namespace Auth.Application.DTOs;

public class RegisterUserResponse
{
    public Guid UserId { get; set; }
    public required string Email { get; set; }
}