using System.Reflection.Metadata.Ecma335;
using AuthZ.Domain.Entities;
using AuthZ.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuthZ.Infrastructure.Persistence.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly AuthorizationDbContext _context;

    public RoleRepository(AuthorizationDbContext context)
    {
        _context = context;
    }

    public async Task<Role?> GetByIdAsync(Guid id)
    {
        return await _context.Roles.FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task AddAsync(Role role)
    {
        await _context.Roles.AddAsync(role);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await _context.Roles.AnyAsync(r => r.Name == name);
    }

    public async Task<bool> ExistsAsync(Guid roleId)
    {
        return await _context.Roles.AnyAsync(r => r.Id == roleId);
    }    

    
}
