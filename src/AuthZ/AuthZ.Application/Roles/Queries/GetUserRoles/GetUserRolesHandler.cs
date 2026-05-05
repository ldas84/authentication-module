using AuthZ.Domain.Interfaces;

namespace AuthZ.Application.Roles.Queries.GetUserRoles;

public class GetUserRolesHandler
{
    private readonly IUserRoleRepository _userRoleRepository;
    private readonly IRoleRepository _roleRepository;

    public GetUserRolesHandler(
        IUserRoleRepository userRoleRepository,
        IRoleRepository roleRepository)
    {
        _userRoleRepository = userRoleRepository;
        _roleRepository = roleRepository;
    }

    public async Task<UserRolesDto> HandleAsync(GetUserRolesQuery query)
    {
        // Get every UserRole of the user
        var userRoles = await _userRoleRepository.GetAllByUserIdAsync(query.UserId);

        var result = new UserRolesDto();

        foreach(var ur in userRoles)
        {
            var role = await _roleRepository.GetByIdAsync(ur.RoleId);
            if(role != null)
                result.Roles.Add(role.Name);
        }

        return result;
    }
}