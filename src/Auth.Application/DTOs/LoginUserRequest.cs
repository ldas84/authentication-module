using MediatR;

namespace Auth.Application.DTOs;
public class LoginUserRequest : IRequest<LoginUserResponse>
{
    public string Email { get; set; }
    public string Password { get; set; }
}