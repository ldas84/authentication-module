using AuthZ.Domain.Entities;
using AuthZ.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuthZ.Infrastructure.Persistence.Repositories;

public class UserRoleRepository : IUserRoleRepository
{
    private readonly AuthorizationDbContext _context;

    public UserRoleRepository(AuthorizationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> ExistsAsync(Guid userId, Guid roleId)
    {
        return await _context.UserRoles
            .AnyAsync(ur => ur.UserId == userId && ur.RoleId == roleId);
    }

    public async Task AddAsync(UserRole userRole)
    {
        await _context.UserRoles.AddAsync(userRole);
        await _context.SaveChangesAsync();
    }
}
