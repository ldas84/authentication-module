using Auth.Application.DTOs;
using Auth.Application.UseCases.RegisterUser;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using System.Reflection.Metadata.Ecma335;
using System.Net;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IMediator mediator, ILogger<AuthController> logger)
    {
        _mediator = mediator;
        _logger = logger;            
    }

    [HttpPost("auth/login")]
    public async Task<IActionResult> Login([FromBody] LoginUserRequest request)
    {
        try
        {
            var result = await _mediator.Send(request);
            return Ok(new 
            {
                token = result.Token,
                roles = result.Roles,
                email = result.Email
            });
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex, "Error in logging for user {Email}", request.Email);
            
            // Controlled Errors
            if(ex.Message.Contains("Invalid Credentials"))
                return Unauthorized(new { message = "Invalid Credentials" });
            
            // Unexpected Errors
            return StatusCode(500, new {message = "Internal server error"});
        }
    }

    [HttpPost("auth/register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
    {
        try
        {
            var result = await _mediator.Send(request);

            return CreatedAtAction(nameof(Register), new
            {
                userId = result.UserId,
                email = result.Email
            });
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Error at registering user {Email}", request.Email);

            if (ex.Message.Contains("ya existe"))
                return Conflict(new { message = "The user already exists." });

            return StatusCode(500, new { message = "Internal server error." });
        }
    }
}

