using MediatR;

namespace Auth.Application.DTOs;

public class LoginUserResponse
{
    public required string Token{ get; set; }
    public required IEnumerable<string> Roles { get; set; }
    public required string Email { get; set; }
}