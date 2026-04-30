namespace AuthZ.Domain.Interfaces;

public interface IAuthorizationService
{
    Task<bool> RoleHasPermissionAsync(Guid roleId, string permissionCode);
}