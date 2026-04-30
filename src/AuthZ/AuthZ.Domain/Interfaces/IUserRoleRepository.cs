using AuthZ.Domain.Entities;

namespace AuthZ.Domain.Interfaces;

public interface IUserRoleRepository
{
    Task<bool> ExistsAsync(Guid userId, Guid roleId);
    Task AddAsync(UserRole userRole);
}
