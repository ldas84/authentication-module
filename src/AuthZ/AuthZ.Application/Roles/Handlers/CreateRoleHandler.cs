using AuthZ.Application.Roles.Commands;
using AuthZ.Domain.Interfaces;
using AuthZ.Domain.Entities;

namespace AuthZ.Application.Roles.Handlers;

public class CreateRoleHandler
{
    private readonly IRoleRepository _roles;

    public CreateRoleHandler(IRoleRepository roles)
    {
        _roles = roles;
    }

    public async Task<Guid> HandleAsync(CreateRoleCommand command)
    {
        var role = new Role(command.Name);
        await  _roles.AddAsync(role);
        return role.Id;
    }
}