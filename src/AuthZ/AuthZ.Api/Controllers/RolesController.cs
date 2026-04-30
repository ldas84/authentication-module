using AuthZ.Application.Roles.Commands;
using AuthZ.Application.Roles.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace AuthZ.Api.Controllers;

[ApiController]
[Route("api/roles")]
public class RolesController : ControllerBase
{
    private readonly CreateRoleHandler _createRole;

    public RolesController(CreateRoleHandler createRole)
    {
        _createRole = createRole;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRoleCommand command)
    {
        var id = await _createRole.HandleAsync(command);
        return Ok(new {Id = id});
    }
}