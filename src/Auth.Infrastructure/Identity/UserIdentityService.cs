using Auth.Application.Abstractions;
using Microsoft.EntityFrameworkCore;
using Auth.Infrastructure.Persistence;

namespace Auth.Infrastructure.Identity;

public class UserIdentityService : IUserIdentityService
{
    private readonly AuthDbContext _dbContext;

    public UserIdentityService(AuthDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<bool> UserExistsAsync(Guid userId)
    {
        return _dbContext.Users.AnyAsync(u => u.Id == userId);
    }
}