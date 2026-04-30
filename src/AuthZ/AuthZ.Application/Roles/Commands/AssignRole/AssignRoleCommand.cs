using MediatR;

namespace AuthZ.Application.Roles.Commands.AssignRole;
public record AssignRoleCommand(Guid UserId, Guid RoleId) : IRequest<bool>;