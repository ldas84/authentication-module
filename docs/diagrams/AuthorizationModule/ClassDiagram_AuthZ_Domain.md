classDiagram

    class Role {
        +Guid Id
        +string Name
        +Role(string name)
    }

    class Permission {
        +Guid Id
        +string Code
        +Permission(string code)
    }

    class RolePermission {
        +Guid RoleId
        +Guid PermissionId
        +RolePermission(Guid roleId, Guid permissionId)
    }

    class IRoleRepository {
        <<interface>>
        +Task<Role?> GetByIdAsync(Guid id)
        +Task AddAsync(Role role)
        +Task<bool> ExistsByNameAsync(string name)
    }

    class IPermissionRepository {
        <<interface>>
        +Task<Permission?> GetByIdAsync(Guid id)
        +Task AddAsync(Permission permission)
        +Task<bool> ExistsByCodeAsync(string code)
    }

    class IAuthorizationService {
        <<interface>>
        +Task<bool> RoleHasPermissionAsync(Guid roleId, string permissionCode)
    }

    %% Relaciones
    Role "1" --> "0..*" RolePermission : has
    Permission "1" --> "0..*" RolePermission : grants

    IRoleRepository --> Role
    IPermissionRepository --> Permission
    IAuthorizationService --> Role
    IAuthorizationService --> Permission
