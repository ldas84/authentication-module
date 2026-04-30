using AuthZ.Domain.Entities;

namespace AuthZ.Domain.Interfaces;

public interface IRoleRepository
{
    Task<Role?> GetByIdAsync(Guid id);
    Task AddAsync(Role role);
    Task<bool> ExistsByNameAsync(string name);    
}