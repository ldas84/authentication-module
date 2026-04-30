using AuthZ.Application.Permissions.Commands;
using AuthZ.Domain.Interfaces;
using AuthZ.Domain.Entities;

namespace AuthZ.Application.Permissions.Handlers;

public class CreatePermissionHandler
{
    private readonly IPermissionRepository _permissions;

    public CreatePermissionHandler(IPermissionRepository permission)
    {
        _permissions = permission;
    }

    public async Task<Guid> HandleAsync(CreatePermissionCommand command)
    {
        var permission = new Permission(command.Code);
        await _permissions.AddAsync(permission);
        return permission.Id;
    }
}