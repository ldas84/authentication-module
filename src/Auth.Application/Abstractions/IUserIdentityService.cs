namespace Auth.Application.Abstractions;

public interface IUserIdentityService
{
    Task<bool> UserExistsAsync(Guid userId);
}