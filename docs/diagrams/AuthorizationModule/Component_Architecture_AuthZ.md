flowchart TB

    %% Styles
    classDef api fill:#4b5563,stroke:#fff,stroke-width:1px,color:#fff;
    classDef app fill:#1f2937,stroke:#fff,stroke-width:1px,color:#fff;
    classDef domain fill:#111827,stroke:#fff,stroke-width:1px,color:#fff;
    classDef infra fill:#374151,stroke:#fff,stroke-width:1px,color:#fff;

    %% ============================
    %% API LAYER
    %% ============================
    subgraph API["AuthZ.Api (Presentation Layer)"]
        direction TB
        Controllers["Controllers\n- RolesController\n- PermissionsController"]
    end
    class API api

    %% ============================
    %% APPLICATION LAYER
    %% ============================
    subgraph APPLICATION["AuthZ.Application (Application Layer)"]
        direction TB
        Commands["Commands\n- CreateRoleCommand\n- CreatePermissionCommand\n- AssignRoleCommand"]
        Handlers["Handlers\n- CreateRoleHandler\n- CreatePermissionHandler\n- AssignRoleHandler"]
        DTOs["DTOs\n- RoleDto\n- PermissionDto"]
    end
    class APPLICATION app

    %% ============================
    %% DOMAIN LAYER
    %% ============================
    subgraph DOMAIN["AuthZ.Domain (Domain Layer)"]
        direction TB
        Role["Entities\n- Role"]
        Permission["Entities\n- Permission"]
        RolePermission["Entities\n- RolePermission"]
        Interfaces["Interfaces\n- IRoleRepository\n- IPermissionRepository\n- IAuthorizationService"]
    end
    class DOMAIN domain

    %% ============================
    %% INFRASTRUCTURE LAYER
    %% ============================
    subgraph INFRA["AuthZ.Infrastructure (Infrastructure Layer)"]
        direction TB
        DbContext["AuthorizationDbContext"]
        RoleRepo["RoleRepository"]
        PermissionRepo["PermissionRepository"]
        AuthService["AuthorizationService"]
    end
    class INFRA infra

    %% ============================
    %% LAYERS DEPENDENCY
    %% ============================
    API --> APPLICATION
    APPLICATION --> DOMAIN
    APPLICATION --> INFRA
    INFRA --> DOMAIN