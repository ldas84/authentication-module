using MediatR;

namespace Auth.Application.DTOs;
public class LoginUserRequest : IRequest<LoginUserResponse>
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}