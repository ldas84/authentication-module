using AuthZ.Application.Roles.Commands;
using AuthZ.Application.Roles.Handlers;
using AuthZ.Application.Roles.Queries.GetUserRoles;
using Microsoft.AspNetCore.Mvc;

namespace AuthZ.Api.Controllers;

[ApiController]
[Route("api/roles")]
public class RolesController : ControllerBase
{
    private readonly CreateRoleHandler _createRole;
    private readonly GetUserRolesHandler _handler;

    public RolesController(CreateRoleHandler createRole)
    {
        _createRole = createRole;
    }

    public RolesController(GetUserRolesHandler handler)
    {
        _handler = handler;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRoleCommand command)
    {
        var id = await _createRole.HandleAsync(command);
        return Ok(new {Id = id});
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetUserRoles(Guid userId)
    {
        var result = await _handler.HandleAsync(new GetUserRolesQuery(userId));
        return Ok(result);
    }
}