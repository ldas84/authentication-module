using MediatR;

namespace Auth.Application.DTOs;

public class LoginUserResponse
{
    public string Token{ get; set; }
    public IEnumerable<string> Roles { get; set; }
    public string Email { get; set; }
}