using Auth.Application.Abstractions;
using AuthZ.Domain.Entities;
using AuthZ.Domain.Interfaces;
using MediatR;

namespace AuthZ.Application.Roles.Commands.AssignRole;

public class AssignRoleCommandHandler : IRequestHandler<AssignRoleCommand, bool>
{
    private readonly IUserIdentityService _userIdentityService;
    private readonly IRoleRepository _roleRepository;
    private readonly IUserRoleRepository _userRoleRepository;

    public AssignRoleCommandHandler(
        IUserIdentityService userIdentityService,
        IRoleRepository roleRepository,
        IUserRoleRepository userRoleRepository)
    {
        _userIdentityService = userIdentityService;
        _roleRepository = roleRepository;
        _userRoleRepository = userRoleRepository;
    }

    public async Task<bool> Handle(AssignRoleCommand request, CancellationToken cancellationToken)
    {
        // Validate User
        if (!await _userIdentityService.UserExistsAsync(request.UserId))
            return false;

        // Validate Role
        var role = await _roleRepository.GetByIdAsync(request.RoleId);
        if (role is null)
            return false;



        // Validate if the role is already assigned
        var alreadyAssigned = await _userRoleRepository.ExistsAsync(request.UserId, request.RoleId);
        if (alreadyAssigned)
            return true;

        // Create assignation
        var userRole = UserRole.Create(request.UserId, request.RoleId);
        await _userRoleRepository.AddAsync(userRole);

        return true;
    }
}