namespace AuthZ.Domain.Entities;

public class UserRole
{
    public Guid UserId { get; private set; }
    public Guid RoleId { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private UserRole() { }
    
    private UserRole(Guid userId, Guid roleId)
    {
        UserId = userId;
        RoleId = roleId;
        CreatedAt = DateTime.UtcNow;
    }

    public static UserRole Create(Guid userId, Guid roleId)
        =>new(userId, roleId);

}