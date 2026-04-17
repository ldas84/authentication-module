using Auth.Application.DTOs;
using Auth.Application.UseCases.RegisterUser;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly RegisterUserHandler _registerHandler;

        public AuthController(RegisterUserHandler registerHandler)
        {
            _registerHandler = registerHandler;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto dto)
        {
            await _registerHandler.HandleAsync(dto);
            return Ok();
        }
    }
}
