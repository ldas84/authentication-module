flowchart TB

    %% ============================
    %% ESTILOS
    %% ============================
    classDef module fill:#1f2937,stroke:#fff,stroke-width:1px,color:#fff;
    classDef infra fill:#374151,stroke:#fff,stroke-width:1px,color:#fff;
    classDef future fill:#4b5563,stroke:#fff,stroke-width:1px,color:#fff;
    classDef shared fill:#111827,stroke:#fff,stroke-width:1px,color:#fff;

    %% ============================
    %% AUTH MODULE
    %% ============================
    subgraph AUTH["Auth Module (Authentication)"]
        direction TB
        AUTH_API["Auth.Api\n(Login, Register, Refresh)"]
        AUTH_APP["Auth.Application\n(Handlers, Commands, Queries)"]
        AUTH_DOMAIN["Auth.Domain\n(User, RefreshToken)"]
        AUTH_INFRA["Auth.Infrastructure\n(AuthDbContext, Repositories)"]
    end
    class AUTH module

    AUTH_API --> AUTH_APP
    AUTH_APP --> AUTH_DOMAIN
    AUTH_APP --> AUTH_INFRA
    AUTH_INFRA --> AUTH_DOMAIN

    %% ============================
    %% AUTHZ MODULE
    %% ============================
    subgraph AUTHZ["AuthZ Module (Authorization)"]
        direction TB
        AUTHZ_API["AuthZ.Api\n(Roles, Permissions, Authorization)"]
        AUTHZ_APP["AuthZ.Application\n(Commands, Handlers, DTOs)"]
        AUTHZ_DOMAIN["AuthZ.Domain\n(Role, Permission, RolePermission)"]
        AUTHZ_INFRA["AuthZ.Infrastructure\n(AuthorizationDbContext, Repositories)"]
    end
    class AUTHZ module

    AUTHZ_API --> AUTHZ_APP
    AUTHZ_APP --> AUTHZ_DOMAIN
    AUTHZ_APP --> AUTHZ_INFRA
    AUTHZ_INFRA --> AUTHZ_DOMAIN

    %% ============================
    %% FUTURE MODULES
    %% ============================
    subgraph USERS["Users Module (Future)"]
        USERS_API["Users.Api"]
        USERS_APP["Users.Application"]
        USERS_DOMAIN["Users.Domain"]
        USERS_INFRA["Users.Infrastructure"]
    end
    class USERS future

    USERS_API --> USERS_APP
    USERS_APP --> USERS_DOMAIN
    USERS_APP --> USERS_INFRA
    USERS_INFRA --> USERS_DOMAIN

    subgraph NOTIF["Notifications Module (Future)"]
        NOTIF_API["Notifications.Api"]
        NOTIF_APP["Notifications.Application"]
        NOTIF_DOMAIN["Notifications.Domain"]
        NOTIF_INFRA["Notifications.Infrastructure"]
    end
    class NOTIF future

    NOTIF_API --> NOTIF_APP
    NOTIF_APP --> NOTIF_DOMAIN
    NOTIF_APP --> NOTIF_INFRA
    NOTIF_INFRA --> NOTIF_DOMAIN

    subgraph AUDIT["Audit Module (Future)"]
        AUDIT_API["Audit.Api"]
        AUDIT_APP["Audit.Application"]
        AUDIT_DOMAIN["Audit.Domain"]
        AUDIT_INFRA["Audit.Infrastructure"]
    end
    class AUDIT future

    AUDIT_API --> AUDIT_APP
    AUDIT_APP --> AUDIT_DOMAIN
    AUDIT_APP --> AUDIT_INFRA
    AUDIT_INFRA --> AUDIT_DOMAIN

    %% ============================
    %% SHARED INFRASTRUCTURE
    %% ============================
    subgraph SHARED["Shared Infrastructure"]
        DB["SQL Server"]
        CACHE["Redis (Future)"]
        BUS["Message Bus (Future)"]
        LOGS["Centralized Logging"]
    end
    class SHARED shared

    AUTH_INFRA --> DB
    AUTHZ_INFRA --> DB
    USERS_INFRA --> DB
    NOTIF_INFRA --> DB
    AUDIT_INFRA --> DB

    NOTIF_INFRA --> BUS
    AUDIT_INFRA --> BUS

    AUTH_APP --> AUTHZ_APP
    USERS_APP --> AUTH_APP
    USERS_APP --> AUTHZ_APP
