flowchart TB

    %% Estilos
    classDef layer fill:#1f2937,stroke:#fff,stroke-width:1px,color:#fff;
    classDef domain fill:#111827,stroke:#fff,stroke-width:1px,color:#fff;
    classDef infra fill:#374151,stroke:#fff,stroke-width:1px,color:#fff;
    classDef api fill:#4b5563,stroke:#fff,stroke-width:1px,color:#fff;

    %% Capas principales
    subgraph API["AuthZ.Api (Presentation Layer)"]
        Controllers["Controllers\n(RolesController, PermissionsController)"]
    end
    class API api

    subgraph APPLICATION["AuthZ.Application (Application Layer)"]
        Commands["Commands\n(CreateRole, CreatePermission)"]
        Handlers["Handlers\n(CreateRoleHandler, CreatePermissionHandler)"]
        DTOs["DTOs\n(RoleDto, PermissionDto)"]
    end
    class APPLICATION layer

    subgraph DOMAIN["AuthZ.Domain (Domain Layer)"]
        Role["Role"]
        Permission["Permission"]
        RolePermission["RolePermission"]
        Interfaces["Interfaces\n(IRoleRepository, IPermissionRepository, IAuthorizationService)"]
    end
    class DOMAIN domain

    subgraph INFRA["AuthZ.Infrastructure (Infrastructure Layer)"]
        DbContext["AuthorizationDbContext"]
        RoleRepo["RoleRepository"]
        PermissionRepo["PermissionRepository"]
    end
    class INFRA infra

    %% Dependencias
    API --> APPLICATION
    APPLICATION --> DOMAIN
    APPLICATION --> INFRA
    INFRA --> DOMAIN
