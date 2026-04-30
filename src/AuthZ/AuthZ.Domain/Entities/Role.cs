namespace AuthZ.Domain.Entities;

public class Role
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty; // ← evita el warning

    private Role() { } // EF Core

    public Role(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }
}