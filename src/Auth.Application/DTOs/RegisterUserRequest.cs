using MediatR;

namespace Auth.Application.DTOs;

public class RegisterUserRequest : IRequest<RegisterUserResponse>
{
    public string Email { get; set; }
    public string Password { get; set; }
}