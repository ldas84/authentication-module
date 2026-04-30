using AuthZ.Application.Permissions.Commands;
using AuthZ.Application.Permissions.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace AuthZ.Api.Controllers;

[ApiController]
[Route("api/permissions")]
public class PermissionsController : ControllerBase
{
    private readonly CreatePermissionHandler _createPermission;

    public PermissionsController(CreatePermissionHandler createPermission)
    {
        _createPermission = createPermission;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePermissionCommand command)
    {
        var id = await _createPermission.HandleAsync(command);
        return Ok(new { Id = id });
    }
}