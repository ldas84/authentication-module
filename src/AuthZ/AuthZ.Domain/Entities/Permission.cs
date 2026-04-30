namespace AuthZ.Domain.Entities;

public class Permission
{
    public Guid Id { get; private set; }
    public string Code { get; private set; } = string.Empty; // ← evita el warning

    private Permission() { } // EF Core

    public Permission(string code)
    {
        Id = Guid.NewGuid();
        Code = code;
    }
}
