using AuthZ.Domain.Interfaces;
using AuthZ.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthZ.Infrastructure.Repositories;

public class PermissionRepository : IPermissionRepository
{
    private readonly AuthorizationDbContext _db;

    public PermissionRepository(AuthorizationDbContext db)
    {
        _db = db;
    }

    public Task<Permission?> GetByIdAsync(Guid id) =>
        _db.Permissions.FirstOrDefaultAsync(p => p.Id == id);
    
    public async Task AddAsync(Permission permission)
    {
        _db.Permissions.Add(permission);
        await _db.SaveChangesAsync();
    }

    public Task<bool> ExistsByCodeAsync(string code) =>
        _db.Permissions.AnyAsync(p => p.Code == code);
}