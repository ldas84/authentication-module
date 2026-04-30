using AuthZ.Domain.Entities;

namespace AuthZ.Domain.Interfaces;

public interface IPermissionRepository
{
    Task<Permission?>GetByIdAsync(Guid id);
    Task AddAsync(Permission permission);
    Task<bool>ExistsByCodeAsync(string code);
}